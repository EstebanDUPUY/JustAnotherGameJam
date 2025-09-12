using UnityEngine;
using System.Collections;
using System;
public class TimerReadyLevel : MonoBehaviour
{
    public static event Action StartLevel;
    public static event Action<string> UpdateTimerLevel;

    void Start()
    {
        StartCoroutine(WaitForLevel());
    }
    IEnumerator WaitForLevel()
    {
        UpdateTimerLevel?.Invoke("3");
        yield return new WaitForSeconds(1f);
        UpdateTimerLevel?.Invoke("2");
        yield return new WaitForSeconds(1f);
        UpdateTimerLevel?.Invoke("1");
        yield return new WaitForSeconds(1f);
        UpdateTimerLevel?.Invoke("GO!");
        yield return new WaitForSeconds(0.5f);
        UpdateTimerLevel?.Invoke("");
        StartLevel?.Invoke();
    }
}
