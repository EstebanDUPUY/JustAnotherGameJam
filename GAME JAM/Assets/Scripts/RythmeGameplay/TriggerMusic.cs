using UnityEngine;
using System;

public class TriggerMusic : MonoBehaviour
{
    public static event Action MusicOn;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            MusicOn?.Invoke();
            Destroy(gameObject);
        }
    }
}
