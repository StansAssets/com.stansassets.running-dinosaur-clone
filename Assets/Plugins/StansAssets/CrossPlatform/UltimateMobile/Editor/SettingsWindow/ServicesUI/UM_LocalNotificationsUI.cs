using UnityEngine;
using SA.Android;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_LocalNotificationsUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_UserNotificationsUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_UserNotificationsResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_LocalNotificationsFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_LocalNotificationsResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Local-Notifications)");
            AddFeatureUrl("Scheduling", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Scheduling-Notifications");
            AddFeatureUrl("Canceling", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Responding-to-Notification#canceling-notifications");
            AddFeatureUrl("Responding", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Responding-to-Notification");
            AddFeatureUrl("Application Badges", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/ApplicationIcon-Badge-Number");
        }

        public override string Title => "Local Notifications";

        protected override string Description => "Supports the delivery and handling of local notifications.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_notification_icon.png");

        protected override void OnServiceUI() { }
    }
}
