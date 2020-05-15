using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MathNet.Numerics.LinearAlgebra;

public class NeuronalTest : MonoBehaviour
{
    public Text inputText;
    public Text outputText;

    public int nLayers = 2;
    public int nNeurons = 3;

    private NNet net;
 
    public void CreateNet()
    {
        net = new NNet();
        net.Initialise(nLayers, nNeurons);
        PrintWeights(net, inputText);
    }

    private void PrintWeights(NNet net, Text uiText)
    {
        List<Matrix<float>> weights = net.weights;
        MatrixData matrixData;
        string text = "";

        Vector<float> array;

        text += " *** WEIGHTS ***\n";
        for (int i = 0; i < weights.Count; i++)
        {
            matrixData = new MatrixData(weights[i]);
            text += matrixData.ToString() + "\n\n";
        }


        text += " *** BIASES ***";
        text += "[";
        for(int i = 0; i < net.biases.Count; i++)
        {
            text += net.biases[i].ToString();
            if (i < net.biases.Count - 1) text += ", ";
        }
        text += "]\n\n";

        int nRows, nColumns;
        text += " *** DIMENSIONS ***";

        nRows = net.inputLayer.RowCount;
        nColumns = net.inputLayer.ColumnCount;
        text += "Size input layer = " + "(" + nRows + ", " + nColumns + ")\n";

        text += "Size hidden layers = ";
        for(int i = 0; i < net.hiddenLayers.Count; i++)
        {
            nRows = net.hiddenLayers[i].RowCount;
            nColumns = net.hiddenLayers[i].ColumnCount;
            text += "(" + nRows + ", " + nColumns + ")";
        }
        text += "\n";

        nRows = net.outputLayer.RowCount;
        nColumns = net.outputLayer.ColumnCount;
        text += "Size output layer = " + "(" + nRows + ", " + nColumns + ")\n"; ;

        uiText.text = text;
    }

    public void Save()
    {
        NNetData data = new NNetData(net);
        SaveSystem.Save(Application.persistentDataPath + "/net.dat", data);
        Debug.Log("Saved successfully!");
    }

    public void Load()
    {
        NNetData data;
        SaveSystem.Load(Application.persistentDataPath + "/net.dat", out data);
        PrintWeights(data.getNNet(), outputText);
    }
}
