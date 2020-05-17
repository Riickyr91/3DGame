using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RankingMenu : MonoBehaviour
{
    public Text rankingText;

    private void Start()
    {
        ShowRanking();
    }

    private void ShowRanking()
    {
        rankingText.text = "";

        RankingData ranking = new RankingData();

        float[] rankingTimes = ranking.rankingTimes;
        string[] rankingNames = ranking.rankingNames;

        string text = "";
        for (int i = 0; i < rankingTimes.Length; i++)
        {
            //if (rankingTimes[i] >= 0)
            //{
                text = "";
                text += i + 1 + ".  " + rankingNames[i] + "  ";

                int numPoints = 30 - text.Length;
                for (int j = 0; j < numPoints; j++)
                {
                    text += ". ";
                }
                text += rankingTimes[i] + "\n";

                rankingText.text += text;
            //}
        }
    }


    public void ResetRanking()
    {
        string path = Application.persistentDataPath + "/ranking.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        GameManager.ranking = new RankingData();

        ShowRanking();
    }
}
