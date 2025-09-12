using UnityEngine;
using System;

public class PauseGame : MonoBehaviour
{
    private bool isPause = false;
    public void Pause()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
            AudioManager.Instance.PauseSong();
            isPause = true;
        }
    }

    public void UnPause()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            AudioManager.Instance.ResumeSong();
            isPause = false;
        }
    }
}
