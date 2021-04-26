using UnityEngine;
using SA.Android;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_FoundationUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_FoundationUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_FoundationResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_AppFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_CoreResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Introduction", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Introduction");

            AddFeatureUrl("Plugin Editor UI", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Plugin-Editor-UI");
            AddFeatureUrl("3rd-Party Tab", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/3rd-Party-Tab");
            AddFeatureUrl("Summary Tab", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Summary-Tab");

            AddFeatureUrl("Native Dialogs", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Native-Dialogs");
            AddFeatureUrl("Native Preloader", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Native-Preloader");
            AddFeatureUrl("Rate Us Dialog", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Rate-Us-Dialog");
            AddFeatureUrl("Dialogs Utility ", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Native-Dialogs#utility");
            AddFeatureUrl("Date Picker Dialog", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Date-Picker-Dialog");
            AddFeatureUrl("Wheel Picker Dialog", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Wheel-Picker-Dialog");

            AddFeatureUrl("Build Info", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Build-Info");
            AddFeatureUrl("Locale Info", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Locale");
            AddFeatureUrl("Permissions", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Permissions");

            AddFeatureUrl("Dark Mode", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Dark-Mode");
            AddFeatureUrl("Send To Background", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Application#send-to-background");
        }

        public override string Title => "Foundation";

        protected override string Description => "Access operation-system services to define the base layer of functionality for your app.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_system_icon.png");

        protected override void OnServiceUI() { }
    }
}
