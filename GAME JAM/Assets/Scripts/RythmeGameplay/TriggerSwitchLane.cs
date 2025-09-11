using UnityEngine;

public class TriggerSwitchLane : MonoBehaviour
{
    private Color colorSimple = new(232f, 84f, 202f, 255f);
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
                //collision.GetComponent<SpriteRenderer>().color = colorSimple;
            }
        }
    }
}
