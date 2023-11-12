using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseController : MonoBehaviour
{


    public GameObject loginPanel, signupPanel, forgotpasswordpanel;

    public TMP_InputField loginEmail, forgotPasswordEmail, loginPassword, signupEmail, signupPassword, signupFname, signupLname, signupUsername;

    // login
    public void LoginUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
            return;
        }
        
    }

    //sign up
    public void SignupUser()
     {

        if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupUsername.text))

            return;
        
     }
    
        

}
