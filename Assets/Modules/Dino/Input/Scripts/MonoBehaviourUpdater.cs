using System;
using System.Collections.Generic;
using UnityEngine;

namespace StansAssets.ProjectSample.Controls
{
    class MonoBehaviourUpdater : MonoBehaviour
    {
        readonly List<Action> m_Subscribers = new List<Action>();

        public void Subscribe(Action action)
        {
            if (m_Subscribers.Contains(action))
            {
                Debug.LogWarning($"Action: {action.ToString()} already subscribed to MonoBehaviourUpdater");
                return;
            }

            m_Subscribers.Add(action);
        }

        public void Unsubscribe(Action action)
        {
            if (m_Subscribers.Contains(action))
            {
                m_Subscribers.Remove(action);
            }
        }

        private void Update()
        {
            for(var i = 0; i < m_Subscribers.Count; ++i)
            {
                m_Subscribers[i].Invoke();
            }
        }
    }
}