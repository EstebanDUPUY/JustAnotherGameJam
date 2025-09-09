using UnityEngine;
using System.Collections;

public class SpawnNote : MonoBehaviour
{
    [SerializeField] private int id;

    private float bpmFind;

    void OnEnable()
    {
        BossSpawnNote.spawnNote += OnSpawnNote;
        //AudioManager.FindBPM += ChangeBPMOnRunTime;
    }

    void OnDisable()
    {
        BossSpawnNote.spawnNote -= OnSpawnNote;
        //AudioManager.FindBPM -= ChangeBPMOnRunTime;
    }

    /*private void ChangeBPMOnRunTime(float _bpm)
    {
        bpmFind = _bpm;
    }*/

    private void OnSpawnNote(int _id, GameObject note)
    {
        if (_id == id)
        {
            GameObject tempo;
            tempo = Instantiate(note, this.transform.position, Quaternion.identity);
            //tempo.GetComponent<NoteMovement>().beatTempo = bpmFind;
            
        }
    }


}
