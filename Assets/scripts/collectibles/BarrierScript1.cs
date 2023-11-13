using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarrierScript1 : MonoBehaviour
{
    public BarrierScript2 barrier2Script; // Reference to the second barrier.

    public BarrierScript3 barrier3Script;

    public bool isCollected = false;

    public TMP_Text barrierText;

    private int number;

    private int variableNumber;

    private int variableNumber2;

    private int variableNumber3;

    public GameObject player;




    void Start()
    {
        //generate a random number for the equation to be used
        number = Random.Range(0, 2);

        if (number == 0)
        {
            variableNumber = Random.Range(0, 80);
            barrierText.text = "x + " + variableNumber.ToString();
        }

        if (number == 1)
        {
            variableNumber = Random.Range(1, 8);
            variableNumber2 = Random.Range(1, 8);
            variableNumber3 = Random.Range(1, 50);
            barrierText.text = "(" + variableNumber.ToString() + " * " + variableNumber2.ToString() + ") + " + variableNumber3.ToString();
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
            barrier2Script.isCollected = true;

            barrier3Script.isCollected = true;

            if (number == 0)
            {
                Random1();
            }

            if (number == 1)
            {
                Random2();
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

        collectableControl.scoreCount += (variableNumber * variableNumber2 + variableNumber3);
    }



}