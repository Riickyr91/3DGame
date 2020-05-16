using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOptions : MonoBehaviour
{
    public void playGame(){
        SceneManager.LoadScene(1);
        AudioManager.instance.Play("PulseSound");
    }

    public void settings(){
        SceneManager.LoadScene(3);
        AudioManager.instance.Play("PulseSound");
    }

    public void ranking(){
        SceneManager.LoadScene(4);
        AudioManager.instance.Play("PulseSound");
    }

    public void iaTraning(){
        SceneManager.LoadScene(5);
        AudioManager.instance.Play("PulseSound");
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
        AudioManager.instance.Play("PulseSound");
    }

    public void pulseSound(){
        AudioManager.instance.Play("PulseSound");
    }

    public void quit(){
        Application.Quit();
    }
}
