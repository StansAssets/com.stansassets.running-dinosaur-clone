using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Controls
{
    class MobileInput : MonoBehaviour, IPlatformInput
    {
        public event Action<string> OnPressed;
        public event Action<string> OnReleased;

        [SerializeField] string m_InputName;

#if UNITY_EDITOR || UNITY_STANDALONE
        void Start()
        {
            foreach (var btn in GetComponentsInChildren<Button>()) {
                btn.interactable = false;
            }
        }
#endif

        [UsedImplicitly] public void Press() => OnPressed?.Invoke(m_InputName);
        [UsedImplicitly] public void Release() => OnReleased?.Invoke(m_InputName);
    }
}