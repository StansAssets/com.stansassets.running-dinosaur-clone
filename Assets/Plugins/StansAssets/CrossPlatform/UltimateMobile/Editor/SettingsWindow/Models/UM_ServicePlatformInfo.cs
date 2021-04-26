using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.CrossPlatform.Editor
{
    [Serializable]
    class UM_ServicePlatformInfo
    {
        [SerializeField]
        UM_UIPlatform platform;
        [SerializeField]
        UM_NativeServiceSettings m_settings;

        [SerializeField]
        GUIContent m_content;
        [SerializeField]
        UM_PlatformStateLayout m_layout;

        public UM_ServicePlatformInfo(UM_UIPlatform platform, UM_NativeServiceSettings settings)
        {
            this.platform = platform;
            m_settings = settings;

            switch (platform)
            {
                case UM_UIPlatform.IOS:
                    m_content = new GUIContent(" iOS (" + m_settings.ServiceName + ")", UM_Skin.GetPlatformIcon("ios_icon.png"));
                    break;
                case UM_UIPlatform.Android:
                    m_content = new GUIContent(" Android (" + m_settings.ServiceName + ")", UM_Skin.GetPlatformIcon("android_icon.png"));
                    break;
            }

            m_layout = new UM_PlatformStateLayout(m_content);
        }

        public UM_PlatformStateLayout Layout => m_layout;

        public UM_UIPlatform Platform => platform;

        public UM_NativeServiceSettings Settings => m_settings;
    }
}
