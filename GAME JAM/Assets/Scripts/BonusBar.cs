using UnityEngine;
using UnityEngine.UI;

public class BonusBar : MonoBehaviour
{
    private Image barImage;
    private ButtonManager buttonManager;

    void Awake()
    {
        // Create Canvas if none exists
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Create the bar
        GameObject barGO = new GameObject("BonusBar");
        barGO.transform.SetParent(canvas.transform);

        barImage = barGO.AddComponent<Image>();
        barImage.color = Color.cyan;
        barImage.type = Image.Type.Filled;
        barImage.fillMethod = Image.FillMethod.Horizontal;
        barImage.fillAmount = 0f;

        // Set size and position
        RectTransform rt = barGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.1f, 0.9f);
        rt.anchorMax = new Vector2(0.4f, 0.95f);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Find ButtonManager automatically
        buttonManager = FindObjectOfType<ButtonManager>();
        if (buttonManager == null)
            Debug.LogWarning("ButtonManager not found. Bonus bar won't work.");
    }

    void Update()
    {
        if (buttonManager != null)
        {
            float progress = Mathf.Clamp01((float)buttonManager.CurrentStreak / buttonManager.StreakToActivate);
            barImage.fillAmount = progress;

            // Change color if bonus ready
            barImage.color = buttonManager.BonusReady ? Color.yellow : Color.cyan;
        }
    }
}
