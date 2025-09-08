using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource music;
    public float bpm = 80f;    
    void Start()
    {
        music.Play(0);
    }
}
