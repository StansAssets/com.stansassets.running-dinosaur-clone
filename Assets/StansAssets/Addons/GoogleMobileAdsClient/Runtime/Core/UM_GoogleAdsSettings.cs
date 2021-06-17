using UnityEngine;
using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using SA.Foundation.Patterns;
using SA.Foundation.Config;

namespace SA.CrossPlatform.Advertisement
{
    public class UM_GoogleAdsSettings : SA_ScriptableSingleton<UM_GoogleAdsSettings>
    {
        public UM_PlatformAdIds AndroidIds;
        public UM_PlatformAdIds IOSIds;

        public List<string> TestDevices = new List<string>();
        public List<string> Keywords = new List<string>();
        public bool TagForChildDirectedTreatment = false;
        public bool NPA = true;
        public AdPosition BannerPosition = AdPosition.Bottom;
        public UM_GoogleBannerSize BannerSize = UM_GoogleBannerSize.SmartBanner;
        public DateTime Birthday;
        public Gender Gender = Gender.Unknown;

        public UM_PlatformAdIds Platform
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return AndroidIds;
                    case RuntimePlatform.IPhonePlayer:
                        return IOSIds;
                    default:
                        return new UM_PlatformAdIds();
                }
            }
        }

        //--------------------------------------
        // SA_ScriptableSettings
        //--------------------------------------

        public override string PluginName => "UM Google AdMob";
        public override string DocumentationURL => "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Google-AdMob";
        public override string SettingsUIMenuItem => SA_Config.EditorMenuRoot + "Cross-Platform/3rd-Party";
        protected override string BasePath => string.Empty;
    }
}
