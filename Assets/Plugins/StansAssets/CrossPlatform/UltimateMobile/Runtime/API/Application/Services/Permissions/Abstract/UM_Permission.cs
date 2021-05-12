using System;
using SA.Android.App;
using SA.Android.Content.Pm;
using SA.Android.Manifest;
using UnityEngine;

namespace SA.CrossPlatform.App
{
    abstract class UM_Permission : UM_IPermission
    {
        protected abstract UM_AuthorizationStatus IOSAuthorization { get; }
        protected abstract void IOSRequestAccess(Action<UM_AuthorizationStatus> callback);
        protected abstract AMM_ManifestPermission[] AndroidPermissions { get; }

        public UM_AuthorizationStatus Authorization
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        var granted = true;
                        foreach (var permission in AndroidPermissions)
                        {
                            var state = AN_PermissionsManager.CheckSelfPermission(permission);
                            if (state == AN_PackageManager.PermissionState.Denied)
                            {
                                granted = false;
                                break;
                            }
                        }

                        return granted ? UM_AuthorizationStatus.Granted : UM_AuthorizationStatus.Denied;

                    case RuntimePlatform.IPhonePlayer:
                        return IOSAuthorization;
                    default:
                        return UM_AuthorizationStatus.Granted;
                }
            }
        }

        public void RequestAccess(Action<UM_AuthorizationStatus> callback)
        {
            if (Authorization == UM_AuthorizationStatus.Granted)
            {
                callback.Invoke(UM_AuthorizationStatus.Granted);
                return;
            }

            StartRequestAccessFlow(callback);
        }

        void StartRequestAccessFlow(Action<UM_AuthorizationStatus> callback)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    IOSRequestAccess(callback);
                    break;
                case RuntimePlatform.Android:
                    AN_PermissionsUtility.TryToResolvePermission(AndroidPermissions, (granted) =>
                    {
                        if (granted)
                            callback.Invoke(UM_AuthorizationStatus.Granted);
                        else
                            callback.Invoke(UM_AuthorizationStatus.Denied);
                    });
                    break;
                default:
                    UM_EditorAPIEmulator.WaitForNetwork(() => { callback.Invoke(UM_AuthorizationStatus.Granted); });
                    break;
            }
        }
    }
}
