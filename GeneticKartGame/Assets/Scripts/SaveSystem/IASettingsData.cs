using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IASettingsData
{
    public int populationSize;
    public int maxGenerations;
    public int numElite;
    public float bestPercentage;

    public IASettingsData()
    {
        populationSize = 85;
        maxGenerations = 5;
        numElite = 5;
        bestPercentage = 0.6f;
    }

}
