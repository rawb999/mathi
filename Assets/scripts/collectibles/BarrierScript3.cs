using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarrierScript3 : MonoBehaviour
{
    public BarrierScript1 barrier1Script;

    public BarrierScript2 barrier2Script;

    public bool isCollected = false;

    public TMP_Text barrierText;

    private int number;

    private int variableNumber;

    private int variableNumber2;

    public GameObject player;

    private int variableNumber3;
    private int variableNumber4;
    public string type = "ranger";

    void Start()
    {
        //generate a random number for the equation to be used
        number = Random.Range(0, 7);

        if (number == 0)
        {
            variableNumber = Random.Range(10, 151);
            variableNumber2 = Random.Range(10, 151);
            barrierText.text = variableNumber.ToString() + " + " + variableNumber2.ToString();
        }

        if (number == 1)
        {
            variableNumber = Random.Range(2, 11);
            variableNumber2 = Random.Range(4, 11);
            variableNumber3 = Random.Range(20, 61);
            barrierText.text = "(" + variableNumber.ToString() + " * " + variableNumber2.ToString() + ") + " + variableNumber3.ToString();

        }

        if (number == 2)
        {
            variableNumber = Random.Range(3, 9);
            variableNumber2 = Random.Range(3, 9);
            variableNumber3 = Random.Range(3, 9);
            variableNumber4 = Random.Range(3, 9);
            barrierText.text = "(" + variableNumber.ToString() + " * " + variableNumber2.ToString() + ") + (" + variableNumber3.ToString() + " * " + variableNumber4.ToString() + ")";
        }

        if (number == 3)
        {
            variableNumber = Random.Range(160, 321);
            variableNumber2 = Random.Range(2, 11);
            barrierText.text = variableNumber.ToString() + " / " + variableNumber2.ToString();
        }

        if (number == 4)
        {
            variableNumber = Random.Range(10, 37);
            variableNumber2 = Random.Range(10, 37);
            variableNumber3 = Random.Range(10, 37);
            barrierText.text = variableNumber.ToString() + " + " + variableNumber2.ToString() + " + " + variableNumber3.ToString();
        }

        if (number == 5)
        {
            variableNumber = Random.Range(4, 10);
            variableNumber2 = Random.Range(4, 10);
            barrierText.text = variableNumber.ToString() + "^2 + " + variableNumber2.ToString() + "^2";
        }

        if (number == 6)
        {
            variableNumber = Random.Range(20, 51);
            variableNumber2 = Random.Range(10, 21);
            variableNumber3 = Random.Range(2, 9);
            barrierText.text = "(" + variableNumber.ToString() + " - " + variableNumber2.ToString() + ") * " + variableNumber3.ToString();
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

            barrier2Script.isCollected = true;

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

            if (number == 3)
            {
                Random4();
            }

            if (number == 4)
            {
                Random5();
            }

            if (number == 5)
            {
                Random6();
            }

            if (number == 6)
            {
                Random7();
            }


        }
    }

    private void Random1()
    {
        // Update the score.
        int added = variableNumber + variableNumber2;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }

    private void Random2()
    {
        // Update the score.
        int added = variableNumber * variableNumber2 + variableNumber3;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";

    }

    private void Random3()
    {
        int added = (variableNumber * variableNumber2) + (variableNumber3 * variableNumber4);
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }

    private void Random4()
    {
        int added = variableNumber / variableNumber2;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }

    private void Random5()
    {
        int added = variableNumber + variableNumber2 + variableNumber3;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }

    private void Random6()
    {
        int added = (variableNumber * variableNumber) + (variableNumber2 * variableNumber2);
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }

    private void Random7()
    {
        int added = (variableNumber - variableNumber2) * variableNumber3;
        ArmyLogic.updateArmyCooldown = .5f;
        collectableControl.rangedScoreCount += added;
        collectableControl.totalScoreCount += added;
        ArmyLogic.recentType = "ranged";
    }



}