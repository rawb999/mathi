using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Menu : MonoBehaviour
{
    //
    static public void Start_Screen()
    {
        //SubmitLeaderboardScore.GetPlayerHighScore();
        SceneManager.LoadSceneAsync(0);
        //Reset.resetValues();
    }
    static public void main_menuScreen()
    {

        SceneManager.LoadSceneAsync(0);

    }
    static public void start_game()
    {

        SceneManager.LoadSceneAsync(2);

    }
}
