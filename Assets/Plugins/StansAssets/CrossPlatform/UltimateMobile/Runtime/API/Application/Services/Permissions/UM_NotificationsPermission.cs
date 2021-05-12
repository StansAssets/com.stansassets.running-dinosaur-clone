using System;
using SA.Android.Manifest;
using SA.iOS.UserNotifications;

namespace SA.CrossPlatform.App
{
    class UM_NotificationsPermission : UM_Permission
    {
        protected override UM_AuthorizationStatus IOSAuthorization => UM_AuthorizationStatus.Denied;

        protected override void IOSRequestAccess(Action<UM_AuthorizationStatus> callback)
        {
            var options = ISN_UNAuthorizationOptions.Alert | ISN_UNAuthorizationOptions.Sound;
            ISN_UNUserNotificationCenter.RequestAuthorization(options, (result) =>
            {
                if (result.IsSucceeded)
                    callback.Invoke(UM_AuthorizationStatus.Granted);
                else
                    callback.Invoke(UM_AuthorizationStatus.Denied);
            });
        }

        protected override AMM_ManifestPermission[] AndroidPermissions
        {
            get { return new[] { AMM_ManifestPermission.WAKE_LOCK }; }
        }
    }
}
