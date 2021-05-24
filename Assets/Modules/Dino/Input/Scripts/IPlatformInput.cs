using System;

namespace StansAssets.ProjectSample.Controls
{
    public enum InputEventType
    {
        Jump = 0,
        Duck = 1
    }

    public interface IPlatformInput
    {
        event Action<InputEventType> OnPressed, OnReleased;
        void Update();
    }
}