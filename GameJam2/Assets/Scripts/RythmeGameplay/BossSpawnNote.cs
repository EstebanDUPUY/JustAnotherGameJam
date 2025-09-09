using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class BossSpawnNote : MonoBehaviour
{

    public static event Action<int, GameObject> spawnNote;
    private float timeSpawn = 2f;

    private float bpm;

    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
    }

    void OnEnable()
    {
        AudioManager.FindBPM += ChangeBPMOnRunTime;
    }

    void OnDisable()
    {
        AudioManager.FindBPM -= ChangeBPMOnRunTime;
    }

    private void ChangeBPMOnRunTime(float _bpm)
    {
        bpm = _bpm;
    }


    private GameObject ChooseNote()
    {
        GameObject tempo = null;
        int random = UnityEngine.Random.Range(0, 3);

        switch (random)
        {
            case 0:
                tempo = Resources.Load<GameObject>("NoteSimple");
                break;
            case 1:
                tempo = Resources.Load<GameObject>("NoteBomb");
                break;
            case 2:
                tempo = Resources.Load<GameObject>("NoteTrick");
                break;
        }
        return tempo;
    }
    
    IEnumerator SpawnNoteInRythm()
    {
        while (true)
        {
            spawnNote?.Invoke(UnityEngine.Random.Range(0,2), ChooseNote());
            yield return new WaitForSeconds(bpm / 60);
        }
    }

}
