using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RankingMenu : MonoBehaviour
{
    public Text rankingText;

    private float minute;
    private float second;
    private float milli;
    private float aux;
    private void Start()
    {
        ShowRanking();
    }

    private void ShowRanking()
    {
        rankingText.text = "";

        RankingData ranking = GameManager.ranking;

        float[] rankingTimes = ranking.rankingTimes;
        string[] rankingNames = ranking.rankingNames;
        string[] rankingCars = ranking.rankingCars;

        string text = "";
        for (int i = 0; i < rankingTimes.Length; i++)
        {
            if (rankingTimes[i] != float.PositiveInfinity)
            {
                text = "";
                text += i + 1 + ".  " + rankingNames[i] + " with " + rankingCars[i];

                int numPoints = 30 - text.Length;
                for (int j = 0; j < numPoints; j++)
                {
                    text += ". ";
                }

                milli = Mathf.Floor(rankingTimes[i]) % 10;
                aux = Mathf.Floor(rankingTimes[i] / 10);
                second = Mathf.Floor(aux % 60);
                minute  = Mathf.Floor(aux / 60);
                
                text += " " + minute + ":" + second + "." + milli + "\n";

                rankingText.text += text;
            }
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
        AudioManager.instance.Play("PulseSound");
        ShowRanking();
    }
}
