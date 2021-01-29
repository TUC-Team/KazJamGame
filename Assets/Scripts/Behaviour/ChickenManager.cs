using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenManager : MonoBehaviour
{
    public List<ChickenAIBase> Chickens { get; private set; } = new List<ChickenAIBase>();
    public int ChickensCount => Chickens.Count;

    public System.Action<ChickenAIBase> ChickenRemovedEvent;
    public System.Action<ChickenAIBase> ChickenRegisteredEvent;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void RegisterChicken(ChickenAIBase chicken)
    {
        Chickens.Add(chicken);
        ChickenRegisteredEvent?.Invoke(chicken);
    }

    public void RemoveChicken(ChickenAIBase chicken)
    {
        Chickens.Remove(chicken);
        ChickenRemovedEvent?.Invoke(chicken);

        if (ChickensCount == 0)
            _gameController.EndGame(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }

    }
}
