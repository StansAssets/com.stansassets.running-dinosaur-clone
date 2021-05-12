using System;
using UnityEngine;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;
using UnityEditor;

namespace SA.CrossPlatform.Editor
{
    class UM_AdvertisementPlatformUI : SA_CollapsableWindowBlockLayout
    {
        readonly GUIContent m_Off;
        readonly GUIContent m_On;
        readonly SA_iAPIResolver m_Resolver;

        public UM_AdvertisementPlatformUI(string name, string image, SA_iAPIResolver resolver, Action onGUI)
            : base(new GUIContent(name, UM_Skin.GetPlatformIcon(image)), onGUI)
        {
            m_On = new GUIContent("ON");
            m_Off = new GUIContent("OFF");
            var stateLabel = new IMGUIHyperLabel(m_On, EditorStyles.boldLabel);
            stateLabel.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);

            m_Resolver = resolver;
        }

        protected override void OnAfterHeaderGUI()
        {
            GUILayout.FlexibleSpace();
            if (m_Resolver.IsSettingsEnabled)
                using (new IMGUIChangeColor(SettingsWindowStyles.SelectedElementColor))
                {
                    EditorGUILayout.LabelField(m_On, EditorStyles.boldLabel, GUILayout.Width(35));
                }
            else
                EditorGUILayout.LabelField(m_Off, EditorStyles.boldLabel, GUILayout.Width(35));
        }
    }
}
