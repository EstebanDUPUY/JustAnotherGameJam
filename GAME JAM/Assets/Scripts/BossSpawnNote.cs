using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class BossSpawnNote : MonoBehaviour
{

    public static event Action<int, GameObject> spawnNote;
    private float timeSpawn = 1f;

    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
        Resources.Load<GameObject>("NoteSimple");
    }

    private GameObject ChooseNote()
    {
        GameObject tempo = null;
        int random = UnityEngine.Random.Range(0, 2);

        switch (random)
        {
            case 0:
                tempo = Resources.Load<GameObject>("NoteSimple");
                break;
            case 1:
                tempo = Resources.Load<GameObject>("NoteBomb");
                break;
        }
        return tempo;
    }
    
    IEnumerator SpawnNoteInRythm()
    {
        while (true)
        {
            spawnNote?.Invoke(UnityEngine.Random.Range(0,2), ChooseNote());
            yield return new WaitForSeconds(timeSpawn);
        }
    }

}
