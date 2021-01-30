using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TextMeshPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chickenCountText;

    private ChickenManager _chickenManager;

    private void Awake()
    {
        _chickenManager = FindObjectOfType<ChickenManager>();
    }

    private void UpdateChickenCount(ChickenAIBase chicken)
    {
        chickenCountText.text = _chickenManager.ChickensCount.ToString();
    }

    private void OnEnable()
    {
        _chickenManager.ChickenRegisteredEvent += UpdateChickenCount;
        _chickenManager.ChickenRemovedEvent += UpdateChickenCount;
    }
    
    private void OnDisable()
    {
        _chickenManager.ChickenRegisteredEvent -= UpdateChickenCount;
        _chickenManager.ChickenRemovedEvent -= UpdateChickenCount;
    }
}
