using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public BooleanEvent GameEndedEvent = new BooleanEvent();
    public bool IsGameEnded { get; private set; } = false;
    
    public void EndGame(bool isWin)
    {
        IsGameEnded = true;
        GameEndedEvent?.Invoke(isWin);

        print("Game ended, is win = " + isWin);

        CursorHelper.DisableFpsMode();
    }

    void Update() {
        if ( !Application.isEditor ) {
            return;
        }
        if ( Input.GetKeyDown(KeyCode.Y) ) {
            EndGame(true);
        }
        if ( Input.GetKeyDown(KeyCode.U) ) {
            EndGame(false);
        }
    }
}
