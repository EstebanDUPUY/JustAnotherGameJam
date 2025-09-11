using UnityEngine;

public class DataRythmeScore : MonoBehaviour
{
    public static DataRythmeScore Instance;

    public int miss;
    public int bad;
    public int good;
    public int perfect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    public void ResetScore()
    {
        miss = 0;
        bad = 0;
        good = 0;
        perfect = 0;
    }

    public int GetTotalNote()
    {
        return miss + bad + good + perfect;
    }

    public string GetJudgement()
    {
        float total = GetTotalNote();
        float totalMaxScore = total * 3;
        float scorePlayer = bad * 1 + good * 2 + perfect * 3;

        float judge = scorePlayer / totalMaxScore * 100;




        Debug.Log("Total = " + total);
        Debug.Log("TotalMax = " + totalMaxScore);
        Debug.Log("ScorePlayer = " + scorePlayer);
        Debug.Log("judge = " + judge);


        if (judge == 100)
        {
            return "FULL COMBO!!\n AMAZING!!";
        }
        else if (judge < 100 && judge >= 70)
        {
            return "GREAT JOB!";
        }
        else if (judge < 70 && judge >= 50)
        {
            return "I've seen better...";
        }
        else
        {
            return "Life is not Daijobu.";
        }
    }
}
