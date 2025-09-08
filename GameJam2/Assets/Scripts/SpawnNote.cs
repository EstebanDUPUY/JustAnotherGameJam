using UnityEngine;
using System.Collections;

public class SpawnNote : MonoBehaviour
{
    public GameObject note;
    void Start()
    {
        StartCoroutine(SpawnNoteInRythm());
    }

    IEnumerator SpawnNoteInRythm()
    {
        while (true)
        {
            Instantiate(note, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
