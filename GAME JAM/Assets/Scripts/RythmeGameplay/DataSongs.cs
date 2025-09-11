using UnityEngine;
using System.Collections.Generic;
public class DataSongs : MonoBehaviour
{
    public static DataSongs Instance;

    public enum SongName
    {
        Level1,
        Level2,
        Level3
    }

    public Dictionary<SongName, float> SongDictionary;

    void Awake()
    {
        SongDictionary = new Dictionary<SongName, float>
        {
            {SongName.Level1, 80},
            {SongName.Level2, 120}
        };
    }
}
