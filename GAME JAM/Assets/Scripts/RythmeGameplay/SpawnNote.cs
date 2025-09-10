using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    [SerializeField] private int id; // 0 or 1 to match BossSpawnNote

    public GameObject SaveLane; // optional, can leave null
    private Transform leftButton;
    private Transform rightButton;

    void Awake()
    {
        // Automatically find buttons in the scene by name
        GameObject lb = GameObject.Find("Bouton1");
        GameObject rb = GameObject.Find("Bouton2");

        if (lb == null || rb == null)
            Debug.LogError("Bouton1 or Bouton2 not found in the scene!");

        leftButton = lb?.transform;
        rightButton = rb?.transform;
    }

    void OnEnable()
    {
        BossSpawnNote.spawnNote += OnSpawnNote;
    }

    void OnDisable()
    {
        BossSpawnNote.spawnNote -= OnSpawnNote;
    }

    private void OnSpawnNote(int _id, GameObject notePrefab)
    {
        Debug.Log($"OnSpawnNote called with _id: {_id}, notePrefab: {notePrefab}");

        if (_id != id || notePrefab == null) return;

        // Instantiate note (as child of SaveLane if assigned)
        GameObject tempo;
        if (SaveLane != null)
            tempo = Instantiate(notePrefab, transform.position, Quaternion.identity, SaveLane.transform);
        else
            tempo = Instantiate(notePrefab, transform.position, Quaternion.identity);

        Debug.Log("Note spawned at position: " + transform.position);

        // Assign the target button
        Transform targetButton = (_id % 2 == 0) ? leftButton : rightButton;

        NoteScaler scaler = tempo.GetComponent<NoteScaler>();
        if (scaler != null)
            scaler.SetTargetButton(targetButton);
    }
}

