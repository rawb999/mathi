using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    //game object name pause menu
    [SerializeField] GameObject pauseMenu;



    //Pasue Menu Options
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        SceneManager.LoadScene("Start_Menu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        ArmyLogic.currentZombs = 1;
        ArmyLogic.currentTankZombs = 0;
        ArmyLogic.currentMeleeZombs = 1;
        ArmyLogic.currentRangedZombs = 0;
        ArmyLogic.recentTotalScore = 10;
        ArmyLogic.updatedTotalScore = 10;
        collectableControl.waveNumber = 0;


    }
    public void quit()
    {
        Application.Quit();
        
    }
}
