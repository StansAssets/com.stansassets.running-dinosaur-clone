using StansAssets.Foundation;
using StansAssets.Foundation.Patterns;
using UnityEditor;
using UnityEngine;

namespace SA.CrossPlatform.Editor.Advertisement
{
    public class UM_GoogleMobileAdsSettingsProxy : Singleton<UM_GoogleMobileAdsSettingsProxy>
    {
        const string k_PlaymakerUIClassName = "GoogleMobileAds.Editor.GoogleMobileAdsSettings";
        const string k_MobileAdsSettingsDir = "Assets/GoogleMobileAds";
        const string k_MobileAdsSettingsResDir = "Assets/GoogleMobileAds/Resources";
        const string k_MobileAdsSettingsFile = "Assets/GoogleMobileAds/Resources/GoogleMobileAdsSettings.asset";
        readonly ScriptableObject m_GoogleSettings;

        public UM_GoogleMobileAdsSettingsProxy()
        {
            var settingsType = ReflectionUtility.FindType(k_PlaymakerUIClassName);

            if (!AssetDatabase.IsValidFolder(k_MobileAdsSettingsResDir))
            {
                AssetDatabase.CreateFolder(k_MobileAdsSettingsDir, "Resources");
            }

            m_GoogleSettings = (ScriptableObject) AssetDatabase.LoadAssetAtPath(k_MobileAdsSettingsFile, settingsType);

            if (m_GoogleSettings == null)
            {
                m_GoogleSettings = ScriptableObject.CreateInstance(settingsType);
                AssetDatabase.CreateAsset(m_GoogleSettings, k_MobileAdsSettingsFile);
            }
        }

        public bool IsAdMobEnabled => true;

        public string AdMobAndroidAppId
        {
            get => (string) ReflectionUtility.GetPropertyValue(m_GoogleSettings, "GoogleMobileAdsAndroidAppId");
            set => ReflectionUtility.SetPropertyValue(m_GoogleSettings, "GoogleMobileAdsAndroidAppId", value);
        }

        public string AdMobIOSAppId
        {
            get => (string) ReflectionUtility.GetPropertyValue(m_GoogleSettings, "GoogleMobileAdsIOSAppId");
            set => ReflectionUtility.SetPropertyValue(m_GoogleSettings, "GoogleMobileAdsIOSAppId", value);
        }

        public void Save()
        {
            EditorUtility.SetDirty((UnityEngine.Object) m_GoogleSettings);
        }
    }
}
