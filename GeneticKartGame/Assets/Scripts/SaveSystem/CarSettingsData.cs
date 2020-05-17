using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarSettingsData
{
    public Dictionary<string, float[]> data;

    public CarSettingsData()
    {
        data = new Dictionary<string, float[]>();
        float[] values; // 0 = Acceleration, 1 = Max velocity, 2 = Driving

        //Sedan values
        values = new float[3];
        values[0] = 2500;
        values[1] = 15;
        values[2] = 4;
        data.Add("Sedan", values);

        //Sports values
        values = new float[3];
        values[0] = 25000;
        values[1] = 20;
        values[2] = 6;
        data.Add("Sports", values);

        //Utility values
        values = new float[3];
        values[0] = 6000;
        values[1] = 17;
        values[2] = 5;
        data.Add("Utility", values);

        //Bus values
        values = new float[3];
        values[0] = 1000;
        values[1] = 15;
        values[2] = 5;
        data.Add("Bus", values);
    }
}
