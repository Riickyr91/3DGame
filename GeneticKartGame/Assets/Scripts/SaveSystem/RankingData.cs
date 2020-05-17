using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class RankingData 
{
    public float[] rankingTimes;
    public string[] rankingNames;

    private int rankingSize = 10;

    public RankingData()
    {
        rankingTimes = new float[rankingSize];
        rankingNames = new string[rankingSize];
        for (int i = 0; i < rankingTimes.Length; i++)
        {
            rankingTimes[i] = -1;
            rankingNames[i] = "None";
        }
    }

    public void UpdateRanking(float time, string name)
    {
        // Search for the first bigger
        int index = rankingSize - 1;
        while (index >= 0 && rankingTimes[index] <= time)
        {
            index--;
        }


        // Cast ranking arrays to lists
        List<float> arrayTimes = new List<float>(rankingTimes);
        List<string> arrayNames = new List<string>(rankingNames);


        // Insert the new element
        index++;
        if(index < rankingSize)
        {
            arrayTimes.Insert(index, time);
            arrayNames.Insert(index, name);
        }


        // Cast list to arrays
        for(int i = 0; i < rankingSize; i++)
        {
            rankingTimes[i] = arrayTimes[i];
            rankingNames[i] = arrayNames[i];
        }
    }
}
