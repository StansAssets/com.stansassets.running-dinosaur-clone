using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Foundation;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_PlaymakerUI : UM_PluginSettingsUI
    {
        class UM_PlaymakerResolver : SA_iAPIResolver
        {
            public bool IsSettingsEnabled
            {
                get => UM_DefinesResolver.IsPlayMakerInstalled;

                set { }
            }

            public void ResetRequirementsCache() { }
        }

        const string k_PlaymakerUIClassName = "SA.CrossPlatform.Addons.PlayMaker.UM_PlaymakerActionsUI";
        const string k_PlaymakerStoreURL = "https://assetstore.unity.com/packages/tools/visual-scripting/playmaker-368";

        IMGUILayoutElement m_PlaymakerSettingsUI;
        UM_PlaymakerResolver m_PlaymakerResolver;

        public override void OnAwake()
        {
            base.OnAwake();

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Playmaker)");
            AddFeatureUrl("In App Purchases", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/In-App-Purchases");
            AddFeatureUrl("Game Services", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Game-Services");
            AddFeatureUrl("Social", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Social");
            AddFeatureUrl("Camera & Gallery", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Camera-&-Gallery");
            AddFeatureUrl("Local Notifications", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Local-Notifications");
            AddFeatureUrl("Native UI", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Native-UI");
            AddFeatureUrl("Advertisement", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Advertisement");
            AddFeatureUrl("Analytics", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Analytics-(Playmaker)");
        }

        public override string Title => "Playmaker";

        protected override string Description => "Use Ultimate Mobile API with Playmaker visual scripting solution.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_playmaker.png");

        protected override SA_iAPIResolver Resolver
        {
            get
            {
                if (m_PlaymakerResolver == null) m_PlaymakerResolver = new UM_PlaymakerResolver();

                return m_PlaymakerResolver;
            }
        }

        protected override void OnServiceUI()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Playmaker")))
            {
                if (UM_DefinesResolver.IsPlayMakerInstalled)
                {
                    EditorGUILayout.HelpBox("PlayMaker Plugin Installed!", MessageType.Info);
                    DrawPlayMakerSettings();
                }
                else
                {
                    EditorGUILayout.HelpBox("PlayMaker Plugin is Missing!", MessageType.Warning);
                    using (new IMGUIBeginHorizontal())
                    {
                        GUILayout.FlexibleSpace();
                        var click = GUILayout.Button("Get Playmaker", EditorStyles.miniButton, GUILayout.Width(120));
                        if (click) Application.OpenURL(k_PlaymakerStoreURL);

                        var refreshClick = GUILayout.Button("Refresh", EditorStyles.miniButton, GUILayout.Width(120));
                        if (refreshClick) UM_DefinesResolver.ProcessAssets();
                    }

                    EditorGUILayout.Space();

#if SA_DEVELOPMENT_PROJECT
                    EditorGUILayout.HelpBox("Dev mode section!", MessageType.Info);
                    DrawPlayMakerSettings();
#endif
                }
            }
        }

        void DrawPlayMakerSettings()
        {
            if (m_PlaymakerSettingsUI == null)
            {

                var settingsUI = CreateInstance(ReflectionUtility.FindType(k_PlaymakerUIClassName));
                if (settingsUI != null)
                {
                    m_PlaymakerSettingsUI = settingsUI as IMGUILayoutElement;
                    m_PlaymakerSettingsUI.OnLayoutEnable();
                }
            }

            if (m_PlaymakerSettingsUI == null)
                UM_SettingsUtil.DrawAddonRequestUI(UM_Addon.Playmaker);
            else
                m_PlaymakerSettingsUI.OnGUI();
        }
    }
}
