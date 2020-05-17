using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    public static RankingData ranking;
    public static CarSettingsData carSettings;
    public static IASettingsData iaSettings;
    public static int numRounds;

    static GameManager()
    {
        // Load ranking data
        SaveSystem.Load(PathManager.rankingPath, out ranking);
        if (ranking == default(RankingData)) ranking = new RankingData();

        // Load car settings data
        SaveSystem.Load(PathManager.carSettingsPath, out carSettings);
        if (carSettings == default(CarSettingsData)) carSettings = new CarSettingsData();

        // Load ia settings data
        SaveSystem.Load(PathManager.iaSettingsPath, out iaSettings);
        if (iaSettings == default(IASettingsData)) iaSettings = new IASettingsData();

        numRounds = 1;
    }
}
