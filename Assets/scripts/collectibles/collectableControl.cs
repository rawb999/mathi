using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectableControl : MonoBehaviour
{
    public static int scoreCount = 200;
    public GameObject scoreCountDisplay;

    void Update()
    {
        scoreCountDisplay.GetComponent<Text>().text = "" + scoreCount;
    }
}