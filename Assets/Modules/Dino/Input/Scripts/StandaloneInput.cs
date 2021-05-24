using System;
using System.Collections.Generic;
using UnityEngine;

namespace StansAssets.ProjectSample.Controls
{
    public class StandaloneInput : IPlatformInput
    {
        const string VERTICAL_AXIS_NAME = "Vertical";

        public event Action<InputEventType> OnPressed = delegate { };
        public event Action<InputEventType> OnReleased = delegate { };

        readonly float m_AxisInputError = 0.05f;
        readonly List<InputValue> m_Inputs = new List<InputValue>();

        public StandaloneInput(float axisInputError = 0.05f)
        {
            m_AxisInputError = axisInputError;

            m_Inputs.Add(new InputValue(InputEventType.Jump, 
                () => Input.GetButton(InputEventType.Jump.ToString()) || Input.GetAxis(VERTICAL_AXIS_NAME) > m_AxisInputError));
            m_Inputs.Add(new InputValue(InputEventType.Duck, 
                () => Input.GetButton(InputEventType.Duck.ToString()) || Input.GetAxis(VERTICAL_AXIS_NAME) < -m_AxisInputError));

            Updater.Subscribe(Update);
        }

        public void Update()
        {
            foreach (var input in m_Inputs)
            {
                var changedValue = input.GetChangedValue();
                if (changedValue != null)
                {
                    (changedValue.Value ? OnPressed : OnReleased)?.Invoke(input.EventType);
                }
            }
        }

        static MonoBehaviourUpdater s_Updater;
        static MonoBehaviourUpdater Updater
        {
            get
            {
                if(s_Updater == null)
                {
                    GameObject updaterGO = new GameObject();
                    updaterGO.name = nameof(MonoBehaviourUpdater);
                    UnityEngine.Object.DontDestroyOnLoad(updaterGO);
                    s_Updater = updaterGO.AddComponent<MonoBehaviourUpdater>();
                }
                return s_Updater;
            }
        }
    }
}