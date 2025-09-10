using UnityEngine;

public class DataZoneCollider : MonoBehaviour
{
    public static DataZoneCollider Instance;

    public enum ZoneType
    {
        MissZone,
        GoodZone,
        PerfectZone
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

