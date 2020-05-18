using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationMenu : MonoBehaviour
{
    public Text text;
    public GeneticManager geneticManager;

    // Update is called once per frame
    void Update()
    {
        text.text = "Generation = " + geneticManager.currentGeneration + "/" + geneticManager.maxGeneration + "\n" +
                    "Genome = " + geneticManager.currentGenome + "/" + geneticManager.populationSize + "\n" +
                    "Elite list = " + geneticManager.eliteListSize + "\n" +
                    "Best percentage = " + geneticManager.bestPercentage;
    }

    public void RestoreTimeScale()
    {
        Time.timeScale = 1;
    }
}
