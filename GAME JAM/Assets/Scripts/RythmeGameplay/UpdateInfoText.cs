using System;
using TMPro;
using UnityEngine;

public class UpdateInfoText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TextMeshProUGUI textMP;
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        ButtonManager.SignalText += OnUpdateText;
    }

    void OnDisable()
    {
        ButtonManager.SignalText -= OnUpdateText;
    }

    private void OnUpdateText(string _text)
    {
        if (_text == "PERFECT!")
            textMP.fontSize = 50;
        textMP.text = _text;
    }
}
