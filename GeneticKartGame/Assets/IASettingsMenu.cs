using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IASettingsMenu : MonoBehaviour
{
    public Slider populationSizeSlider;
    public Slider maxGenerationsSlider;
    public Slider numEliteSlider;
    public Slider bestPercentegeSlider;

    public void Start()
    {
        ShowValues();
    }

    public void ShowValues()
    {
        populationSizeSlider.value = GameManager.iaSettings.populationSize;
        maxGenerationsSlider.value = GameManager.iaSettings.maxGenerations;
        numEliteSlider.value = GameManager.iaSettings.numElite;
        bestPercentegeSlider.value = GameManager.iaSettings.bestPercentage;
    }

    public void UpdatePopulation()
    {
        GameManager.iaSettings.populationSize = (int)populationSizeSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateMaxGeneration()
    {
        GameManager.iaSettings.maxGenerations = (int)maxGenerationsSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateNumElite()
    {
        GameManager.iaSettings.numElite = (int)numEliteSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateBestPercentage()
    {
        GameManager.iaSettings.bestPercentage = bestPercentegeSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void RestoreDefaultValues()
    {
        GameManager.iaSettings = new IASettingsData();
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
        ShowValues();
    }
}
