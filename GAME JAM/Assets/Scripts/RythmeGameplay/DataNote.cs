using System;
using UnityEngine;

public class DataNote : MonoBehaviour
{
    public static DataNote Instance;

    public enum NoteType
    {
        SimpleNote,
        BombNote,
        HoldNote,
        TrickNote
   }
}
