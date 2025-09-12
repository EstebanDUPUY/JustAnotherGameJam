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


        if (judge == 100)
        {
            return "FULL COMBO!!\n AMAZING!!";
        }
        else if (judge < 100 && judge >= 95)
        {
            return "So close to Full Combo, but so far...";
        }
        else if (judge < 95 && judge >= 70)
        {
            return "Great Job!";
        }
        else if (judge < 70 && judge >= 60)
        {
            return "I've seen better...";
        }
        else if (judge < 60 && judge >= 50)
        {
            return "Did you even try?";
        }
        else
        {
            return "Life is not Daijobu.";
        }
    }
}
