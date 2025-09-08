using UnityEngine;
using System.Collections;

public class SpawnNote : MonoBehaviour
{


    void OnEnable()
    {
        BossSpawnNote.spawnNote += OnSpawnNote;
    }

    void OnDisable()
    {
        BossSpawnNote.spawnNote -= OnSpawnNote;
    }

    private void OnSpawnNote()
    {
        Instantiate(Resources.Load<GameObject>("NoteSimple"), this.transform.position, Quaternion.identity);
    }


}
