using UnityEngine;
using UnityEditor;
using Rotorz.ReorderableList;
using SA.CrossPlatform.Advertisement;
using SA.Foundation.Editor;
using GoogleMobileAds.Api;
using GoogleMobileAds.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor.Advertisement
{
    [InitializeOnLoad]
    public class UM_GoogleMobileAdsUI : IMGUILayoutElement
    {
        static UM_GoogleMobileAdsUI()
        {
            UM_AdvertisementUI.RegisterAdMobUILayout(CreateInstance<UM_GoogleMobileAdsUI>);
        }

        static void FillExampleSettings()
        {
            var settings = UM_GoogleAdsSettings.Instance;
            var android = settings.AndroidIds;

            android.AppId = "ca-app-pub-6101605888755494~6591356173";
            android.BannerId = "ca-app-pub-6101605888755494/8666994797";
            android.RewardedId = "ca-app-pub-6101605888755494/4727749786";
            android.NonRewardedId = "ca-app-pub-6101605888755494/6084105626";

            var ios = settings.IOSIds;
            ios.AppId = "ca-app-pub-6101605888755494~3384895876";
            ios.BannerId = "ca-app-pub-6101605888755494/5025280606";
            ios.RewardedId = "ca-app-pub-6101605888755494/7756628990";
            ios.NonRewardedId = "ca-app-pub-6101605888755494/2207545572";

            UM_GoogleAdsSettings.Save();
        }

        public override void OnGUI()
        {
            var settings = UM_GoogleAdsSettings.Instance;
            settings.IOSIds.AppId = UM_GoogleMobileAdsSettingsProxy.Instance.AdMobIOSAppId;
            settings.AndroidIds.AppId = UM_GoogleMobileAdsSettingsProxy.Instance.AdMobAndroidAppId;

            using (new IMGUIBeginHorizontal())
            {
                GUILayout.FlexibleSpace();
                var example = GUILayout.Button("Example", EditorStyles.miniButton, GUILayout.Width(80));
                if (example)
                    FillExampleSettings();

                var click = GUILayout.Button("Dashboard", EditorStyles.miniButton, GUILayout.Width(80));
                if (click)
                    Application.OpenURL("https://apps.admob.com/");

                var googleAdsInspector = GUILayout.Button("Google Ads Inspector", EditorStyles.miniButton, GUILayout.Width(140));
                if (googleAdsInspector)
                    GoogleMobileAdsSettingsEditor.OpenInspector();
            }

            using (new SA_H2WindowBlockWithSpace(new GUIContent("IOS")))
            {
                UM_AdvertisementUI.DrawPlatformIds(settings.IOSIds);
            }

            using (new SA_H2WindowBlockWithSpace(new GUIContent("ANDROID")))
            {
                if (string.IsNullOrEmpty(settings.AndroidIds.AppId))
                {
                    EditorGUILayout.HelpBox("Application id MUST be provided on Android platform before you make a build. " +
                        "Empty Application id may result in app crash on launch.", MessageType.Error);
                }

                UM_AdvertisementUI.DrawPlatformIds(settings.AndroidIds);
            }

            using (new SA_H2WindowBlockWithSpace(new GUIContent("SETTINGS")))
            {
                settings.BannerSize = (UM_GoogleBannerSize)IMGUILayout.EnumPopup("Banner Size", settings.BannerSize);
                settings.BannerPosition = (AdPosition)IMGUILayout.EnumPopup("Banner Position", settings.BannerPosition);
                settings.NPA = IMGUILayout.ToggleFiled("Non-Personalized Ads",
                    settings.TagForChildDirectedTreatment,
                    IMGUIToggleStyle.ToggleType.YesNo);

                settings.TagForChildDirectedTreatment = IMGUILayout.ToggleFiled("Tag For Child Directed Treatment",
                    settings.TagForChildDirectedTreatment,
                    IMGUIToggleStyle.ToggleType.YesNo);

                ReorderableListGUI.Title("Test Devices");
                ReorderableListGUI.ListField(settings.TestDevices, DrawTextFiled, () =>
                {
                    EditorGUILayout.LabelField("Configure your device as a test device.");
                });
                EditorGUILayout.Space();

                ReorderableListGUI.Title("Keywords");
                ReorderableListGUI.ListField(settings.Keywords, DrawTextFiled, () =>
                {
                    EditorGUILayout.LabelField("Provide keywords to admob so the ads can be targeted.");
                });
                EditorGUILayout.Space();
            }

            if (GUI.changed)
            {

                //UM_GoogleMobileAdsSettingsProxy.Instance.IsAdMobEnabled = true;
                UM_GoogleMobileAdsSettingsProxy.Instance.AdMobIOSAppId = settings.IOSIds.AppId;
                UM_GoogleMobileAdsSettingsProxy.Instance.AdMobAndroidAppId = settings.AndroidIds.AppId;

                UM_GoogleAdsSettings.Save();
                UM_GoogleMobileAdsSettingsProxy.Instance.Save();
            }

        }

        string DrawTextFiled(Rect position, string value)
        {
            return EditorGUI.TextField(position, value);
        }
    }
}
