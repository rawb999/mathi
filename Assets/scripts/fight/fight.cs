using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fight : MonoBehaviour
{
    public bool fightStarted = false;
    //public GameObject player;
    //public GameObject eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void startFight()
    {
        collectableControl.scoreCount -= 20;
        PlayerMovement.inFight = true;
        ArmyLogic.inFight = true;
        CameraController.inFight = true;
    }

    public static void endFight()
    {
        PlayerMovement.inFight = false;
        ArmyLogic.inFight = false;
        CameraController.inFight = false;
    }
}
