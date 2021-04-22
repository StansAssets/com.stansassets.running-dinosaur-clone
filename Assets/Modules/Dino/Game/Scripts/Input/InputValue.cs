using System;

namespace StansAssets.ProjectSample.Controls
{
    struct InputValue
    {
        public readonly string Name;
        readonly Func<bool> m_GetInputValueFunc;

        public InputValue(string name, Func<bool> getValueFunc)
        {
            Name = name;
            m_GetInputValueFunc = getValueFunc;
            Value = false;
        }

        public bool Value { get; private set; }

        public bool HasChanged()
        {
            var currentValue = Value;
            Value = m_GetInputValueFunc();
            return currentValue != Value;
        }
    }
}