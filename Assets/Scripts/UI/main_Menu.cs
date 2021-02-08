using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class main_Menu : MonoBehaviour
{
    public GameObject Main_Menu;
    public GameObject Settings_Menu;
    //public GameObject Credits_Menu;

    public static bool isOpened = false;

    public void StartTheGame()
    {
      //  Debug.Log("Start");
        SceneManager.LoadScene("TutorialScene");
    }
    public void SettingsTheGame()
    {
        Debug.Log("Settings");
        Main_Menu.SetActive(false);
        Settings_Menu.SetActive(true);
    }
    public void CreditsTheGame()
    {
      //  Debug.Log("Credits");
        SceneManager.LoadScene("CreditsScene");
    }
    public void QuitTheGame()
    {
       // Debug.Log("Quit");
        Application.Quit();
    }


    public void Back()
    {
        Main_Menu.SetActive(true);
        Settings_Menu.SetActive(false);
    }

    public void Start()
    {

        CursorHelper.DisableFpsMode();
        EventSystem.current.SetSelectedGameObject(null);

    }


    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("GameScene");
           //SceneManager.SetActiveScene(SceneManager.("GameScene"));
            //SceneManager.UnloadSceneAsync("MenuScene");
        }

    }

}

