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

    private void Start()
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
            // Récupère les bounds
            Bounds a = other.gameObject.GetComponent<BoxCollider2D>().bounds;
            Debug.Log("a =" + a);
            //Bounds b = buttonCollider.bounds;
            Debug.Log("b =" + b);


            // Calcule l’intersection (largeur & hauteur)
            float overlapX = Mathf.Max(0, Mathf.Min(a.max.x, b.max.x) - Mathf.Max(a.min.x, b.min.x));
            float overlapY = Mathf.Max(0, Mathf.Min(a.max.y, b.max.y) - Mathf.Max(a.min.y, b.min.y));

            // Surface de l’intersection
            float overlapArea = overlapX * overlapY;

            // Surfaces des deux colliders
            float areaA = a.size.x * a.size.y;
            float areaB = b.size.x * b.size.y;

            // Pourcentage d’overlap par rapport au plus petit collider
            float overlapPercent = overlapArea / Mathf.Min(areaA, areaB);

            Debug.Log(overlapPercent);
            SignalText?.Invoke(overlapPercent.ToString());


            if (!other.GetComponent<NoteMovement>().isValided)
            {
                SignalText?.Invoke("Missed!");
            }
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (canActivateNote && context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
        {
            if (tempoNote.type == 0)
                ShortNoteActivate();
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
