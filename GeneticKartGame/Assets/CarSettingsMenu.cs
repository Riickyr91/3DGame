using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSettingsMenu : MonoBehaviour
{
    public SliderGroup sedanGroup;
    public SliderGroup sportsGroup;
    public SliderGroup utilityGroup;
    public SliderGroup busGroup;

    public void RestoreDefaultValues()
    {
        GameManager.carSettings = new CarSettingsData();
        SaveSystem.Save(PathManager.carSettingsPath, GameManager.carSettings);

        sedanGroup.ShowSliderValues();
        sportsGroup.ShowSliderValues();
        utilityGroup.ShowSliderValues();
        busGroup.ShowSliderValues();
    }
}
