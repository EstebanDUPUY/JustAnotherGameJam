using System;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted = true;

    public DataNote.NoteType type;
    public bool isValided = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 12f);
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(beatTempo * Time.fixedDeltaTime, 0f, 0f);
    }
}
