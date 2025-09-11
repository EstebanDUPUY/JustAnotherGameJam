using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;

public class BossSpawnNote : MonoBehaviour
{

    public static event Action<int, GameObject> spawnNote;
    public static event Action endMusic;

    private GameObject simpleNote;
    private GameObject trickNote;

    //private float bpm = 60f;

    public bool isLevelPlaying = false;

    private bool ready = true;

    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
        //StartCoroutine(WaitCooldown());

        simpleNote = Resources.Load<GameObject>("Prefabs/Note/NoteSimple");
        trickNote = Resources.Load<GameObject>("Prefabs/Note/NoteTrick");
    }

    void OnEnable()
    {
        AudioManager.FindBPM += ChangeBPMOnRunTime;
        TriggerMusic.MusicOn += PlayLevel;
    }

    void OnDisable()
    {
        AudioManager.FindBPM -= ChangeBPMOnRunTime;
        TriggerMusic.MusicOn -= PlayLevel;

    }

    private void PlayLevel()
    {
        isLevelPlaying = true;
    }

    private void ChangeBPMOnRunTime(float _bpm)
    {
        //bpm = _bpm;
    }

    void Update()
    {
        if (!AudioManager.Instance.GetAudio().isPlaying && isLevelPlaying && ready)
        {
            ready = false;
            isLevelPlaying = false;
            StopAllCoroutines();
            StartCoroutine(WaitCooldown());
        }
    }


    private GameObject ChooseNote()
    {
        GameObject tempo = null;
        int random = UnityEngine.Random.Range(0, 2);

        switch (random)
        {
            case 0:
                tempo = simpleNote;
                break;
            case 1:
                tempo = trickNote;
                break;
        }
        return tempo;
    }

    private int ChooseLane()
    {
        return UnityEngine.Random.Range(0, 2);
    }

    private bool isDouble()
    {
        if (UnityEngine.Random.Range(0, 4) == 3)
        {
            return true;
        }

        return false;
    }

    IEnumerator SpawnNoteInRythm()
    {
        int laneId = 0;
        int laneIdTempo = 0;
        GameObject noteChosen = null;
        while (ready)
        {
            laneId = ChooseLane();
            
            noteChosen = ChooseNote();

            if (isDouble())
            {
                noteChosen = simpleNote;

                if (laneId == 0)
                    laneIdTempo = 1;
                else if (laneId == 1)
                    laneIdTempo = 0;

                spawnNote?.Invoke(laneIdTempo, noteChosen);
            }

            spawnNote?.Invoke(laneId, noteChosen);

            yield return new WaitForSeconds(60f / AudioManager.Instance.bpm);
        }
    }

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(5f);
        endMusic?.Invoke();
    }

}
