using UnityEngine;
using System.Collections;

public class SpawnNote : MonoBehaviour
{
    [SerializeField] private int id;

    void OnEnable()
    {
        BossSpawnNote.spawnNote += OnSpawnNote;
    }

    void OnDisable()
    {
        BossSpawnNote.spawnNote -= OnSpawnNote;
    }

    private void OnSpawnNote(int _id, GameObject note)
    {
        if (_id == id)
            Instantiate(note, this.transform.position, Quaternion.identity);
    }


}
