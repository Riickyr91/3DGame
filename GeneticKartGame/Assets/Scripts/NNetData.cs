using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

[System.Serializable]
public class NNetData
{
    //public MatrixData inputLayer;

    //public MatrixData[] hiddenLayers;

    //public MatrixData outputLayer;

    public MatrixData[] weightsData;

    public float[] biasesData;

    public float fitnessData;

    public int nLayers;

    public NNetData(NNet net)
    {
        List<Matrix<float>> weights = net.weights;
        nLayers = weights.Count;

        weightsData = new MatrixData[nLayers];

        for(int i = 0; i < nLayers; i++)
        {
            weightsData[i] = new MatrixData(weights[i]);
        }

        biasesData = net.biases.ToArray();
        fitnessData = net.fitness;
    }

    public NNet getNNet()
    {
        NNet net = new NNet();

        // Weights
        List<Matrix<float>> weights = new List<Matrix<float>>();
        for(int i = 0; i < nLayers; i++)
        {
            weights.Add(weightsData[i].GetMatrix());
        }
        net.weights = weights;

        // Biases
        net.biases = new List<float>(biasesData);

        // Fitness
        net.fitness = fitnessData;

        // Input layer
        int nInputs = weights[0].RowCount;
        net.inputLayer = Matrix<float>.Build.Dense(1, nInputs);

        // Output layer
        int nOutputs = weights[nLayers - 1].ColumnCount;
        net.outputLayer = Matrix<float>.Build.Dense(1, nOutputs);

        // Hidden layers
        List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();
        int nNeurons;
        for(int i = 0; i < nLayers-1; i++)
        {
            nNeurons = weights[i].ColumnCount;
            hiddenLayers.Add(Matrix<float>.Build.Dense(1, nNeurons));
        }
        net.hiddenLayers = hiddenLayers;

        return net;
    }
}
