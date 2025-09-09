using UnityEngine;
using UnityEngine.UI;

public class BonusBar : MonoBehaviour
{
    private Image barImage;

    void Awake()
    {
        // Cherche un Canvas existant
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            // Crée un Canvas si aucun n’existe
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Crée la barre
        GameObject barGO = new GameObject("BonusBar");
        barGO.transform.SetParent(canvas.transform);

        barImage = barGO.AddComponent<Image>();
        barImage.color = Color.cyan;           // couleur par défaut
        barImage.type = Image.Type.Filled;
        barImage.fillMethod = Image.FillMethod.Horizontal;
        barImage.fillAmount = 0f;

        // Position et taille
        RectTransform rt = barGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.1f, 0.9f);
        rt.anchorMax = new Vector2(0.4f, 0.95f);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // S’abonner aux events de StreakSystem
        StreakSystem.OnStreakProgress += UpdateBar;
    }

    void OnDestroy()
    {
        StreakSystem.OnStreakProgress -= UpdateBar;
    }

    private void UpdateBar(float progress)
    {
        if (barImage != null)
        {
            barImage.fillAmount = progress;
            // change la couleur si le bonus est prêt
            barImage.color = progress >= 1f ? Color.yellow : Color.cyan;
        }
    }
}
