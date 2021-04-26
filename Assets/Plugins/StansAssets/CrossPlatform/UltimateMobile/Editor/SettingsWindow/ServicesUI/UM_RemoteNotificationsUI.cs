using UnityEngine;
using SA.Android;
using SA.iOS;
using SA.iOS.XCode;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_RemoteNotificationsUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_UserNotificationsUI>();

            public override bool IsEnabled => ISD_API.Capability.PushNotifications.Enabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
        }

        public override string Title => "Remote Notifications";

        protected override string Description => "Supports the delivery and handling of remote notifications.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_remote_notifications_icon.png");

        protected override void OnServiceUI() { }
    }
}
