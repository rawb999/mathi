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
    private int variableNumber4;
    public string type = "melee";

    public GameObject player;




    void Start()
    {
        //generate a random number for the equation to be used
        number = Random.Range(0, 3);

        if (number == 0)
        {
            variableNumber = Random.Range(50, 251);
            variableNumber2 = Random.Range(50, 251);
            barrierText.text = variableNumber.ToString() + " + " + variableNumber2.ToString();
        }

        if (number == 1)
        {
            variableNumber = Random.Range(1, 51);
            variableNumber2 = Random.Range(2, 9);
            variableNumber3 = Random.Range(5, 26);
            barrierText.text = "(" + variableNumber.ToString() + " * " + variableNumber2.ToString() + ") + " + variableNumber3.ToString();
        }

        if (number == 2)
        {
            variableNumber = Random.Range(5, 16);
            variableNumber2 = Random.Range(1, 4);
            variableNumber3 = Random.Range(5, 11);
            variableNumber4 = Random.Range(6, 15);
            barrierText.text = "(" + variableNumber.ToString() + " * " + variableNumber2.ToString() + ") + (" + variableNumber3.ToString() + " * " + variableNumber4.ToString() + ")";
        }
       //(x / y) + z
       //(sqrt(x)^y)
       //(x + y + z)
       //((x - y) * z)
       //x^2 + y^2

    

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

            if (number == 2)
            {
                Random3();
            }

        }
    }

    private void Random1()
    {
        // Update the score.
        int added = variableNumber + variableNumber2;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.meleeScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "melee";
    }

    private void Random2()
    {
        // Update the score.
        int added = variableNumber * variableNumber2 + variableNumber3;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.meleeScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "melee";

    }

    private void Random3()
    {
        int added = (variableNumber * variableNumber2) + (variableNumber3 * variableNumber4);
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.meleeScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "melee";
    }



}