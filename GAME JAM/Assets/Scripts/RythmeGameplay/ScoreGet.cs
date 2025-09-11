using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreGet : MonoBehaviour
{
    private TextMeshProUGUI textMP;
    private int score = 0;
    void Start()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        textMP.text = "Score = 0";
    }

    private void OnEnable()
    {
        ButtonManager.AddScore += UpdateScore;
    }

    private void OnDisable()
    {
        ButtonManager.AddScore -= UpdateScore;
    }

    private void UpdateScore(int _score)
    {
        score += _score;
        textMP.text = "Score = " + score.ToString();
    }
}
