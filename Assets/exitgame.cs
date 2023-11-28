using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class exitgame : MonoBehaviour
{


    public Button exitButton;

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
        exitButton.onClick.AddListener(() => { QuitGame(); });

    }

    
        //exitButton.onClick.AddListener(() =>{QuitGame();});

    
}
