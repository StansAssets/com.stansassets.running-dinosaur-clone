using System;

namespace StansAssets.ProjectSample.Controls
{
    class InputValue
    {
        public readonly string Name;
        readonly Func<bool> m_GetInputValueFunc;

        bool m_CurrentValue;

        public InputValue(string name, Func<bool> getValueFunc)
        {
            Name = name;
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