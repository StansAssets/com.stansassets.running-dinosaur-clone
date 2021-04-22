using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class UnityInput : MonoBehaviour, IPlatformInput
{
    public event Action<string> OnPressed;
    public event Action<string> OnReleased;
#if UNITY_EDITOR || UNITY_STANDALONE

    readonly List<InputValue> m_Inputs = new List<InputValue>();
    
    void Start()
    {
        m_Inputs.Add(new InputValue("Jump", () => Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0.05f));
        m_Inputs.Add(new InputValue("Duck", () => Input.GetButton("Crouch") || Input.GetAxis("Vertical") < -0.05f));
    }

    void Update()
    {
        foreach (var input in m_Inputs.Where(i => i.HasChanged())) {
            (input.Value ? OnPressed : OnReleased)?.Invoke(input.Name);
        }
    }
#endif
}
