using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectableControl : MonoBehaviour
{
    public static int rangedScoreCount = 0;
    public static int tankScoreCount = 0;
    public static int meleeScoreCount = 0;
    public GameObject scoreCountDisplay;
    public static int waveNumber = 0;
    public GameObject waveCountDisplay;
    public static int totalScoreCount = 10; // Lupe here changed this to 11  was 0, for the game over test
    public Game_managerUI game_End; // for refrence to end the game 
    public static bool playerDead = false;

    private bool isDead; // to make sure it isn't constantly happening
    void Update()
    {
        scoreCountDisplay.GetComponent<Text>().text = "" + totalScoreCount;
        waveCountDisplay.GetComponent<Text>().text = "" + waveNumber;


        // THis should end the game - Lupe 
        if (totalScoreCount < 10 && !isDead)
        {
            playerDead = true;
            game_End.gameOver();
            
        }
    }

    void start()
    {
        // testing for bugs
      //  waveNumber = 0;
    }
}