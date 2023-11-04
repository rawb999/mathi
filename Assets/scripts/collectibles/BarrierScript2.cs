using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarrierScript2 : MonoBehaviour
{
    public BarrierScript1 barrier1Script; // Reference to the first barrier.

    public bool isCollected = false;

    public TMP_Text barrierText;

    private int number;

    private int variableNumber;

    private int variableNumber2;

    public GameObject player;


    void Start()
    {
        //generate a random number for the equation to be used
        number = Random.Range(0, 3);

        if (number == 0)
        {
            variableNumber = Random.Range(0, 10);
            barrierText.text = "x + " + variableNumber.ToString();
        }

        if (number == 1)
        {
            variableNumber = Random.Range(2, 4);
            variableNumber2 = Random.Range(1, 15);
            barrierText.text = "x * " + variableNumber.ToString() + " - " + variableNumber2.ToString();
        }

        if (number == 2)
        {
            variableNumber = Random.Range(2, 4);
            variableNumber2 = Random.Range(1, 15);
            barrierText.text = "x / " + variableNumber.ToString() + " + " + variableNumber2.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected)
        {
            // Disable this barrier.
            this.gameObject.SetActive(false);

            // Disable the other barrier.
            // Access the BarrierScript1 component on barrier1 and set isCollected to false.
            barrier1Script.isCollected = true;

            if (number == 0)
            {
                Random1();
            }

            if (number == 1)
            {
                Random2();
            }

            if (number == 2)
            {
                Random3();
            }

        }
    }

    private void Random1()
    {
        // Update the score.

        collectableControl.scoreCount += variableNumber;
    }

    private void Random2()
    {
        // Update the score.

        collectableControl.scoreCount *= variableNumber;
        collectableControl.scoreCount -= variableNumber2;
    }

    private void Random3()
    {
        // Update the score.

        collectableControl.scoreCount /= variableNumber;
        collectableControl.scoreCount += variableNumber2;
    }

}