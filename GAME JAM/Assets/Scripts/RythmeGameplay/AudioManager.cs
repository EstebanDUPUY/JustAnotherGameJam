using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource music;

    public AudioClip clip;

    public static event Action<float> FindBPM;
    public float bpm = 80f;
    private float saveBpm;


    public int sampleSize = 1024; // FFT size
    public float threshold = 1.5f; // seuil pour considérer un onset
    public int smoothCount = 5; // nombre de BPM moyens à lisser


    void Start()
    {
        //bpm = UniBpmAnalyzer.AnalyzeBpm(clip);

       

        /*if (bpm > 200)
            bpm /= 2;
        if (bpm < 60)
            bpm *= 2;*/

        Debug.Log("bpm = " + bpm);

        FindBPM?.Invoke(bpm);
        //music.Play(0);
    }

    void OnEnable()
    {
        TriggerMusic.MusicOn += PlaySong;
    }

    void OnDisable()
    {
        TriggerMusic.MusicOn -= PlaySong;
    }

    private void PlaySong()
    {
        music.Play(0);
    }
}
