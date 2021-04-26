using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    abstract class UM_ServiceSettingsUI : SA_ServiceLayout
    {
        readonly List<UM_ServicePlatformInfo> m_Platforms = new List<UM_ServicePlatformInfo>();

        public override void OnLayoutEnable()
        {
            m_Features.Clear();
            m_Platforms.Clear();
        }

        protected override IEnumerable<string> SupportedPlatforms => new List<string> { "iOS", "Android", "Unity Editor" };

        protected override SA_iAPIResolver Resolver => null;

        protected void AddPlatform(UM_UIPlatform platform, UM_NativeServiceSettings settings)
        {
            var info = new UM_ServicePlatformInfo(platform, settings);
            m_Platforms.Add(info);
        }

        protected override void DrawServiceRequirements() { }

        protected void DrawDefaultBlocks()
        {
            if (m_Platforms.Count > 0)
            {
                using (new SA_WindowBlockWithSpace(new GUIContent("Plugins"), 5))
                {
                    foreach (var platformInfo in m_Platforms)
                    {
                        var clicked = platformInfo.Layout.OnGUI();
                        if (clicked) OpenPlatformUI(platformInfo);
                    }
                }
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("COMING SOON!!\n" +
                    "Feel free to get in touch if you need to get this working NOW.", MessageType.Info);
            }
        }

        protected override void DrawGettingStartedBlock()
        {
            base.DrawGettingStartedBlock();
            DrawDefaultBlocks();
        }

        void OpenPlatformUI(UM_ServicePlatformInfo platformInfo)
        {
            var info = new UM_SettingsWindow.SelectedBlockInfo();
            info.SettingsBlockTypeName = platformInfo.Settings.ServiceUIType.Name;
            info.Platform = platformInfo.Platform;

            UM_SettingsWindow.SelectBlock(info);
        }

        protected override void DrawServiceStateInteractive() { }

        protected override bool DrawServiceStateInfo()
        {
            foreach (var platform in m_Platforms)
            {
                using (new IMGUIChangeContentColor(platform.Layout.StateColor))
                {
                    var content = new GUIContent(platform.Layout.Content.image);
                    EditorGUILayout.LabelField(content, GUILayout.Height(22), GUILayout.Width(22));
                }

                GUILayout.Space(-6);
            }

            return false;
        }

        protected override void CheckServiceAvailability()
        {
            foreach (var platform in m_Platforms) platform.Layout.SetActiveState(platform.Settings.IsEnabled);
        }
    }
}
