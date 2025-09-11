using TMPro;
using UnityEngine;

public class JudgeTextUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TextMeshProUGUI textMP;

    void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetJudgement()
    {
        textMP.text = DataRythmeScore.Instance.GetJudgement();
    }
}
