using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public Slider timeSlider;

    public void timeUpdate()
    {
        Time.timeScale = timeSlider.value;
    }
}
