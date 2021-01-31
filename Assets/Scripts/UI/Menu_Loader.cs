using AK.Wwise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Loader : MonoBehaviour
{
    private AK.Wwise.Event _lastPlayed;
    public IReadOnlyDictionary<string, Tuple<AK.Wwise.Event, State>> Events { get; private set; }
    private GameObject _lastObject;

    private void Start()
    {
        var scenesMusic = GetComponent<ScenesMusic>();
        Events = scenesMusic.MusicEvents;
        var activeScene = SceneManager.GetActiveScene();
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
        Debug.Log("Loader Scene");
    }

    

    public void QuitTheGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
