using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ButtonManager : MonoBehaviour
{
    private bool canActivateNote = false;
    private GameObject tempoNoteObject;
    private NoteMovement tempoNote;
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
                Debug.Log("Missed!");
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (canActivateNote)
        {
            if (tempoNote.type == 0)
                ShortNoteActivate();
            if (tempoNote.type == 1)
                LongNoteActivate(context);
        }
    }

    private void ShortNoteActivate()
    {
        tempoNote.isValided = true;
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
