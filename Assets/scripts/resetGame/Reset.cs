using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{

    public static void resetValues()
    {
        Time.timeScale = 1;
        ArmyLogic.currentZombs = 0;
        ArmyLogic.currentTankZombs = 0;
        ArmyLogic.currentMeleeZombs = 0;
        ArmyLogic.currentRangedZombs = 0;
        ArmyLogic.recentTotalScore = 10;
        ArmyLogic.updatedTotalScore = 10;
        collectableControl.waveNumber = 0;
        collectableControl.totalScoreCount = 10;
        ArmyLogic.inFight = false;
        ArmyLogic.recentType = "melee";
        collectableControl.playerDead = false;
        fight.endFight();
        ArmyLogic.waveStarted = false;
    }
}
