using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Signal")]
public class SignalSender : ScriptableObject
{

    public List<SignalListeners> listeners = new List<SignalListeners>();

    public void Raise()
    {
        // raise the signal
        for (int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListeners listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
        
    }

    public void DeRegisterListener(SignalListeners listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
        
    } 

}
