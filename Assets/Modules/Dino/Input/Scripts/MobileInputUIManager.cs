using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StansAssets.ProjectSample.Controls
{
    internal class MobileInputUIManager : MonoBehaviour
    {
        public static event Action<InputEventType> OnPressed = delegate { };
        public static event Action<InputEventType> OnReleased = delegate { };

        [UsedImplicitly]
        public void OnJumpPressed(BaseEventData eventData)
        {
            OnPressed.Invoke(InputEventType.Jump);
        }

        [UsedImplicitly]
        public void OnJumpReleased(BaseEventData eventData)
        {
            OnReleased.Invoke(InputEventType.Jump);
        }

        [UsedImplicitly]
        public void OnDuckPressed(BaseEventData eventData)
        {
            OnPressed.Invoke(InputEventType.Duck);
        }

        [UsedImplicitly]
        public void OnDuckReleased(BaseEventData eventData)
        {
            OnReleased.Invoke(InputEventType.Duck);
        }
    }
}