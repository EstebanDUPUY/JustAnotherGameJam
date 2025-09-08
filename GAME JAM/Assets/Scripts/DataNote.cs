using System;
using Unity.Android.Gradle.Manifest;
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
