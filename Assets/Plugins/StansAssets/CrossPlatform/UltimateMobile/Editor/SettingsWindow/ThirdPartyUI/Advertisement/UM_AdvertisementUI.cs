using System;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using SA.CrossPlatform.Advertisement;
using StansAssets.Plugins.Editor;
using UnityEditor.PackageManager;

namespace SA.CrossPlatform.Editor
{
    public class UM_AdvertisementUI : UM_PluginSettingsUI
    {
        static IMGUILayoutElement s_AdMobSettingsLayout;
        static IMGUILayoutElement s_UnityAdsSettingsLayout;

        static Func<IMGUILayoutElement> s_CreateAdMobLayout;
        static Func<IMGUILayoutElement> s_CreateUnityAdsLayout;


        public static void RegisterAdMobUILayout(Func<IMGUILayoutElement>  createAdMobLayout)
        {
            s_CreateAdMobLayout = createAdMobLayout;
        }

        public static void RegisterUnityAdsUILayout(Func<IMGUILayoutElement> createLayoutElement)
        {
            s_CreateUnityAdsLayout = createLayoutElement;
        }

        class UM_AdsResolver : SA_iAPIResolver
        {
            public bool IsSettingsEnabled
            {
                get => UM_DefinesResolver.IsAdMobInstalled || UM_DefinesResolver.IsUnityAdsInstalled;
                set { }
            }

            public void ResetRequirementsCache() { }
        }

        class UM_GoogleAdsResolver : SA_iAPIResolver
        {
            public bool IsSettingsEnabled
            {
                get => UM_DefinesResolver.IsAdMobInstalled;
                set { }
            }

            public void ResetRequirementsCache() { }
        }

        public class UM_UnityAdsResolver : SA_iAPIResolver
        {
            public bool IsSettingsEnabled
            {
                get => UM_DefinesResolver.IsUnityAdsInstalled;
                set { }
            }

            public void ResetRequirementsCache() { }
        }

        public class UM_ChartBoostResolver : SA_iAPIResolver
        {
            public bool IsSettingsEnabled
            {
                get => false;
                set { }
            }

            public void ResetRequirementsCache() { }
        }

        const string k_AdMobSdkDownloadUrl = "https://github.com/googleads/googleads-mobile-unity/releases/download/v5.4.0/GoogleMobileAds-v5.4.0.unitypackage";

        UM_AdsResolver m_ServiceResolver;

        UM_AdvertisementPlatformUI m_AdMobBlock;
        UM_AdvertisementPlatformUI m_UnityAdBlock;
        UM_AdvertisementPlatformUI m_ChartboostBlock;

