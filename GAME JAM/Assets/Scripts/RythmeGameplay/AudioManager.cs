using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static AudioManager Instance;

    public enum SfxCode
    {
        miss,
        bad,
        good,
        perfect,
        hover,
        click,
        score
    }

    private AudioSource[] sources;

    private AudioClip clipTitleSong;
    private AudioClip clipLevel1;
    private AudioClip clipLevel2;
    private AudioClip clipLevel3;
    //private AudioClip clipEpic;
    private AudioClip sfxMissNote;
    private AudioClip sfxBadNote;
    private AudioClip sfxGoodNote;
    private AudioClip sfxPerfectNote;
    private AudioClip sfxBubbleHover;
    private AudioClip sfxBubbleClick;
    private AudioClip sfxScoreEnd;

    public static event Action<float> FindBPM;
    public float bpm;
    private float saveBpm;

    [HideInInspector] public float extraSpeedNote = 0;
    [HideInInspector] public float timerSong = 0;

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



        clipTitleSong = Resources.Load<AudioClip>("Musics/FinalMusic/AquaticTitle");
        clipLevel1 = Resources.Load<AudioClip>("Musics/FinalMusic/SousOcean_99bpm");
        clipLevel2 = Resources.Load<AudioClip>("Musics/FinalMusic/Epic_120");
        clipLevel3 = Resources.Load<AudioClip>("Musics/FinalMusic/Pirate_70");
        //clipEpic = Resources.Load<AudioClip>("Musics/Epic_120");
        sfxMissNote = Resources.Load<AudioClip>("SFX/classic_hurt");
        sfxBadNote = Resources.Load<AudioClip>("Musics/Esteban/clean-whoosh-382726");
        sfxGoodNote = Resources.Load<AudioClip>("Musics/Esteban/simple-whoosh-382724");
        sfxPerfectNote = Resources.Load<AudioClip>("Musics/Esteban/whoosh-effect-382717");
        sfxBubbleHover = Resources.Load<AudioClip>("SFX/selected_bubble");
        sfxBubbleClick = Resources.Load<AudioClip>("SFX/clicked_bubble");
        sfxScoreEnd = Resources.Load<AudioClip>("Musics/FinalMusic/endscore");

        if (!sources[0].clip)
            sources[0].clip = clipLevel2;


        sources[2].clip = clipTitleSong;
        FindBPM?.Invoke(bpm);
        PlayTitleSong(true);
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
                bpm = 99;
                extraSpeedNote = 0;
                timerSong = clipLevel1.length;
                sources[0].clip = clipLevel1;
                break;
            case DataSongs.SongName.Level2:
                bpm = 120;
                extraSpeedNote = 0;
                timerSong = clipLevel2.length;
                sources[0].clip = clipLevel2;
                break;
            case DataSongs.SongName.Level3:
                bpm = 70;
                extraSpeedNote = 10;
                timerSong = clipLevel3.length - 5f;
                sources[0].clip = clipLevel3;
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
        if (_code == SfxCode.hover)
        {
            sources[1].PlayOneShot(sfxBubbleHover);
            return;
        }
        if (_code == SfxCode.click)
        {
            sources[1].PlayOneShot(sfxBubbleClick);
            return;
        }
        if (_code == SfxCode.score)
        {
            sources[1].PlayOneShot(sfxScoreEnd);
            return;
        }

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

    public void PauseSong()
    {
        sources[0].Pause();
    }

    public void ResumeSong()
    {
        sources[0].UnPause();
    }

    public void StopSong()
    {
        sources[0].Stop();
    }

    public void PlayTitleSong(bool _play)
    {
        if (_play)
            sources[2].Play();
        else
            sources[2].Stop();
    }
}
