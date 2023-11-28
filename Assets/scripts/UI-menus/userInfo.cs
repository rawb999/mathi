using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userInfo : MonoBehaviour
{
    public string UserID;
    public string UserName;
    public string Email;
    public string Name;
    public string Upassword;

    public void SetInfo(string username, string userpassword)
    { 
        UserName = username;
        Upassword = userpassword;
        
    
    }
}
