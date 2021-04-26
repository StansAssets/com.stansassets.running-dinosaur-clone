using System;
using UnityEngine;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    [Serializable]
    class UM_PlatformStateLayout
    {
        [SerializeField]
        IMGUIHyperLabel m_Header;
        [SerializeField]
        IMGUIHyperLabel m_StateLabel;

        [SerializeField]
        GUIContent m_Off;
        [SerializeField]
        GUIContent m_ON;

        public UM_PlatformStateLayout(GUIContent content)
        {
            m_Header = new IMGUIHyperLabel(content, UM_Skin.PlatformBlockHeader);
            m_Header.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
            m_ON = new GUIContent("ON");
            m_Off = new GUIContent("OFF");
            m_StateLabel = new IMGUIHyperLabel(m_ON, UM_Skin.PlatformBlockHeader);
            m_StateLabel.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
        }

        public void SetActiveState(bool isActive)
        {
            if (isActive)
            {
                m_StateLabel.SetContent(m_ON);
                m_StateLabel.SetColor(SettingsWindowStyles.SelectedElementColor);
            }
            else
            {
                m_StateLabel.SetContent(m_Off);
                m_StateLabel.SetColor(SettingsWindowStyles.ProDisabledImageColor);
            }
        }

        public GUIContent Content => m_Header.Content;

        public Color StateColor => m_StateLabel.Color;

        public bool OnGUI()
        {
            GUILayout.Space(5);
            using (new IMGUIBeginHorizontal())
            {
                GUILayout.Space(10);

                var headerWidth = m_Header.CalcSize().x;
                var click = m_Header.Draw(GUILayout.Width(headerWidth));
                GUILayout.FlexibleSpace();
                var labelClick = m_StateLabel.Draw(GUILayout.Width(40));
                if (click || labelClick)
                    return true;
                else
                    return false;
            }
        }
    }
}
