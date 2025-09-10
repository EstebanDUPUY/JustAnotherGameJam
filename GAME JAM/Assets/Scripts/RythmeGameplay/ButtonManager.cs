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
    public static event Action<int> AddScore;

    public bool missValue = false;

    public int id;
    public List<Transform> childZone;

    private GameObject vfx;

    private void Start()
    {
        childZone = new List<Transform>();

        foreach (Transform child in transform)
        {
            childZone.Add(child);
        }

        vfx = Resources.Load<GameObject>("Prefabs/VFX/Water_Explosion 1");
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

        if (other.CompareTag("LongEnter"))
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
                AudioManager.Instance.PlaySfx();
            }
        }

        /*if (other.CompareTag("LongEnter") && !other.GetComponentInParent<NoteMovement>().isValided)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            SignalText?.Invoke("Miss!");
        }

        if (other.CompareTag("LongEnter") && !other.GetComponentInParent<NoteMovement>().isValided)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            SignalText?.Invoke("Miss!");
            
        }*/
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (tempoNoteObject == null)
            return;


        if (context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
        {
            GameObject newVFX = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(newVFX, 1f);

            if (tempoNoteObject == null)
            {
                SignalText?.Invoke("Sois patient stp");
                return;
            }

            CheckIfInZone();
        }

    }

    public void OnNoteLong(InputAction.CallbackContext context)
    {

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
                        AddScore?.Invoke(100);
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.GoodZone:
                        SignalText?.Invoke("Good!");
                        AddScore?.Invoke(50);
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.MissZone:
                        SignalText?.Invoke("Bad!");
                        AddScore?.Invoke(10);
                        find = true;
                        break;
                }

                if (find)
                    tempoNote.isValided = true;

                if (tempoNote.type != DataNote.NoteType.LongNote)
                    Destroy(tempoNoteObject);

                return;
            }
        }
    }
}
