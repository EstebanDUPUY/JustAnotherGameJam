using UnityEngine;

public class DataNote : MonoBehaviour
{
    public static DataNote Instance;

    public enum NoteType
    {
        SimpleNote,
        BombNote,
        HoldNote,
        TrickNote,
        LongNote
    }

    void Start()
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
}
