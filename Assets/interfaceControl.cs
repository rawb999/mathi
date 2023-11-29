using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interfaceControl : MonoBehaviour
{
    public GameObject startPage, loginPanel, registerPanel, userPage, myAccount, leaderBoardpage, myaccountpanel;


    public void openstartPage()
    {
        startPage.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userPage.SetActive(false);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(false);

    }
    public void openloginPanel()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        userPage.SetActive(false);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(false);
       
    }
    public void openregisterPanel()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        userPage.SetActive(false);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(false);

    }
    public void openuserPage()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userPage.SetActive(true);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(false);

    }
    public void openmyAccount()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userPage.SetActive(false);
        myAccount.SetActive(true);
        leaderBoardpage.SetActive(false);

    }
    public void openleaderBoardpage()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userPage.SetActive(false);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(true);

    }

    public void openmyaccountpanel()
    {
        startPage.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userPage.SetActive(false);
        myAccount.SetActive(false);
        leaderBoardpage.SetActive(false);
        myaccountpanel.SetActive(true);

    }

}
