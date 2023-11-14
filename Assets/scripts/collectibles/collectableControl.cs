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
    public static int totalScoreCount = 0;

    void Update()
    {
        scoreCountDisplay.GetComponent<Text>().text = "" + totalScoreCount;
        waveCountDisplay.GetComponent<Text>().text = "" + waveNumber;
    }

    void start()
    {
        waveNumber = 0;
    }
}