using System;
using UnityEngine;
using System.Collections;
using System.Data.Common;
using UnityEngine.Rendering.Universal;

public class NoteMovement : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted = true;

    private SpriteRenderer sprite;

    public DataNote.NoteType type;
    public bool isValided = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 12f);
        beatTempo = beatTempo / 60f;

        /*if (type == DataNote.NoteType.TrickNote)
        {
            StartCoroutine(SwitchLane());
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(beatTempo * Time.fixedDeltaTime, 0f, 0f);
    }

    /*IEnumerator SwitchLane()
    {
        float tempoY = 0;

        yield return new WaitForSeconds(8f);

        if (transform.position.y == 0)
        {
            tempoY = 4;
        }

        transform.position = new Vector3(transform.position.x, tempoY, 0);
        type = DataNote.NoteType.SimpleNote;
        sprite.color = Color.white;
    }*/
}
