using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ScoreResult : MonoBehaviour
{
    private TextMeshProUGUI textMP;
    private void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }


    public void SetResult()
    {
        string result = "Perfect : " + DataRythmeScore.Instance.perfect.ToString() +
                        "\n\nGood : " + DataRythmeScore.Instance.good.ToString() +
                        "\n\nBad : " + DataRythmeScore.Instance.bad.ToString() +
                        "\n\nMiss : " + DataRythmeScore.Instance.miss.ToString();

        DataRythmeScore.Instance.ResetScore();
        textMP.text = result;
    }
}
