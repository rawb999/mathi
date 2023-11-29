using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Menu : MonoBehaviour
{
    //
    public void Start_Screen()
    {
        
        SceneManager.LoadSceneAsync(1);
        Reset.resetValues();
    }
}
