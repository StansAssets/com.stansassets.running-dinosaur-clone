using System;
using SA.Android.Manifest;
using SA.iOS.Contacts;

namespace SA.CrossPlatform.App
{
    class UM_ContactsPermission : UM_Permission
    {
        protected override UM_AuthorizationStatus IOSAuthorization
        {
            get
            {
                var status = ISN_CNContactStore.GetAuthorizationStatus(ISN_CNEntityType.Contacts);
                switch (status)
                {
                    case ISN_CNAuthorizationStatus.Authorized:
                        return UM_AuthorizationStatus.Granted;
                    default:
                        return UM_AuthorizationStatus.Denied;
                }
            }
        }

        protected override void IOSRequestAccess(Action<UM_AuthorizationStatus> callback)
        {
            ISN_CNContactStore.RequestAccess(ISN_CNEntityType.Contacts, (result) =>
            {
                if (result.IsSucceeded)
                    callback.Invoke(UM_AuthorizationStatus.Granted);
                else
                    callback.Invoke(UM_AuthorizationStatus.Denied);
            });
        }

        protected override AMM_ManifestPermission[] AndroidPermissions
        {
            get { return new[] { AMM_ManifestPermission.READ_CONTACTS }; }
        }
    }
}
