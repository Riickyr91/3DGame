using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class RankingData 
{
    public int[] rankingPoints;
    public string[] rankingNames;

    private int rankingSize = 10;

    public RankingData()
    {
        rankingPoints = new int[rankingSize];
        rankingNames = new string[rankingSize];
        for (int i = 0; i < rankingPoints.Length; i++)
        {
            rankingPoints[i] = -1;
            rankingNames[i] = "None";
        }
    }

    public void UpdateRanking(int points, string name)
    {
        // Search for the first bigger
        int index = rankingSize - 1;
        while (index >= 0 && rankingPoints[index] <= points)
        {
            index--;
        }


        // Cast ranking arrays to lists
        List<int> arrayPoints = new List<int>();
        List<string> arrayNames = new List<string>();

        for(int i = 0; i < rankingSize; i++)
        {
            arrayPoints.Add(rankingPoints[i]);
            arrayNames.Add(rankingNames[i]);
        }



        // Insert the new element
        index++;
        if(index < rankingSize)
        {
            arrayPoints.Insert(index, points);
            arrayNames.Insert(index, name);
        }


        // Cast list to arrays
        for(int i = 0; i < rankingSize; i++)
        {
            rankingPoints[i] = arrayPoints[i];
            rankingNames[i] = arrayNames[i];
        }
    }
}
