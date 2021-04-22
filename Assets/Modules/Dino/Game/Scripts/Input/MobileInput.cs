using System;
using UnityEngine;

class MobileInput : MonoBehaviour, IPlatformInput
{
    public event Action<string> OnPressed;
    public event Action<string> OnReleased;

    [SerializeField] string m_InputName;

#if UNITY_EDITOR || UNITY_STANDALONE
    void Start()
    {
        gameObject.SetActive(false);  
    }
#endif
    
    public void Press() => OnPressed?.Invoke(m_InputName);
    public void Release() => OnReleased?.Invoke(m_InputName);
}
