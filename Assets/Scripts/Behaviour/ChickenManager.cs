using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenManager : MonoBehaviour
{
    public List<ChickenAIBase> Chickens { get; private set; } = new List<ChickenAIBase>();
    public int ChickensCount => Chickens.Count;
    
    public System.Action<ChickenAIBase> ChickenRemovedEvent;
    public System.Action<ChickenAIBase> ChickenRegisteredEvent;
    
    public void RegisterChicken(ChickenAIBase chicken)
    {
        Chickens.Add(chicken);
        ChickenRegisteredEvent?.Invoke(chicken);
    }

    public void RemoveChicken(ChickenAIBase chicken)
    {
        Chickens.Remove(chicken);
        ChickenRemovedEvent?.Invoke(chicken);
    }
}
