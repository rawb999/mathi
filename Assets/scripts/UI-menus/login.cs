using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class login : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button LoginButton;


    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
            StartCoroutine(MainUI.Instance.web_request.Login(emailInput.text, passwordInput.text));

        });
    }

}
