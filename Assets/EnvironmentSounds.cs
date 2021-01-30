using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSounds :
    MonoBehaviour
{
    public string soundParameter;
    public AK.Wwise.Event chic_sum;

    private ChickenManager chickenManager;

    private int InitialChickenCount = 0;

    protected void Start()
    {
        chickenManager = FindObjectOfType<ChickenManager>();
        InitialChickenCount = chickenManager.ChickensCount;
        chic_sum.Post(gameObject);
    }

    protected void Update()
    {
        var normalizedCount = (int)Math.Round(chickenManager.ChickensCount * 100.0) / InitialChickenCount;
        AkSoundEngine.SetRTPCValue(soundParameter, normalizedCount);
    }
}