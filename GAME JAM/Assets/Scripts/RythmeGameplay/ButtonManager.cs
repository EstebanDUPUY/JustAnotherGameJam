using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Data.Common;

public class ButtonManager : MonoBehaviour
{
    public bool canActivateNote = false;
    private GameObject tempoNoteObject;
    private NoteMovement tempoNote;

    private BoxCollider2D buttonCollider;

    private Bounds b;

    public static event Action<string> SignalText;

    public bool missValue = false;

    public int id;

    private void Start()
    {

    }

    void Update()
    {
        buttonCollider = GetComponent<BoxCollider2D>();
        Bounds b = buttonCollider.bounds;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            canActivateNote = true;
            tempoNoteObject = other.gameObject;
            tempoNote = other.gameObject.GetComponent<NoteMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {

            if (!other.GetComponent<NoteMovement>().isValided)
            {
                if (tempoNote.type == DataNote.NoteType.BombNote)
                    SignalText?.Invoke("SAFE!");
                else
                    SignalText?.Invoke("Missed!");
            }
            canActivateNote = false;
            Debug.Log("Here Main Core");
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        Debug.Log("Je suis ici dans le OnNote = " + id);
        if ((canActivateNote || missValue) && context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
        {
            if (tempoNote.type == DataNote.NoteType.SimpleNote)
                ShortNoteActivate();
            if (tempoNote.type == DataNote.NoteType.BombNote)
                BombNoteActivate();
        }
    }

    private void ShortNoteActivate()
    {
        tempoNote.isValided = true;
        if (canActivateNote)
            SignalText?.Invoke("Perfect!");
        else
            SignalText?.Invoke("Bad!");
        Debug.Log("missValue = " + missValue);
        missValue = false;
        canActivateNote = false;
        Destroy(tempoNoteObject);
    }

    private void BombNoteActivate()
    {
        tempoNote.isValided = true;
        SignalText?.Invoke("BOOOM!");
        canActivateNote = false;
        Destroy(tempoNoteObject);
    }
}
