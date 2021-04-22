using System;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    readonly Dictionary<string, List<Action<bool>>> m_Subscriptions = new Dictionary<string, List<Action<bool>>>();

    public void Subscribe(string control, Action<bool> callback)
    {
        if (!m_Subscriptions.ContainsKey(control)) {
            var list = new List<Action<bool>> { callback };
            m_Subscriptions.Add(control, list);
        }
        else {
            if (!m_Subscriptions[control].Contains(callback))
                m_Subscriptions[control].Add(callback);
        }
    }

    public void UnSubscribe(string control, Action<bool> callback)
    {
        if (m_Subscriptions.TryGetValue(control, out var maybeList)
         && maybeList.Contains(callback)) 
        {
            maybeList.Remove(callback);
        }
    }
    
    void Start()
    {
        foreach (var input in GetComponentsInChildren<IPlatformInput>()) {
            input.OnPressed += HandlePressed;
            input.OnReleased += HandleReleased;
        }
    }

    void HandlePressed(string control) => HandleInput(control, true);
    void HandleReleased(string control) => HandleInput(control, false);

    void HandleInput(string control, bool value)
    {
        if (m_Subscriptions.TryGetValue(control, out var callbackList)) {
            foreach (var callback in callbackList) {
                callback.Invoke(value);
            }
        }
    }
}
