using UnityEngine;

public class ZoneButton : MonoBehaviour
{
    public DataZoneCollider.ZoneType type;

    public int id;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (id == 1)
        {
            if (collision.CompareTag("Note"))
            {
                Debug.Log("NOW");
            }
        }
    }

}
