using UnityEngine;

public class TriggerSwitchLane : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<NoteMovement>().type == DataNote.NoteType.TrickNote)
            {
                float tempoY = 0;

                if (collision.transform.position.y == 0)
                {
                    tempoY = 4;
                }

                collision.transform.position = new Vector3(collision.transform.position.x, tempoY, 0);
                collision.GetComponent<NoteMovement>().type = DataNote.NoteType.SimpleNote;
                collision.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
