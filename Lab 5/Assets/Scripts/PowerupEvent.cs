using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerupEvent", menuName = "ScriptableObjects/PowerupEvent", order = 3)]
public class PowerupEvent : ScriptableObject
{
    private readonly List<PowerupEventListener> eventListeners = 
        new List<PowerupEventListener>();

    public void Raise(Powerup p)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(p);
    }

    public void RegisterListener(PowerupEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(PowerupEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}