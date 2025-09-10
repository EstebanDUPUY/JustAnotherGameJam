using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static AudioManager Instance;

    private AudioSource[] sources;
    //rivate AudioSource music;

    private AudioClip clipHerbal;
    private AudioClip clipNoel;
    private AudioClip clipEpic;
    private AudioClip sfxMissNote;

    public static event Action<float> FindBPM;
    public float bpm;
    private float saveBpm;


    public int sampleSize = 1024; // FFT size
    public float threshold = 1.5f; // seuil pour considérer un onset
    public int smoothCount = 5; // nombre de BPM moyens à lisser

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //music = GetComponent<AudioSource>();

        sources = GetComponents<AudioSource>();



        clipHerbal = Resources.Load<AudioClip>("Musics/herbal tea - Artificial.Music_130");
        clipNoel = Resources.Load<AudioClip>("Musics/Noel_S7_80bpm");
        clipEpic = Resources.Load<AudioClip>("Musics/Epic_120");
        sfxMissNote = Resources.Load<AudioClip>("SFX/classic_hurt");

        sources[0].clip = clipEpic;
        sources[1].clip = sfxMissNote;

        FindBPM?.Invoke(bpm);
    }

    void OnEnable()
    {
        TriggerMusic.MusicOn += PlaySong;
    }

    void OnDisable()
    {
        TriggerMusic.MusicOn -= PlaySong;
    }

    public void PlaySfx()
    {
        sources[1].Play();
        Debug.Log("I'm playing song");
    }

    public void PlaySong()
    {
        sources[0].Play();
    }
}
