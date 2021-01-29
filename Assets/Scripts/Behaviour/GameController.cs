﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent<bool> GameEndedEvent;
    public bool IsGameEnded { get; private set; } = false;
    
    public void EndGame(bool isWin)
    {
        IsGameEnded = true;
        GameEndedEvent?.Invoke(isWin);

        print("Game ended, is win = " + isWin);
    }
}