using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class BossSpawnNote : MonoBehaviour
{

    public static event Action<int, GameObject> spawnNote;
    public static event Action endMusic;

    private float bpm = 60f;

    public bool isLevelPlaying = false;

    private bool ready = true;

    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
        StartCoroutine(WaitCooldown());
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
        bpm = _bpm;
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
                tempo = Resources.Load<GameObject>("Prefabs/Note/NoteSimple");
                break;
            case 1:
                tempo = Resources.Load<GameObject>("Prefabs/Note/NoteTrick");
                break;
        }
        return tempo;
    }

    IEnumerator SpawnNoteInRythm()
    {
        while (ready)
        {
            spawnNote?.Invoke(UnityEngine.Random.Range(0, 2), ChooseNote());
            yield return new WaitForSeconds(60f / bpm);
        }
    }

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(2f);
        endMusic?.Invoke();
    }

}
