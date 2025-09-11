using System;
using TMPro;
using UnityEngine;

public class UpdateInfoText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TextMeshProUGUI textMP;
    public int comboCounter = 0;
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        ButtonManager.SignalText += OnUpdateText;
        ButtonManager.AddCombo += OnUpdateCombo;
    }

    void OnDisable()
    {
        ButtonManager.SignalText -= OnUpdateText;
        ButtonManager.AddCombo -= OnUpdateCombo;

    }

    private void OnUpdateText(string _text)
    {
        if (_text == "PERFECT!")
        {
            textMP.fontSize = 50;
            textMP.text = _text + " x " + comboCounter.ToString();
            return;
        }
        textMP.text = _text;
    }

    private void OnUpdateCombo(bool _isCombo)
    {
        if (_isCombo)
            comboCounter += 1;
        else
            comboCounter = 0;
    }
}
