using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public float bpm = 80f;    
    void Start()
    {
        music.Play(0);
    }
}
