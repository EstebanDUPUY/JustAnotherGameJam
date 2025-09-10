using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Data.Common;
using Unity.VisualScripting;
using System.Collections.Generic;

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
    public List<Transform> childZone;

    private void Start()
    {
        childZone = new List<Transform>();

        foreach (Transform child in transform)
        {
            childZone.Add(child);
        }

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
            tempoNoteObject = other.gameObject;
            tempoNote = other.gameObject.GetComponent<NoteMovement>();
        }

        if (other.CompareTag("LongEnter") || other.CompareTag("LongExit"))
        {
            tempoNoteObject = other.gameObject;
            tempoNote = other.gameObject.GetComponentInParent<NoteMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {

            if (!other.GetComponent<NoteMovement>().isValided)
            {
                SignalText?.Invoke("Miss!");
            }
            canActivateNote = false;
        }

        if ((other.CompareTag("LongEnter") || other.CompareTag("LongExit")) && !other.GetComponentInParent<NoteMovement>().isValided)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            SignalText?.Invoke("Miss!");
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
        {
            if (tempoNoteObject == null)
            {
                SignalText?.Invoke("Sois patient stp");
                return;
            }

            CheckIfInZone();

            /*if (GetComponent<BoxCollider2D>().OverlapPoint(tempoNoteObject.transform.position))
                {
                    if (tempoNote.type == DataNote.NoteType.SimpleNote)
                        ShortNoteActivate();
                    if (tempoNote.type == DataNote.NoteType.BombNote)
                        BombNoteActivate();
                }*/
            }
    }

    private void CheckIfInZone()
    {
        bool find = false;

        foreach (Transform child in childZone)
        {
            BoxCollider2D tempoBox = child.GetComponent<BoxCollider2D>();
            DataZoneCollider.ZoneType tempoType = child.GetComponent<ZoneButton>().type;
            if (tempoBox.bounds.Intersects(tempoNoteObject.GetComponent<BoxCollider2D>().bounds))
            {

                switch (tempoType)
                {
                    case DataZoneCollider.ZoneType.PerfectZone:
                        SignalText?.Invoke("PERFECT!");
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.GoodZone:
                        SignalText?.Invoke("Good!");
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.MissZone:
                        SignalText?.Invoke("Bad!");
                        find = true;
                        break;
                }

                if (find)
                {
                    if (tempoNote.type == DataNote.NoteType.LongNote && tempoNote.isValided)
                    {
                        Debug.Log("I'm here");
                        tempoNote.isLongValided = true;
                    }

                    tempoNote.isValided = true;

                    if (tempoNote.type == DataNote.NoteType.LongNote && tempoNote.isLongValided)
                    {
                        Destroy(tempoNoteObject.transform.parent.gameObject);
                    }
                    if (tempoNote.type != DataNote.NoteType.LongNote)
                            Destroy(tempoNoteObject);



                    
                    return;
                }
            }
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

}
