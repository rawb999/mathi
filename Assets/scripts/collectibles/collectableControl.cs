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
    public static int totalScoreCount = 0; // Lupe here changed this to 11  was 0, for the game over test
    public Game_managerUI game_End; // for refrence to end the game 
    public static bool playerDead = false;
    public GameObject meleeCountDisplay;
    public GameObject tankCountDisplay;
    public GameObject rangedCountDisplay;

    private bool isDead; // to make sure it isn't constantly happening
    void Update()
    {
        scoreCountDisplay.GetComponent<Text>().text = "" + totalScoreCount;
        waveCountDisplay.GetComponent<Text>().text = "" + waveNumber;
        meleeCountDisplay.GetComponent<Text>().text = "" + meleeScoreCount / 10;
        tankCountDisplay.GetComponent<Text>().text = "" + tankScoreCount / 10;
        rangedCountDisplay.GetComponent<Text>().text = "" + rangedScoreCount / 10;


        // THis should end the game - Lupe 
        if (totalScoreCount < 10 && !isDead && waveNumber > 0)
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