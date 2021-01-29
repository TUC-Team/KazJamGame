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

public class main_Menu : MonoBehaviour
{
    public GameObject Main_Menu;
    public GameObject Settings_Menu;

    public static bool isOpened = false;

    public void StartTheGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("GameScene");
    }
    public void SettingsTheGame()
    {
        Debug.Log("Settings");
        Main_Menu.SetActive(false);
        Settings_Menu.SetActive(true);
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


    public void Back()
    {
        Main_Menu.SetActive(true);
        Settings_Menu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);

    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }

    }

}

