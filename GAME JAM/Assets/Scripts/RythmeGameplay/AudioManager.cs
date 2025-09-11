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
    private AudioClip sfxGoodNote;
    private AudioClip sfxPerfectNote;

    public static event Action<float> FindBPM;
    public float bpm;
    private float saveBpm;

    private DataSongs.SongName currentSong;

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
        sfxBadNote = Resources.Load<AudioClip>("Musics/Esteban/clean-whoosh-382726");
        sfxGoodNote = Resources.Load<AudioClip>("Musics/Esteban/simple-whoosh-382724");
        sfxPerfectNote = Resources.Load<AudioClip>("Musics/Esteban/whoosh-effect-382717");

        if (!sources[0].clip)
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

    public void SetCurrentSong(DataSongs.SongName _name)
    {
        currentSong = _name;
        SetSongSystem();
    }


    public void SetSongSystem()
    {
        switch (currentSong)
        {
            case DataSongs.SongName.Level1:
                bpm = 80;
                sources[0].clip = clipNoel;
                break;
            case DataSongs.SongName.Level2:
                bpm = 120;
                sources[0].clip = clipEpic;
                break;
            default:
                break;
        }
    }
    
    public AudioSource GetAudio()
    {
        return sources[0];
    }

    public void PlaySfx(SfxCode _code)
    {
        if (_code == SfxCode.miss)
            sources[1].clip = sfxMissNote;
        if (_code == SfxCode.bad)
            sources[1].clip = sfxBadNote;
        if (_code == SfxCode.good)
            sources[1].clip = sfxGoodNote;
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
