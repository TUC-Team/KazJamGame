using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<HandAIBase> Hands { get; private set; } = new List<HandAIBase>();
    public int HandsCount => Hands.Count;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void RegisterHand(HandAIBase hand)
    {
        if (Hands.Contains(hand))
            return;
        
        Hands.Add(hand);
    }

    public void RemoveHand(HandAIBase hand)
    {
        Hands.Remove(hand);
        if (HandsCount == 0)
            _gameController.EndGame(true);
    }
}
