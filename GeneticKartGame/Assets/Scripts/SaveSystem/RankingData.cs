using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class RankingData 
{
    public float[] rankingTimes;
    public string[] rankingNames;
    public string[] rankingCars;

    private int rankingSize = 10;

    public RankingData()
    {
        rankingTimes = new float[rankingSize];
        rankingNames = new string[rankingSize];
        rankingCars = new string[rankingSize];
        for (int i = 0; i < rankingTimes.Length; i++)
        {
            rankingTimes[i] = -1;
            rankingNames[i] = "None";
            rankingCars[i] = "Cars";
        }
    }

    public void UpdateRanking(float time, string name, string car)
    {
        // Search for the first bigger
        int index = 0;
        while (index < rankingSize && rankingTimes[index] <= time)
        {
            index++;
        }


        // Cast ranking arrays to lists
        List<float> arrayTimes = new List<float>(rankingTimes);
        List<string> arrayNames = new List<string>(rankingNames);
        List<string> arrayCars = new List<string>(rankingCars);


        // Insert the new element
        if(index < rankingSize)
        {
            arrayTimes.Insert(index, time/10);
            arrayNames.Insert(index, name);
            arrayCars.Insert(index, car);
        }


        // Cast list to arrays
        for(int i = 0; i < rankingSize; i++)
        {
            rankingTimes[i] = arrayTimes[i];
            rankingNames[i] = arrayNames[i];
            rankingCars[i] = arrayCars[i];
        }

        // Save ranking update
        SaveSystem.Save(Application.persistentDataPath + "/ranking.dat", this);
    }
}
