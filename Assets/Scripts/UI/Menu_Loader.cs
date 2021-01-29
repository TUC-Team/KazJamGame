using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Loader : MonoBehaviour
{

    public void LoadTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loader Scene");
    }



    public void QuitTheGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
