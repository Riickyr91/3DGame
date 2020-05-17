using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGroup : MonoBehaviour
{
    public string carName;
    public Slider acSlider;
    public Slider maxVelSlider;
    public Slider drivingSlider;
    public InputField newNumRounds;

    public void Start()
    {
        ShowSliderValues();
    }

    public void ShowSliderValues()
    {
        acSlider.value = GameManager.carSettings.data[carName][0];
        maxVelSlider.value = GameManager.carSettings.data[carName][1];
        drivingSlider.value = GameManager.carSettings.data[carName][2];
    }


    public void UpdateAcceleration()
    {
        GameManager.carSettings.data[carName][0] = acSlider.value;
        SaveSystem.Save(PathManager.carSettingsPath, GameManager.carSettings);
    }

    public void UpdateMaxVel()
    {
        GameManager.carSettings.data[carName][1] = maxVelSlider.value;
        SaveSystem.Save(PathManager.carSettingsPath, GameManager.carSettings);
    }

    public void UpdateDriving()
    {
        GameManager.carSettings.data[carName][2] = drivingSlider.value;
        SaveSystem.Save(PathManager.carSettingsPath, GameManager.carSettings);
    }

    public void updateNumRounds(){
        GameManager.numRounds = int.Parse(newNumRounds.text);
    }
}
