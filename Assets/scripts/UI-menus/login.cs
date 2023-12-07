using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button LoginButton;
    bool user_loggedin = false;
    
    private bool isCoroutineRunning;


    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainUI.Instance.web_request.Login(emailInput.text, passwordInput.text));
          

        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCoroutineRunning)
            {
                Debug.Log("Coroutine is running");
            }
            else
            {
                Debug.Log("Coroutine is not running");
                user_loggedin = true; 
                thingy();
            }
        }
    }



    public void thingy()
    {
        if (user_loggedin == true)
        {
            
           // SceneManager.LoadSceneAsync(1);           
            
        };
    }

}
