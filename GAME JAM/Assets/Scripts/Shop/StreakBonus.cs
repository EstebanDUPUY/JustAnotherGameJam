using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class StreakBonus : MonoBehaviour
{
    [Header("Streak settings")]
    public int streakToActivate = 10;
    private int currentStreak = 0;
    private bool bonusReady = false;

    [Header("Bonus settings")]
    public float bonusDuration = 5f;
    private bool bonusActive = false;
    private float bonusTimer = 0f;

    private InputAction bonusAction;

    public static event Action<string> SignalText;

    void Awake()
    {
        // Create Bonus action directly bound to Space
        bonusAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        bonusAction.performed += OnBonus;
        bonusAction.Enable();
    }

    void OnDestroy()
    {
        bonusAction.performed -= OnBonus;
        bonusAction.Disable();
    }

    void Update()
    {
        if (bonusActive)
        {
            bonusTimer -= Time.deltaTime;
            if (bonusTimer <= 0f)
            {
                EndBonus();
            }
        }
    }

    public void OnNoteSuccess()
    {
        Debug.Log("I here bonus");
        if (bonusActive) return;

        currentStreak++;
        if (currentStreak >= streakToActivate)
        {
            bonusReady = true;
            SignalText?.Invoke("Bonus Ready!");
        }
    }

    public void OnNoteFail()
    {
        currentStreak = 0;
        bonusReady = false;
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

    private void EndBonus()
    {
        bonusActive = false;
        SignalText?.Invoke("Bonus Ended.");
    }
}
