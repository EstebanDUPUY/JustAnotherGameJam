using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
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
    public static event Action<bool> AddCombo;

    public bool missValue = false;

    public int id;
    public List<Transform> childZone;

    private GameObject vfx;

    public bool perfectCombo = true;

    private void Start()
    {
        childZone = new List<Transform>();

        foreach (Transform child in transform)
        {
            childZone.Add(child);
        }

        vfx = Resources.Load<GameObject>("Prefabs/VFX/highlight");
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

            if (!other.GetComponent<NoteMovement>().isValided && !other.GetComponent<NoteMovement>().isAlreadyMissed)
            {
                SignalText?.Invoke("Miss!");
                other.GetComponent<NoteMovement>().isAlreadyMissed = true;
                AudioManager.Instance.PlaySfx(AudioManager.SfxCode.miss);
                DataRythmeScore.Instance.miss += 1;
                perfectCombo = false;
                AddCombo?.Invoke(false);
            }
        }
    }

    public void OnNote(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (context.interaction is PressInteraction && context.phase == InputActionPhase.Started && tempoNoteObject == null)
            {
                SignalText?.Invoke("Too Soon!");
                perfectCombo = false;
                AddCombo?.Invoke(false);
                return;
            }


            if (context.interaction is PressInteraction && context.phase == InputActionPhase.Started)
            {
                GameObject newVFX = Instantiate(vfx, transform.position, Quaternion.identity);
                Destroy(newVFX, 1f);
                CheckIfInZone();
            }
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
                        AudioManager.Instance.PlaySfx(AudioManager.SfxCode.perfect);
                        AddScore?.Invoke(100);
                        AddCombo?.Invoke(true);
                        SignalText?.Invoke("PERFECT!");
                        DataRythmeScore.Instance.perfect += 1;
                        perfectCombo = true;
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.GoodZone:
                        SignalText?.Invoke("Good!");
                        DataRythmeScore.Instance.good += 1;
                        perfectCombo = false;
                        AddCombo?.Invoke(false);
                        AudioManager.Instance.PlaySfx(AudioManager.SfxCode.good);
                        AddScore?.Invoke(50);
                        find = true;
                        break;
                    case DataZoneCollider.ZoneType.MissZone:
                        SignalText?.Invoke("Bad!");
                        DataRythmeScore.Instance.bad += 1;
                        perfectCombo = false;
                        AddCombo?.Invoke(false);
                        AddScore?.Invoke(10);
                        AudioManager.Instance.PlaySfx(AudioManager.SfxCode.bad);
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
