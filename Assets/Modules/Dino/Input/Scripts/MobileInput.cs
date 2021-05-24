using System;

namespace StansAssets.ProjectSample.Controls
{
    public class MobileInput: IPlatformInput
    {
        public event Action<InputEventType> OnPressed;
        public event Action<InputEventType> OnReleased;

        public MobileInput()
        {
            MobileInputUIManager.OnPressed += (eventType) => OnPressed.Invoke(eventType);
            MobileInputUIManager.OnReleased += (eventType) => OnReleased.Invoke(eventType);
        }

        public void Update()
        {
            // Nothing TODO here for Mobile input
        }
    }
}