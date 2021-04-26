////////////////////////////////////////////////////////////////////////////////
//
// @module IOS Native Plugin
// @author Osipov Stanislav (Stan's Assets)
// @support support@stansassets.com
// @website https://stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using SA.Foundation.Patterns;
using SA.Foundation.Config;
using SA.iOS.UIKit;
using SA.iOS.GameKit;
using SA.iOS.StoreKit;
using SA.iOS.Utilities;
using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_Settings : SA_ScriptableSingleton<ISN_Settings>
    {
        public const string PluginTittle = "IOS Native";
        public const string DocumentationUrl = "https://unionassets.com/ios-native-pro/manual";
        public const string IOSNativeFolder = SA_Config.StansAssetsNativePluginsPath + "IOSNativePro/";

        const string k_IOSNativeApi = IOSNativeFolder + "API/";
        public const string IOSNativeXcode = IOSNativeFolder + "XCode/";
        public const string ContactsApiLocation = k_IOSNativeApi + "Contacts/Internal/";
        public const string TestScenePath = IOSNativeFolder + "Tests/Scene/ISN_TestScene.unity";

        //--------------------------------------
        // API Settings
        //--------------------------------------

        public bool Contacts = false;
        public bool CloudKit = false;
        public bool Photos = false;
        public bool ReplayKit = false;
        public bool Social = false;
        public bool AdSupport = false;
        public bool AppTrackingTransparency = false;
        
        // ReSharper disable once InconsistentNaming
        public bool AVKit = false;
        public bool CoreLocation = false;
        public bool AssetsLibrary = false;
        public bool AppDelegate = false;
        public bool UserNotifications = false;
        public bool MediaPlayer = false;
        public bool EventKit = false;

        internal readonly ISN_LogLevel LogLevel = new ISN_LogLevel();

        //--------------------------------------
        // StoreKit Settings
        //--------------------------------------

        public List<ISN_SKProduct> InAppProducts = new List<ISN_SKProduct>();

        //--------------------------------------
        // GameKit Settings
        //--------------------------------------

        public List<ISN_GKAchievement> Achievements = new List<ISN_GKAchievement>();
        public bool SavingAGame = false;

        //--------------------------------------
        // App Delegate Settings
        //--------------------------------------

        public List<ISN_UIApplicationShortcutItem> ShortcutItems = new List<ISN_UIApplicationShortcutItem>();
        public List<ISN_UIUrlType> UrlTypes = new List<ISN_UIUrlType>();

        //--------------------------------------
        // UIKit Settings
        //--------------------------------------

        public List<ISN_UIUrlType> ApplicationQueriesSchemes = new List<ISN_UIUrlType>();

        public bool CameraUsageDescriptionEnabled = true;

        public string CameraUsageDescription
        {
            get => GetPlistKeyValue("NSCameraUsageDescription", "Please change 'Camera Usage Description' with IOS Native UI Kit Editor Settings", CameraUsageDescriptionEnabled);

            set => SetPlistKeyValue("NSCameraUsageDescription", value, CameraUsageDescriptionEnabled);
        }

        public bool MediaLibraryUsageDescriptionEnabled = false;

        public string MediaLibraryUsageDescription
        {
            get => GetPlistKeyValue("NSAppleMusicUsageDescription", "Please change 'Media Library Usage Description' with IOS Native Media Player Editor Settings", PhotoLibraryUsageDescriptionEnabled);

            set => SetPlistKeyValue("NSAppleMusicUsageDescription", value, PhotoLibraryUsageDescriptionEnabled);
        }

        public bool PhotoLibraryUsageDescriptionEnabled = true;

        public string PhotoLibraryUsageDescription
        {
            get => GetPlistKeyValue("NSPhotoLibraryUsageDescription", "Please change 'Photo Library Usage Description' with IOS Native UI Kit Editor Settings", PhotoLibraryUsageDescriptionEnabled);

            set => SetPlistKeyValue("NSPhotoLibraryUsageDescription", value, PhotoLibraryUsageDescriptionEnabled);
        }

        public bool PhotoLibraryAddUsageDescriptionEnabled = true;

        public string PhotoLibraryAddUsageDescription
        {
            get => GetPlistKeyValue("NSPhotoLibraryAddUsageDescription", "Please change 'Photo Library Add Usage Description' with IOS Native UI Kit Editor Settings", PhotoLibraryAddUsageDescriptionEnabled);

            set => SetPlistKeyValue("NSPhotoLibraryAddUsageDescription", value, PhotoLibraryAddUsageDescriptionEnabled);
        }

        public bool MicrophoneUsageDescriptionEnabled = true;

        public string MicrophoneUsageDescription
        {
            get => GetPlistKeyValue("NSMicrophoneUsageDescription", "Please change 'Microphone Usage Description' with IOS Native UI Kit Editor Settings", MicrophoneUsageDescriptionEnabled);

            set => SetPlistKeyValue("NSMicrophoneUsageDescription", value, MicrophoneUsageDescriptionEnabled);
        }

        string GetPlistKeyValue(string key, string defaultValue, bool enabled)
        {
            if (!enabled) return defaultValue;
            var plistKey = ISD_API.GetInfoPlistKey(key);
            if (plistKey == null)
            {
                plistKey = new ISD_PlistKey();
                plistKey.Name = key;
                plistKey.StringValue = defaultValue;
                plistKey.Type = ISD_PlistKeyType.String;
                ISD_API.SetInfoPlistKey(plistKey);
            }

            return plistKey.StringValue;
        }

        void SetPlistKeyValue(string key, string val, bool enabled)
        {
            if (!enabled)
                return;

            if (!val.Equals(GetPlistKeyValue(key, val, true)))

                // We are sure it's not null.
                ISD_API.GetInfoPlistKey(key).StringValue = val;
        }

        //--------------------------------------
        // Contacts Settings
        //--------------------------------------

        public string ContactsUsageDescription = "Please change 'Contacts Usage Description' with IOS Native Contacts Editor Settings";

        //--------------------------------------
        // Core Location
        //--------------------------------------

        public string LocationAlwaysAndWhenInUseUsageDescription = "Please change 'Location Always And When In Use Usage Description' with IOS Native Core Location Editor Settings";
        public string LocationWhenInUseUsageDescription = "Please change 'Location When In Use Usage Description' with IOS Native Core Location Editor Settings";

        //--------------------------------------
        // Event Kit
        //--------------------------------------

        public string NsCalendarsUsageDescription = "This app is require access your Calendar";
        public string NsRemindersUsageDescription = "This app is require access to your Reminder";

        //--------------------------------------
        // pp Tracking Transparency
        //--------------------------------------

        public string UserTrackingUsageDescription = "Please change 'UserTrackingUsageDescription' with IOS Native App Tracking Transparency Editor Settings";

        //--------------------------------------
        // SA_ScriptableSettings
        //--------------------------------------

        protected override string BasePath => IOSNativeFolder;
        public override string PluginName => PluginTittle;
        public override string DocumentationURL => DocumentationUrl;
        public override string SettingsUIMenuItem => SA_Config.EditorMenuRoot + "iOS/Services";
    }
}
