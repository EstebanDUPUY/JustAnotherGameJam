using UnityEngine;
using System.Collections;

public class SpawnNote : MonoBehaviour
{
    [SerializeField] private int id;

    private float bpmFind;

    public GameObject SaveLane;

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
        {
            GameObject tempo;
            tempo = Instantiate(note, this.transform.position, Quaternion.identity, SaveLane.transform);         
        }
    }


}
