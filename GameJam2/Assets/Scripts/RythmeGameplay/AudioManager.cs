using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource music;

    public static event Action<float> FindBPM;
    public float bpm = 80f;
    private float saveBpm;


    public int sampleSize = 1024; // FFT size
    public float threshold = 1.5f; // seuil pour considérer un onset
    public int smoothCount = 5; // nombre de BPM moyens à lisser

    private float[] spectrum;
    private float lastEnergy = 0f;
    private float lastOnsetTime = 0f;
    private List<float> bpmHistory = new List<float>();







    void Start()
    {
        //bpm = UniBpmAnalyzer.AnalyzeBpm(clip);

        spectrum = new float[sampleSize];


        music.Play(0);
        FindBPMRunTime();
    }

    void Update()
    {
        music.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float currentEnergy = 0f;

        // Somme de l'énergie des basses fréquences (fréquences percussives)
        for (int i = 0; i < spectrum.Length / 8; i++)
            currentEnergy += spectrum[i];

        // Détection d'un onset
        if (currentEnergy > lastEnergy * threshold)
        {
            float time = Time.time;
            float interval = time - lastOnsetTime;

            if (interval > 0.05f) // filtre pour éviter le double comptage
            {
                float bpm = 60f / interval;
                bpmHistory.Add(bpm);

                if (bpmHistory.Count > smoothCount)
                    bpmHistory.RemoveAt(0);

                float avgBpm = 0f;
                foreach (float b in bpmHistory) avgBpm += b;
                avgBpm /= bpmHistory.Count;

                Debug.Log("BPM instantané : " + avgBpm.ToString("F1"));
                FindBPM?.Invoke(avgBpm);
            }

            lastOnsetTime = time;
        }

        lastEnergy = currentEnergy;
    }

    private void FindBPMRunTime()
    {
        music.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float currentEnergy = 0f;

        // Somme de l'énergie des basses fréquences (fréquences percussives)
        for (int i = 0; i < spectrum.Length / 8; i++)
            currentEnergy += spectrum[i];

        // Détection d'un onset
        if (currentEnergy > lastEnergy * threshold)
        {
            float time = Time.time;
            float interval = time - lastOnsetTime;

            if (interval > 0.05f) // filtre pour éviter le double comptage
            {
                float bpm = 60f / interval;
                bpmHistory.Add(bpm);

                if (bpmHistory.Count > smoothCount)
                    bpmHistory.RemoveAt(0);

                float avgBpm = 0f;
                foreach (float b in bpmHistory) avgBpm += b;
                avgBpm /= bpmHistory.Count;

                Debug.Log("BPM instantané : " + avgBpm.ToString("F1"));
                FindBPM?.Invoke(avgBpm);
            }

            lastOnsetTime = time;
        }

        lastEnergy = currentEnergy;
    }
}
