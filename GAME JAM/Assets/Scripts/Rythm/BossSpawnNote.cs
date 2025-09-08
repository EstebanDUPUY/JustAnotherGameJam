using UnityEngine;
using System;
using System.Collections;

public class BossSpawnNote : MonoBehaviour
{

    public static event Action spawnNote;
    private float timeSpawn = 1f;

    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
    }
    
    IEnumerator SpawnNoteInRythm()
    {
        while (true)
        {
            spawnNote?.Invoke();
            yield return new WaitForSeconds(timeSpawn);
        }
    }

}
