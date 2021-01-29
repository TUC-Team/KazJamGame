using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine.Audio;

public class Main_Menu : MonoBehaviour
{
   // public GameObject Background;

    public static bool isOpened = false;
    // GameObject menuUI;


    public void StartTheGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }
    public void SettingsTheGame()
    {
        Debug.Log("Settings");
    }
    public void CreditsTheGame()
    {
        Debug.Log("Credits");
    }
    public void QuitTheGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
       GoToMain();
        }

    }

   public void GoToMain()
   {
     SceneManager.GetSceneByName("MenuScene");
   }


}

