using UnityEngine;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;
using UnityEditor;

namespace SA.CrossPlatform.Editor
{
    class UM_Facebook : UM_PluginSettingsUI
    {
        UM_FacebookResolver m_Resolver;

        public override void OnAwake()
        {
            base.OnAwake();

            AddFeatureUrl("Initialization", "https://github.com/StansAssets/com.stansassets.facebook/wiki/Initialization");
            AddFeatureUrl("Get User Info", "https://github.com/StansAssets/com.stansassets.facebook/wiki/Get-User-Info");
            AddFeatureUrl("Get Friends", "https://github.com/StansAssets/com.stansassets.facebook/wiki/GetFriends");
            AddFeatureUrl("Get Profile Url", "https://github.com/StansAssets/com.stansassets.facebook/wiki/GetProfileUrl");
        }

        public override string Title => "Facebook";
        protected override bool CanBeDisabled => false;

        protected override string Description => "Build cross-platform games with Facebook rapidly and easily.";
        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_facebook_icon.png");
        protected override SA_iAPIResolver Resolver => m_Resolver ?? (m_Resolver = new UM_FacebookResolver());

        protected override void OnServiceUI()
        {
            if (!UM_Packages.HasFacebookSDKFolder)
            {
                EditorGUILayout.HelpBox("Make sure you install Facebook SDK before installing an addon package.", MessageType.Warning);
                DownloadPackageButton();

                EditorGUILayout.Space();
            }

            if (!UM_Packages.IsFacebookAddonInstalled)
            {
                EditorGUILayout.HelpBox("Facebook SDK addon package is not yet added to the project.", MessageType.Info);
                ImportPackageButton();
            }

#if SA_FACEBOOK
            StansAssets.Facebook.Editor.FbSettingsTab.DrawSettingsUI();
#endif
        }

        void DownloadPackageButton()
        {
            using (new IMGUIBeginHorizontal())
            {
                GUILayout.FlexibleSpace();
                var click = GUILayout.Button("Open Download Page", EditorStyles.miniButton, GUILayout.Width(160));
                if (click)
                    Application.OpenURL(UM_Packages.FacebookSDKWebPage);
            }
        }

        void ImportPackageButton()
        {
            using (new IMGUIBeginHorizontal())
            {
                GUILayout.FlexibleSpace();
                var click = GUILayout.Button("Install", EditorStyles.miniButton, GUILayout.Width(120));
                if (click)
                    UM_Packages.InstallFacebookAddon();
            }
        }
    }
}
