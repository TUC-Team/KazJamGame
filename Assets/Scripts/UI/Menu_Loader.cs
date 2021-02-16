using AK.Wwise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Loader : MonoBehaviour
{
    public AK.Wwise.Event menuMusic;
    public AK.Wwise.Event levelMusic;
    public AK.Wwise.Event winMusic;
    public AK.Wwise.Event loseMusic;
    public AK.Wwise.Event creditsMusic;
    public State levelStartState;
    public State winState;
    public State loseState;
    private AK.Wwise.Event _lastPlayed;
    public IReadOnlyDictionary<string, Tuple<AK.Wwise.Event, State>> Events { get; private set; }
    private GameObject _lastObject;

    private void Start()
    {

        Events =
        new Dictionary<string, Tuple<AK.Wwise.Event, AK.Wwise.State>>(StringComparer.OrdinalIgnoreCase)
        {
            { "MenuScene", Tuple.Create(menuMusic, (AK.Wwise.State)null) },
            { "LevelScene", Tuple.Create(levelMusic, levelStartState) },
            { "CreditsScene", Tuple.Create(creditsMusic, (AK.Wwise.State)null) },
            { "WinScene", Tuple.Create(winMusic, winState) },
            { "LoseScene", Tuple.Create(loseMusic, loseState) },
        }; var activeScene = SceneManager.GetActiveScene();
        if (activeScene != null)
        {
            ChangeSceneMusic(activeScene);
        }
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene from, Scene to)
    {
        ChangeSceneMusic(to);
    }

    private void ChangeSceneMusic(Scene to)
    {
        var go = to.GetRootGameObjects().First();
        if (go == null)
        {
            return;
        }
        if (Events.TryGetValue(to.name, out var pair))
        {
            pair.Item1.Post(go);
            if (pair.Item2 != null)
            {
                pair.Item2.SetValue();
            }
            var stop = go.AddComponent<music_stop>();
            stop.LastPlayed = pair.Item1;
        }
    }
    public void LoadTheGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // Debug.Log("Loader Scene");
    }



    public void QuitTheGame()
    {
       // Debug.Log("Quit");
        Application.Quit();
    }
}
