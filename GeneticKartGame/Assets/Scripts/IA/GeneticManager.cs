using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MathNet.Numerics.LinearAlgebra;

public class GeneticManager : MonoBehaviour
{
    [Header("References")]
    public CarController controller;

    [Header("Public View")]
    public int populationSize = 85;
    public int maxGeneration = 5;
    public int eliteListSize = 5;
    public float bestPercentage = 0.4f;

    public int currentGeneration = 0;
    public int currentGenome = 0;
    public float alpha = 0.5f;

    private List<NNet> population;
    private bool isSimulationFinish = false;
    public bool playBestCar = false;

    private void Start()
    {
        populationSize = GameManager.iaSettings.populationSize;
        maxGeneration = GameManager.iaSettings.maxGenerations;
        eliteListSize = GameManager.iaSettings.numElite;
        bestPercentage = GameManager.iaSettings.bestPercentage;

        if (playBestCar)
        {
            LoadBestGenome();
        } else
        {
            FillPopulationWithRandoms();
        }

    }

    public void SaveBestGenome()
    {   
        AudioManager.instance.Play("PulseSound");
        SortPopulation();
        population[0].save(Application.persistentDataPath + "/net.dat");
    }

    public void LoadBestGenome()
    {
        AudioManager.instance.Play("PulseSound");
        NNetData data;
        SaveSystem.Load(Application.persistentDataPath + "/net.dat", out data);
        controller.ResetWithNetwork(data.getNNet());
        controller.SetStop(false);
    }

    private void FillPopulationWithRandoms()
    {

        population = new List<NNet>();
        NNet net;

        while(population.Count < populationSize)
        {
            net = new NNet();
            net.Initialise(controller.LAYERS, controller.NEURONS);
            population.Add(net);
        }

        ResetToCurrentGenome();
    }

    private void ResetToCurrentGenome()
    {
        controller.ResetWithNetwork(population[currentGenome]);
    }

    public void Death(float fitness, NNet network)
    {
        if (!isSimulationFinish) {
            if (currentGenome < population.Count - 1)
            {
                population[currentGenome].fitness = fitness;
                currentGenome++;
                ResetToCurrentGenome();
            }
            else
            {
                currentGeneration++;
                if (currentGeneration == maxGeneration)
                {
                    isSimulationFinish = true;
                    controller.SetStop(true);

                }
                else
                {
                    RePopulate();
                }
            }
        }
    }

    private void RePopulate()
    {
        NNet fatherA;
        NNet fatherB;

        List<NNet> newPopulation = CreateEliteList(population, eliteListSize);

        while(newPopulation.Count < populationSize)
        {
            (fatherA, fatherB) = SelectParents(population);

            newPopulation.Add(CrossBLX(fatherA, fatherB));
        }

        population = newPopulation;
        currentGenome = 0;

        ResetToCurrentGenome();
    }

    private List<NNet> CreateEliteList(List<NNet> population, int numElite)
    {
        // Initialize output
        List<NNet> eliteList = new List<NNet>();

        // Sort descending
        SortPopulation();

        // Add elements
        for(int i = 0; i < numElite; i++)
        {
            eliteList.Add(population[i]);
        }

        return eliteList;
    }

    private (NNet, NNet) SelectParents(List<NNet> population)
    {
        int randomPos1 = (int)Random.Range(0.0f, bestPercentage * population.Count);
        int randomPos2 = (int)Random.Range(0.0f, bestPercentage * population.Count);

        return (population[randomPos1], population[randomPos2]);
    }
    

    private NNet CrossBLX(NNet fatherA, NNet fatherB)
    {
        List<Matrix<float>> weightsA = fatherA.weights;
        Matrix<float> biasesA = ArrayToMatrix(fatherA.biases.ToArray());

        List<Matrix<float>> weightsB = fatherB.weights;
        Matrix<float> biasesB = ArrayToMatrix(fatherB.biases.ToArray());

        List<Matrix<float>> childWeigths = new List<Matrix<float>>();
        Matrix<float> childBiases;

        for(int i = 0; i < weightsA.Count; i++)
        {
            childWeigths.Add(CrossBLX(weightsA[i], weightsB[i]));
        }

        childBiases = CrossBLX(biasesA, biasesB);

        
        NNet child = new NNet();
        child.Initialise(controller.LAYERS, controller.NEURONS);
        child.weights = childWeigths;
        child.biases = new List<float>(RowMatrixToArray(childBiases));

        return child;
    }


    private Matrix<float> CrossBLX(Matrix<float> matrixA, Matrix<float> matrixB)
    {
        Matrix<float> matrix = Matrix<float>.Build.Dense(matrixA.RowCount, matrixA.ColumnCount);

        for(int i = 0; i < matrixA.RowCount; i++)
        {
            for(int j = 0; j < matrixA.ColumnCount; j++)
            {
                matrix[i, j] = CrossBLX(matrixA[i, j], matrixB[i, j]);
            }
        }

        return matrix;
    }

    private float CrossBLX(float valueA, float valueB)
    {
        float minValue;
        float maxValue;

        if(valueA < valueB)
        {
            minValue = valueA;
            maxValue = valueB;
        } else
        {
            minValue = valueB;
            maxValue = valueA;
        }

        float difference = maxValue - minValue;

        float newValue = Random.Range(minValue - alpha * difference, maxValue + alpha * difference);

        if (newValue < -1) newValue = -1;
        if (newValue > 1) newValue = 1;

        return newValue;
    }

    private Matrix<float> ArrayToMatrix(float[] array)
    {
        Matrix<float> matrix = Matrix<float>.Build.Dense(1, array.Length);

        for(int i = 0; i < array.Length; i++)
        {
            matrix[0, i] = array[i];
        }

        return matrix;
    }

    private float[] RowMatrixToArray(Matrix<float> matrix)
    {
        float[] array = new float[matrix.ColumnCount];
        for(int i = 0; i < matrix.ColumnCount; i++)
        {
            array[i] = matrix[0, i];
        }

        return array;
    }

    
    private void SortPopulation()
    {
        // Sort ascending
        population.Sort((p1, p2) => (p1.fitness.CompareTo(p2.fitness)));

        // Sort descending
        population.Reverse();

    }
}
