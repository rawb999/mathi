using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class web_request : MonoBehaviour

   

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.
{

    //public GameObject userStartScreen;

    void Start()
    {
        // The following are tests.
        // StartCoroutine(GetDate("http://localhost/backend-time/getdate.php"));  //URL of file goes here 
        //StartCoroutine(Getusertest("http://localhost/MATHi/register_users.php"));
        //StartCoroutine(Login("Batman", "Superman"));
        // StartCoroutine(User_Registration("popsie", "lol0"));
        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }
    //------------------------- Notification Panel ----------------------------------//
/*
    public void showNotification(string title, string message)
    {
        notificationTitle.text = "" + title;
        notificationMessage.text = "" + message;

        notificationPanel.SetActive(true);
    }
      public void closeNotification()
    {
        notificationTitle.text = "";
        notificationMessage.text = "";

        notificationPanel.SetActive(false);
    }
    */

    //--------------------  TEST FOR Retrieving Date ---------------------------------
    public IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Data processing Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    //--------------------  TEST FOR looking at users ---------------------------------
    public IEnumerator Getusertest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Data processing Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    //--------------------  LOGGING IN ---------------------------------
    public IEnumerator Login(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("LoginEmail", email);
        form.AddField("LoginPass", password);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MATHi/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
              // interfaceControl.openuserPage();
                
                
            }
        }

    }





    //================================ User Registration Call ==============================

   public  IEnumerator User_Registration(string username, string password, string email, string f_name, string l_name)
    {
        WWWForm form = new WWWForm();
        form.AddField("LoginUser", username);
        form.AddField("LoginPass", password);
        form.AddField("LoginEmail", email);
        form.AddField("LoginF_name", f_name);
        form.AddField("LoginL_name", l_name);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MATHi/user_registration.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
               Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

        }

    }

    //end
}