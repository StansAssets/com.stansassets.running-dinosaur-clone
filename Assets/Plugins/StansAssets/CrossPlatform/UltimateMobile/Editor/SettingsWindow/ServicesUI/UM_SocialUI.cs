using System.Collections.Generic;
using UnityEngine;
using SA.Android;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_SocialUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_SocialUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_SocialResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_SocialFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_SocialResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Native Sharing", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Native-Sharing");
            AddFeatureUrl("Facebook", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Facebook");
            AddFeatureUrl("Twitter", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Twitter");
            AddFeatureUrl("Instagram", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Instagram");
            AddFeatureUrl("Whatsapp", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Whatsapp");
            AddFeatureUrl("E-mail", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/E-mail");
        }

        public override string Title => "Social";

        protected override string Description => "Integrate your app with supported social networking services.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_social_icon.png");

        protected override void OnServiceUI() { }
    }
}
