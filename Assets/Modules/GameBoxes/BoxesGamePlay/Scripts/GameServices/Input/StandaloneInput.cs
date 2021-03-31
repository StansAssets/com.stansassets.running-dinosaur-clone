using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    class StandaloneInput : MonoBehaviour, IInputService
    {
        public event Action OnJump;

        public float Horizontal =>  Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                OnJump?.Invoke();
            }
        }
    }
}
