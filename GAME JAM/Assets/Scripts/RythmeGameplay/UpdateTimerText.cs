using TMPro;
using UnityEngine;

public class UpdateTimerText : MonoBehaviour
{
    private TextMeshProUGUI textMP;
    private void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        TimerReadyLevel.UpdateTimerLevel += UpdateText;
    }

    private void OnDisable()
    {
        TimerReadyLevel.UpdateTimerLevel -= UpdateText;
    }

    private void UpdateText(string _str)
    {
        textMP.text = _str;
    }
}
