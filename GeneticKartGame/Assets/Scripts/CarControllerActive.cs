using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerActive : MonoBehaviour
{
    public GameObject carControl1;
    public GameObject carControl2;
    public GameObject carControl3;
    public GameObject carControl4;

    // Start is called before the first frame update
    void Start()
    {
        carControl1.GetComponent<LPPV_CarController>().enabled = true;
        carControl2.GetComponent<LPPV_CarController>().enabled = true;
        carControl3.GetComponent<LPPV_CarController>().enabled = true;
        carControl4.GetComponent<LPPV_CarController>().enabled = true;

    }


}
