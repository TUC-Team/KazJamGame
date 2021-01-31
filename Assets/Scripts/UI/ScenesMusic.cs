using AK.Wwise;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesMusic : MonoBehaviour
{
    public AK.Wwise.Event menuMusic;
    public AK.Wwise.Event levelMusic;
    public AK.Wwise.Event winMusic;
    public AK.Wwise.Event loseMusic;
    public AK.Wwise.Event creditsMusic;
    public State levelStartState;
    public State winState;
    public State loseState;

    public IReadOnlyDictionary<string, Tuple<AK.Wwise.Event, State>> MusicEvents { get; private set; }

    private void Start()
    {
        MusicEvents = new Dictionary<string, Tuple<AK.Wwise.Event, AK.Wwise.State>>(StringComparer.OrdinalIgnoreCase)
        {
            { "MenuScene", Tuple.Create(menuMusic, (AK.Wwise.State)null) },
            { "LevelScene", Tuple.Create(levelMusic, levelStartState) },
            { "CreditsScene", Tuple.Create(creditsMusic, (AK.Wwise.State)null) },
            { "WinScene", Tuple.Create(winMusic, winState) },
            { "LoseScene", Tuple.Create(loseMusic, loseState) },
        };
    }
}