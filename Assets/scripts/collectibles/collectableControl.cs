using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectableControl : MonoBehaviour
{
    public static int scoreCount = 2000;
    public GameObject scoreCountDisplay;
    public static int waveNumber = 0;
    public GameObject waveCountDisplay;

    void Update()
    {
        scoreCountDisplay.GetComponent<Text>().text = "" + scoreCount;
        waveCountDisplay.GetComponent<Text>().text = "" + waveNumber;
    }

    void start()
    {
        waveNumber = 0;
    }
}