using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class RegisterUI : MonoBehaviour
{
    //public GameObject startPage, registerpanel;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField emailInput;
    public TMP_InputField f_nameInput;
    public TMP_InputField l_nameInput;
    public Button registerButton;
    //bool madeaccount = false;
    
   // private bool isCoroutineRunning;

    void Start()
    {
        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainUI.Instance.web_request.User_Registration(usernameInput.text, passwordInput.text, emailInput.text, f_nameInput.text, l_nameInput.text));
             //startPage.SetActive(true);
             //SceneManager.LoadSceneAsync(1);
             //registerpanel.SetActive(false);
             //bool madeaccount = true;
             
             
        });
        
        

    
    }

   

}
