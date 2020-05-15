﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;
    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;
    public GameObject lapCounter;
    public GameObject raceFinish;
    public GameObject gameManager;

    int lapsDone = 0;

    void OnTriggerEnter() {
        lapsDone += 1;
        if((MilliDisplay.GetComponent<Text>().text == "00" && MinuteDisplay.GetComponent<Text>().text == "00" && SecondDisplay.GetComponent<Text>().text == "00")
        || (int.Parse(MinuteDisplay.GetComponent<Text>().text) > LapTimeManager.MinuteCount) || 
           (int.Parse(MinuteDisplay.GetComponent<Text>().text) == LapTimeManager.MinuteCount && int.Parse(SecondDisplay.GetComponent<Text>().text) > LapTimeManager.SecondCount) ||
           (int.Parse(MinuteDisplay.GetComponent<Text>().text) == LapTimeManager.MinuteCount && int.Parse(SecondDisplay.GetComponent<Text>().text) == LapTimeManager.SecondCount && 
            float.Parse(MilliDisplay.GetComponent<Text>().text) > LapTimeManager.MilliCount)){

            if(LapTimeManager.MinuteCount <= 9){
                MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount;
            }         
            else{
                MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount;
            }
            if(LapTimeManager.SecondCount <= 9){
                SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount;
            }         
            else{
                SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount;
            }       

            MilliDisplay.GetComponent<Text>().text = "" + LapTimeManager.MilliCount;
        }
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0;

        lapCounter.GetComponent<Text>().text = "" + lapsDone;

        if (lapsDone ==  gameManager.GetComponent<LPPV_GameManager>().lapsRequirement + 1){
            raceFinish.SetActive(true);
        }

        HalfLapTrig.SetActive(true);

        LapCompleteTrig.SetActive(false);
    }
}
