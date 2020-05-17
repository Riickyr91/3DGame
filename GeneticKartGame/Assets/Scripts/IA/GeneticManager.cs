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
        //population = new NNet[initialPopulation];
        //FillPopulationWithRandomValues(population, 0);

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

    //private void FillPopulationWithRandomValues(NNet[] newPopulation, int startingIndex)
    //{
    //    while(startingIndex < populationSize)
    //    {
    //        newPopulation[startingIndex] = new NNet();
    //        newPopulation[startingIndex].Initialise(controller.LAYERS, controller.NEURONS);
    //        startingIndex++;
    //    }
    //}

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
                    //Debug.Log("Simulation finish");
                    isSimulationFinish = true;
                    controller.SetStop(true);
                    //SaveBestGenome();
                    //LoadBestGenome();
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
        //genePool.Clear();
        ////currentGeneration++;
        //naturallySelected = 0;
        //SortPopulation();

        //NNet[] newPopulation = PickBestPopulation();

        NNet fatherA;
        NNet fatherB;

        List<NNet> newPopulation = CreateEliteList(population, eliteListSize);

        while(newPopulation.Count < populationSize)
        {
            (fatherA, fatherB) = SelectParents(population);

            newPopulation.Add(CrossBLX(fatherA, fatherB));
        }


        //Crossover(newPopulation);
        //Mutate(newPopulation);
        //FillPopulationWithRandomValues(newPopulation, naturallySelected);

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

    //private void Mutate(NNet[] newPopulation)
    //{
    //    for(int i = 0; i < naturallySelected; i++)
    //    {
    //        for(int c = 0; c < newPopulation[i].weights.Count; c++)
    //        {
    //            if(Random.Range(0.0f, 1.0f) < mutationRate)
    //            {
    //                newPopulation[i].weights[c] = MutateMatrix(newPopulation[i].weights[c]);
    //            }
    //        }
    //    }
    //}

    //private Matrix<float> MutateMatrix(Matrix<float> A)
    //{
    //    int randomPoints = Random.Range(1, (A.RowCount * A.ColumnCount) / 7);

    //    Matrix<float> C = A;

    //    for(int i = 0; i < randomPoints; i++)
    //    {
    //        int randomColumn = Random.Range(0, C.ColumnCount);
    //        int randomRow = Random.Range(0, C.RowCount);

    //        C[randomRow, randomColumn] = Mathf.Clamp(C[randomRow, randomColumn] + Random.Range(-1f, 1f), -1f, 1f);
    //    }

    //    return C;
    //}

    //private void Crossover(NNet[] newPopulation)
    //{
    //    for(int i = 0; i < numberToCrossover; i += 2)
    //    {
    //        int AIndex = i;
    //        int BIndex = i + 1;

    //        if(genePool.Count >= 1)
    //        {
    //            for(int l = 0; l < 100; l++)
    //            {
    //                AIndex = genePool[Random.Range(0, genePool.Count)];
    //                BIndex = genePool[Random.Range(0, genePool.Count)];

    //                if (AIndex != BIndex)
    //                    break;
    //            }
    //        }

    //        NNet Child1 = new NNet();
    //        NNet Child2 = new NNet();

    //        Child1.Initialise(controller.LAYERS, controller.NEURONS);
    //        Child2.Initialise(controller.LAYERS, controller.NEURONS);

    //        Child1.fitness = 0;
    //        Child2.fitness = 0;

    //        for(int w = 0; w < Child1.weights.Count; w++)
    //        {
    //            if(Random.Range(0.0f, 1.0f) < 0.5f)
    //            {
    //                Child1.weights[w] = population[AIndex].weights[w];
    //                Child2.weights[w] = population[BIndex].weights[w];
    //            } else
    //            {
    //                Child2.weights[w] = population[AIndex].weights[w];
    //                Child1.weights[w] = population[BIndex].weights[w];
    //            }
    //        }

    //        for (int w = 0; w < Child1.biases.Count; w++)
    //        {
    //            if (Random.Range(0.0f, 1.0f) < 0.5f)
    //            {
    //                Child1.biases[w] = population[AIndex].biases[w];
    //                Child2.biases[w] = population[BIndex].biases[w];
    //            }
    //            else
    //            {
    //                Child2.biases[w] = population[AIndex].biases[w];
    //                Child1.biases[w] = population[BIndex].biases[w];
    //            }
    //        }

    //        newPopulation[naturallySelected] = Child1;
    //        naturallySelected++;

    //        newPopulation[naturallySelected] = Child2;
    //        naturallySelected++;
    //    }
    //}

    //private NNet[] PickBestPopulation()
    //{
    //    NNet[] newPopulation = new NNet[populationSize];

    //    for(int i = 0; i < bestAgentSelection; i++)
    //    {
    //        newPopulation[naturallySelected] = population[i].InitialiseCopy(controller.LAYERS, controller.NEURONS);
    //        newPopulation[naturallySelected].fitness = 0;
    //        naturallySelected++;

    //        // The number of times that a gene is in the pool is related with his fitness
    //        int f = Mathf.RoundToInt(population[i].fitness * 10);

    //        // Add the reference of the gene
    //        for(int c = 0; c < f+1; c++)
    //        {
    //            genePool.Add(i);
    //        }


    //    }

    //    for(int i = 0; i < worstAgentSelection; i++)
    //    {
    //        int last = population.Length - 1;
    //        last -= i;

    //        // The number of times that a gene is in the pool is related with his fitness
    //        int f = Mathf.RoundToInt(population[last].fitness * 10);

    //        // Add the reference of the gene
    //        for (int c = 0; c < f + 1; c++)
    //        {
    //            genePool.Add(last);
    //        }
    //    }

    //    return newPopulation;

    //}

    private void SortPopulation()
    {
        // Sort ascending
        population.Sort((p1, p2) => (p1.fitness.CompareTo(p2.fitness)));

        // Sort descending
        population.Reverse();


        //for(int i = 0; i < population.Length; i++)
        //{
        //    for(int j = i; j < population.Length; j++)
        //    {
        //        if(population[i].fitness < population[j].fitness)
        //        {
        //            NNet temp = population[i];
        //            population[i] = population[j];
        //            population[j] = temp;
        //        }
        //    }
        //}
    }
}
