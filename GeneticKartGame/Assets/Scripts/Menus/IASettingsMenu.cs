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
    public Text textPopulation;
    public Text textMaxGenerations;
    public Text textNumElite;
    public Text textBestPercentage;

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
        textPopulation.text = "" + (int)populationSizeSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateMaxGeneration()
    {
        GameManager.iaSettings.maxGenerations = (int)maxGenerationsSlider.value;
        textMaxGenerations.text = "" + (int)maxGenerationsSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateNumElite()
    {
        GameManager.iaSettings.numElite = (int)numEliteSlider.value;
        textNumElite.text = "" + (int)numEliteSlider.value;
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void UpdateBestPercentage()
    {
        GameManager.iaSettings.bestPercentage = bestPercentegeSlider.value;
        textBestPercentage.text = "" + (int)(bestPercentegeSlider.value * 100);
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
    }

    public void RestoreDefaultValues()
    {   
        AudioManager.instance.Play("PulseSound");
        GameManager.iaSettings = new IASettingsData();
        SaveSystem.Save(PathManager.iaSettingsPath, GameManager.iaSettings);
        ShowValues();
    }

    public void RestoreDefaultIA()
    {   
        AudioManager.instance.Play("PulseSound");
        NNetData data;
        SaveSystem.Load(PathManager.defaultNetPath, out data);
        SaveSystem.Save(PathManager.netPath, data);
    }
}
