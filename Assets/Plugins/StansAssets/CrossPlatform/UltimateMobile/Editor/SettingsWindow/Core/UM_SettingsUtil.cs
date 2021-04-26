using System;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    static class UM_SettingsUtil
    {
        const string k_PlaymakerAddon = "https://dl.dropboxusercontent.com/s/moo7qo4dy9aoujf/PlayMakerAddon.v2020.6.unitypackage";
        const string k_AdMobAddon = "https://dl.dropboxusercontent.com/s/7gi07n15hb03slv/GoogleMobileAdsClientv2020.10.unitypackage";
        const string k_UnityAdsAddon = "https://dl.dropboxusercontent.com/s/e796extie4nisiv/UnityAdsClient.v2020.10.unitypackage";

        public static void DrawAddonRequestUI(UM_Addon addon)
        {
            EditorGUILayout.HelpBox("Ultimate Mobile " + addon + " Addon required", MessageType.Warning);
            using (new IMGUIBeginHorizontal())
            {
                GUILayout.FlexibleSpace();
                var content = new GUIContent(" " + addon + " Addon", UM_Skin.GetPlatformIcon("unity_icon.png"));
                var click = GUILayout.Button(content, EditorStyles.miniButton, GUILayout.Width(120), GUILayout.Height(18));
                if (!click) return;

                string url;
                switch (addon)
                {
                    case UM_Addon.AdMob:
                        url = k_AdMobAddon;
                        break;
                    case UM_Addon.Playmaker:
                        url = k_PlaymakerAddon;
                        break;
                    case UM_Addon.UnityAds:
                        url = k_UnityAdsAddon;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(addon), addon, null);
                }

                SA_PackageManager.DownloadAndImport(addon + " Addon", url, false);
            }
        }
    }
}
