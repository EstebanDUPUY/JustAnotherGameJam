using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static AudioManager Instance;

    public enum SfxCode
    {
        miss,
        bad,
        good,
        perfect
    }

    private AudioSource[] sources;

    private AudioClip clipHerbal;
    private AudioClip clipNoel;
    private AudioClip clipEpic;
    private AudioClip sfxMissNote;
    private AudioClip sfxBadNote;
    private AudioClip sfxPerfectNote;

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
        sfxBadNote = Resources.Load<AudioClip>("Musics/Esteban/crdn_upgrkick_012-85590");
        sfxPerfectNote = Resources.Load<AudioClip>("Musics/Esteban/arrow-impact-87260");

        sources[0].clip = clipEpic;

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



    public AudioSource GetAudio()
    {
        return sources[0];
    }

    public void SetSongSystem()
    {
        
    }

    public void PlaySfx(SfxCode _code)
    {
        if (_code == SfxCode.miss)
            sources[1].clip = sfxMissNote;
        if (_code == SfxCode.bad || _code == SfxCode.good)
            sources[1].clip = sfxBadNote;
        if (_code == SfxCode.perfect)
            sources[1].clip = sfxPerfectNote;

        sources[1].Play();
    }

    public void StopAllSongs()
    {
        sources[0].Stop();
        sources[1].Stop();
    }

    public void PlaySong()
    {
        sources[0].Play();
    }
}
