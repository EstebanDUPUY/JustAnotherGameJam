using UnityEngine;

public class ScoreResultManager : MonoBehaviour
{
    public static ScoreResultManager Instance;
    private GameObject resultScreen;
    private GameObject judgeScreen;
    private GameObject gameUIScreen;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        resultScreen = transform.Find("ScoreResult").gameObject;
        judgeScreen = transform.Find("JudgeText").gameObject;
        gameUIScreen = transform.Find("OnGameUI").gameObject;
    }

    private void OnEnable()
    {
        BossSpawnNote.endMusic += ShowResult;
    }

    private void OnDisable()
    {
        BossSpawnNote.endMusic -= ShowResult;
    }

    private void ShowResult()
    {
        resultScreen.SetActive(true);
        judgeScreen.SetActive(true);
        gameUIScreen.SetActive(false);
        resultScreen.GetComponent<ScoreResult>().SetResult();
        judgeScreen.GetComponent<JudgeTextUI>().SetJudgement();
    }

    public void HideResult()
    {
        resultScreen.SetActive(false);
    }
}
