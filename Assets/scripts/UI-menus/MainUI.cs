using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public static MainUI Instance;

    public web_request web_request;
    //public userInfo userInfo;

    void Start()
    {
        Instance = this;
        web_request = GetComponent<web_request>();
       // userInfo = GetComponent<userInfo>();     
    }

    
}