        public override void OnAwake()
        {
            base.OnAwake();
            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Advertisement)");
            AddFeatureUrl("Initialization", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Enabling-the-Ads-Service");
            AddFeatureUrl("Banner Ads", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Banner-Ads");
            AddFeatureUrl("Non-rewarded Ads", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Non-rewarded-Ads");
            AddFeatureUrl("Rewarded Ads", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Rewarded-Ads");

            AddFeatureUrl("Unity Ads", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Unity-Ads");
            AddFeatureUrl("Google AdMob", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Google-AdMob");
            AddFeatureUrl("Google EU Consent", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Google-AdMob#consent-from-european-users");
            AddFeatureUrl("Chartboost", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Chartboost");
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            m_UnityAdBlock = new UM_AdvertisementPlatformUI("Unity Ads", "unity_icon.png", new UM_UnityAdsResolver(), DrawUnityAdsUI);

            m_AdMobBlock = new UM_AdvertisementPlatformUI("Google AdMob", "google_icon.png", new UM_GoogleAdsResolver(), DrawAdMobUI);

            m_ChartboostBlock = new UM_AdvertisementPlatformUI("Chartboost", "chartboost_icon.png", new UM_ChartBoostResolver(), () =>
            {
                EditorGUILayout.HelpBox("COMING SOON!", MessageType.Info);
            });
        }

        public override string Title => "Advertisement";
        protected override string Description => "Integrate banner, rewarded and non-rewarded ads for you game, using the supported ads platforms.";
        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_advertisement_icon.png");
        protected override SA_iAPIResolver Resolver => m_ServiceResolver ?? (m_ServiceResolver = new UM_AdsResolver());

        protected override void OnServiceUI()
        {
            m_UnityAdBlock.OnGUI();
            m_AdMobBlock.OnGUI();
            m_ChartboostBlock.OnGUI();
        }

        void DrawAdMobUI()
        {
            if (UM_DefinesResolver.IsAdMobInstalled)
            {
                EditorGUILayout.HelpBox("Google Mobile Ads SDK Installed!", MessageType.Info);
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    ShowImportGoogleMobileAdsSdkButton("Re-Import SDK");
                }

                GUILayout.Space(10);
                DrawAdMobSettings();
            }
            else
            {
                EditorGUILayout.HelpBox("Google Mobile Ads SDK Missing!", MessageType.Warning);
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    ShowImportGoogleMobileAdsSdkButton("Import SDK");
                    var refreshClick = GUILayout.Button("Refresh", EditorStyles.miniButton, GUILayout.Width(120));
                    if (refreshClick) UM_DefinesResolver.ProcessAssets();
                }
            }
        }

        void ShowImportGoogleMobileAdsSdkButton(string buttonName)
        {
            var click = GUILayout.Button(buttonName, EditorStyles.miniButton, GUILayout.Width(120));
            if (click)
            {
                SA_PackageManager.DownloadAndImport("Google Mobile Ads SDK", k_AdMobSdkDownloadUrl, false);
            }
        }

        static void DrawAdMobSettings()
        {
            if (AdMobSettingsLayout == null)
                UM_SettingsUtil.DrawAddonRequestUI(UM_Addon.AdMob);
            else
                AdMobSettingsLayout.OnGUI();
        }

        static IMGUILayoutElement AdMobSettingsLayout
        {
            get
            {

                if (s_AdMobSettingsLayout != null)
                    return s_AdMobSettingsLayout;

                if (s_CreateAdMobLayout != null)
                {
                    s_AdMobSettingsLayout = s_CreateAdMobLayout.Invoke();
                    return s_AdMobSettingsLayout;
                }

                return null;
            }
        }

        static void DrawUnityAdsSettings()
        {
            if (UnityAdsSettingsLayout == null)
                UM_SettingsUtil.DrawAddonRequestUI(UM_Addon.UnityAds);
            else
                UnityAdsSettingsLayout.OnGUI();
        }

        static IMGUILayoutElement UnityAdsSettingsLayout
        {
            get
            {
                if (s_UnityAdsSettingsLayout != null)
                    return s_UnityAdsSettingsLayout;

                if (s_CreateUnityAdsLayout != null)
                {
                    s_UnityAdsSettingsLayout = s_CreateUnityAdsLayout.Invoke();
                    return s_UnityAdsSettingsLayout;
                }

                return null;
            }
        }

        static void DrawUnityAdsUI()
        {
            if (UM_DefinesResolver.IsUnityAdsInstalled)
            {
                DrawUnityAdsSettings();
            }
            else
            {
                EditorGUILayout.HelpBox("Unity SDK Package Missing!", MessageType.Warning);
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    var click = GUILayout.Button("Install Unity Ads", EditorStyles.miniButton, GUILayout.Width(120));
                    if (click)
                    {
                        Client.Add(UM_DefinesResolver.UnityAdsPackageName);
                    }
                }
            }
        }

        public static void DrawPlatformIds(UM_PlatformAdIds platform)
        {
            platform.AppId = EditorGUILayout.TextField("App Id: ", platform.AppId);
            platform.BannerId = EditorGUILayout.TextField("Banner Id: ", platform.BannerId);
            platform.RewardedId = EditorGUILayout.TextField("Rewarded Id: ", platform.RewardedId);
            platform.NonRewardedId = EditorGUILayout.TextField("Non-Rewarded Id: ", platform.NonRewardedId);
        }
    }
}
