using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Finish : MonoBehaviour
{
    public GameObject sedanCarCam;
    public GameObject busCarCam;
    public GameObject sportCarCam;
    public GameObject utilityCarCam;

    public GameObject sedanCar;
    public GameObject busCar;
    public GameObject sportCar;
    public GameObject utilityCar;

    public GameObject sedanFinishCam;
    public GameObject busFinishCam;
    public GameObject sportFinishCam;
    public GameObject utilityFinishCam;

    public GameObject levelMusic;

    void OnTriggerEnter() {
        sedanCar.GetComponent<AudioSource>().Stop();
        busCar.GetComponent<AudioSource>().Stop();
        sportCar.GetComponent<AudioSource>().Stop();
        utilityCar.GetComponent<AudioSource>().Stop();
        sedanCarCam.SetActive(false);
        sedanFinishCam.SetActive(true);
        busCarCam.SetActive(false);
        busFinishCam.SetActive(true);  
        sportCarCam.SetActive(false);
        sportFinishCam.SetActive(true);        
        utilityCarCam.SetActive(false);
        utilityFinishCam.SetActive(true);

        levelMusic.GetComponent<AudioSource>().Stop();

        sedanFinishCam.GetComponent<AudioSource>().Play();
        busFinishCam.GetComponent<AudioSource>().Play();
        sportFinishCam.GetComponent<AudioSource>().Play();
        utilityFinishCam.GetComponent<AudioSource>().Play();

    }

}
