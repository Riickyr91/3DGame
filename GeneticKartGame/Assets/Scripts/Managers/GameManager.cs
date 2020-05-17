using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    public static RankingData ranking;

    static GameManager()
    {
        // Load ranking data
        SaveSystem.Load(Application.persistentDataPath + "/ranking.dat", out ranking);
        if (ranking == default(RankingData)) ranking = new RankingData();

    }
}
