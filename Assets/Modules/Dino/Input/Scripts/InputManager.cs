using System;
using System.Collections.Generic;

namespace StansAssets.ProjectSample.Controls
{
    public interface IInputManager 
    {
        void Subscribe(InputEventType eventType, Action<bool> callback);
        void UnSubscribe(InputEventType eventType, Action<bool> callback);
    }

    public class InputManager : IInputManager
    {
        readonly Dictionary<InputEventType, List<Action<bool>>> m_Subscriptions = new Dictionary<InputEventType, List<Action<bool>>>();
        private readonly IPlatformInput m_PlatfromInput;

        public InputManager(IPlatformInput platfromInput)
        {
            if(platfromInput == null)
            {
                throw new ArgumentNullException("Given IPlatformInput is null. Please check implementation for current platform.");
            }

            m_PlatfromInput = platfromInput;
            m_PlatfromInput.OnPressed += HandlePressed;
            m_PlatfromInput.OnReleased += HandleReleased;
        }

        public void Subscribe(InputEventType eventType, Action<bool> callback)
        {
            if (!m_Subscriptions.ContainsKey(eventType)) {
                var list = new List<Action<bool>> { callback };
                m_Subscriptions.Add(eventType, list);
            }
            else {
                if (!m_Subscriptions[eventType].Contains(callback))
                    m_Subscriptions[eventType].Add(callback);
            }
        }

        public void UnSubscribe(InputEventType eventType, Action<bool> callback)
        {
            if (m_Subscriptions.TryGetValue(eventType, out var maybeList)
             && maybeList.Contains(callback)) { maybeList.Remove(callback); }
        }

        void HandlePressed(InputEventType eventType) => HandleInput(eventType, true);
        void HandleReleased(InputEventType eventType) => HandleInput(eventType, false);

        void HandleInput(InputEventType eventType, bool value)
        {
            if (m_Subscriptions.TryGetValue(eventType, out var callbackList)) {
                foreach (var callback in callbackList) { callback.Invoke(value); }
            }
        }
    }
}