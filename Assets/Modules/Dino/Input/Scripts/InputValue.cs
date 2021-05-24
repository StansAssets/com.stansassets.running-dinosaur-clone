using System;

namespace StansAssets.ProjectSample.Controls
{
    class InputValue
    {
        public readonly InputEventType EventType;
        readonly Func<bool> m_GetInputValueFunc;

        bool m_CurrentValue;

        public InputValue(InputEventType eventType, Func<bool> getValueFunc)
        {
            EventType = eventType;
            m_GetInputValueFunc = getValueFunc;
            m_CurrentValue = m_GetInputValueFunc();
        }

        public bool? GetChangedValue()
        {
            var value = m_GetInputValueFunc();
            if (m_CurrentValue == value)
                return null;

            m_CurrentValue = value;
            return value;
        }
    }
}