using UnityEngine;

public class MissZone : MonoBehaviour
{
    private ButtonManager buttonM;

    void Start()
    {
        buttonM = GetComponentInParent<ButtonManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            buttonM.missValue = true;
        }
    }

        void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            buttonM.missValue = false;
        }
    }
}
