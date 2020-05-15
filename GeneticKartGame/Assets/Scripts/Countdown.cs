using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameObject countDown;
    public AudioSource getReady;
    public AudioSource goAudio;
    public GameObject lapTimer;
    public GameObject carControllers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countStart());
    }

    IEnumerator countStart(){
        yield return new WaitForSeconds(0.5f);

        countDown.GetComponent<Text>().text = "3";
        getReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);
        countDown.GetComponent<Text>().text = "2";
        getReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);
        countDown.GetComponent<Text>().text = "1";
        getReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);
        goAudio.Play();

        lapTimer.SetActive(true);
        carControllers.SetActive(true);

    }

}