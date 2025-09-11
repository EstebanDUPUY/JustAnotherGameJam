using UnityEngine;


public class NoteMovement : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted = true;

    private SpriteRenderer sprite;

    public DataNote.NoteType type;
    public bool isValided = false;
    public bool isLongValided = false;
    public bool isAlreadyMissed = false;


    void Awake()
    {
        Destroy(gameObject, 5);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        beatTempo /= 60f;


        /*if (type == DataNote.NoteType.TrickNote)
        {
            StartCoroutine(SwitchLane());
        }*/
    }

    void OnEnable()
    {
        //AudioManager.FindBPM += ChangeBPMOnRunTime;
    }

    void Onsable()
    {
        //AudioManager.FindBPM -= ChangeBPMOnRunTime;

    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        transform.position -= new Vector3((10 + AudioManager.Instance.extraSpeedNote) * Time.fixedDeltaTime, 0f, 0f);
    }
}
