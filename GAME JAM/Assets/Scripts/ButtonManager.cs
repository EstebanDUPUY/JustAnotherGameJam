using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ButtonManager : MonoBehaviour
{
    private bool canActivateNote = false;
    private GameObject tempoNoteObject;
    private NoteMovement tempoNote;

    public static event Action<string> SignalText;

    private InputAction noteAction;
    private InputAction bonusAction;

    [Header("Bonus Settings")]
    public int streakToActivate = 10;
    private int currentStreak = 0;
    private bool bonusReady = false;
    private bool bonusActive = false;
    private float bonusTimer = 0f;
    public float bonusDuration = 5f;

    void Awake()
    {
        // Create Note action (example bound to J key)
        noteAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/j");
        noteAction.performed += OnNote;
        noteAction.Enable();

        // Create Bonus action bound to Space
        bonusAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        bonusAction.performed += OnBonus;
        bonusAction.Enable();
    }

    private void OnDestroy()
    {
        noteAction.performed -= OnNote;
        bonusAction.performed -= OnBonus;
        noteAction.Disable();
        bonusAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            canActivateNote = true;
            tempoNoteObject = other.gameObject;
            tempoNote = tempoNoteObject.GetComponent<NoteMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if (!other.GetComponent<NoteMovement>().isValided)
            {
                SignalText?.Invoke("Missed!");
                ResetStreak();
            }
            canActivateNote = false;
        }
    }

    private void OnNote(InputAction.CallbackContext ctx)
    {
        if (canActivateNote && tempoNote != null && tempoNote.type == 0)
        {
            tempoNote.isValided = true;
            SignalText?.Invoke("Success!");
            Destroy(tempoNoteObject);

            // Handle streak
            currentStreak++;
            if (currentStreak >= streakToActivate)
            {
                bonusReady = true;
                SignalText?.Invoke("Bonus Ready!");
            }
        }
    }

    private void OnBonus(InputAction.CallbackContext ctx)
    {
        if (bonusReady && !bonusActive)
        {
            bonusActive = true;
            bonusReady = false;
            currentStreak = 0;
            bonusTimer = bonusDuration;
            SignalText?.Invoke("Bonus Activated!");
        }
    }

    void Update()
    {
        if (bonusActive)
        {
            bonusTimer -= Time.deltaTime;
            if (bonusTimer <= 0f)
            {
                bonusActive = false;
                SignalText?.Invoke("Bonus Ended.");
            }
        }
    }

    private void ResetStreak()
    {
        currentStreak = 0;
        bonusReady = false;
    }

    // Properties for the BonusBar
    public int CurrentStreak => currentStreak;
    public int StreakToActivate => streakToActivate;
    public bool BonusReady => bonusReady;
}
