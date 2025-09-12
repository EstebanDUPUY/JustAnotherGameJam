using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    private TextMeshProUGUI textMP;
    private void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }


    public void SetResult()
    {
        DataRythmeScore.Instance.CalculateScorePlayer();
        string result = "SCORE : " + DataRythmeScore.Instance.scorePlayerValue.ToString() +
                        "\n\nPerfect : " + DataRythmeScore.Instance.perfect.ToString() +
                        "\n\nGood : " + DataRythmeScore.Instance.good.ToString() +
                        "\n\nBad : " + DataRythmeScore.Instance.bad.ToString() +
                        "\n\nMiss : " + DataRythmeScore.Instance.miss.ToString();

        textMP.text = result;
    }
}
