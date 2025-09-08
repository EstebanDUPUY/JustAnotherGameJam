using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;

public class ButtonManager : MonoBehaviour
{
    private bool canActivateNote = false;
    private GameObject tempoNoteObject;
    private NoteMovement tempoNote;

    private BoxCollider2D buttonCollider;

    private Bounds b;

    public static event Action<string> SignalText;

    void LateUpdate()
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
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (canActivateNote && context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
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
        SignalText?.Invoke("Success!");
        Destroy(tempoNoteObject);
    }

    private void BombNoteActivate()
    {
        tempoNote.isValided = true;
        SignalText?.Invoke("BOOOM!");
        Destroy(tempoNoteObject);
    }

    private void LongNoteActivate(InputAction.CallbackContext context)
    {
        /*if (context.interaction is HoldInteraction)
        {
            tempoNote.longNoteEnter = true;
            tempoNoteObject.transform.parent.Find("NoteLongBody").GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (context.canceled)
        {
            if (tempoNote.longNoteEnter)
                tempoNote.isLongValided = true;
            Destroy(tempoNoteObject.transform.parent.gameObject);
        }*/
    }
}
