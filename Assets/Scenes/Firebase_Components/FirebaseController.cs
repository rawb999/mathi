using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseController : MonoBehaviour
{


    public GameObject loginPanel, signupPanel, forgotPasswordPanel, myAccount, notificationPanel;

    public TMP_InputField loginEmail, forgotPasswordEmail, loginPassword, signupEmail, signupPassword, signupFname, signupLname, signupUsername;

    public TMP_Text notificationTitle, notificationMessage, accountUsername, accountEmail;

    public Toggle rememberMe;


    // login
    public void LoginUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
          //  showNotification("Error", "Please enter valid Information");

            return;
        }
        
    }

    //sign up
    public void SignupUser()
     {

        if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupUsername.text))


          //  showNotification("Error", "Please enter valid Information");

            return;
        
     }

    public void forgotpass()
    {

        if (string.IsNullOrEmpty(forgotPasswordEmail.text))
        {
           // showNotification("Error", "Email not Entered");
            return;
        }
    
    }
  //  private void showNotification(string title, string message)
   // {
    //    notificationTitle.text = "" + title;
    //    notificationMessage.text = "" + message;

//        notificationPanel.SetActive(true);
 //   }
  //  public void closeNotification()
   // {
    //    notificationTitle.text = "";
     //   notificationMessage.text = "";
      //  notificationPanel.SetActive(false);
    
    //}
    public void logOut()
    {
        myAccount.SetActive(false);
        //openStartMenu();
    }
}
