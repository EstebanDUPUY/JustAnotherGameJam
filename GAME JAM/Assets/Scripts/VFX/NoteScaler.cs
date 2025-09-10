using UnityEngine;

public class NoteScaler : MonoBehaviour
{
    [HideInInspector] public Transform targetButton;
    public float minScale = 0.1f;   // scale when far away
    public float maxScale = 1f;     // scale when on button
    private float startDistance = 1f;

    void Start()
    {
        transform.localScale = Vector3.one * minScale;
    }

    void Update()
    {
        if (targetButton == null) return;

        float currentDistance = Vector2.Distance(transform.position, targetButton.position);
        float t = Mathf.Clamp01(1f - currentDistance / startDistance);

        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = Vector3.one * scale;
    }

    // Assign the target button when spawned
    public void SetTargetButton(Transform button)
    {
        targetButton = button;
        if (targetButton != null)
        {
            startDistance = Vector2.Distance(transform.position, targetButton.position);
            if (startDistance == 0) startDistance = 0.1f; // avoid division by zero
        }
    }
}



