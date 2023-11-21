using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_managerUI : MonoBehaviour
{
    public GameObject GameOverUI;
  
    public void gameOver()
    {
        GameOverUI.SetActive(true);
    
    }
    
}
