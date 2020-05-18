using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameisPaused = false;

    public GameObject puaseMenuUI;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (gameisPaused){
                resume();
            } else{
                pauseMenu();
            }
        }
    }

    public void resume(){
        puaseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameisPaused = false;
    }

    void pauseMenu(){
        puaseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameisPaused = true;
    }

    public void setMenuPauseFalse(){
        Time.timeScale = 1f;
    }

    public void restart(){
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

}
