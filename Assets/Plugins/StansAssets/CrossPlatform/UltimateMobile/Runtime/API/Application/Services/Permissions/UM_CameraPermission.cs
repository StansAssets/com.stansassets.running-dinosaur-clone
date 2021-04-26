using System;
using SA.Android.Manifest;
using SA.iOS.AVFoundation;

namespace SA.CrossPlatform.App
{
    class UM_CameraPermission : UM_Permission
    {
        protected override UM_AuthorizationStatus IOSAuthorization
        {
            get
            {
                var status = ISN_AVCaptureDevice.GetAuthorizationStatus(ISN_AVMediaType.Video);
                switch (status)
                {
                    case ISN_AVAuthorizationStatus.Authorized:
                        return UM_AuthorizationStatus.Granted;
                    default:
                        return UM_AuthorizationStatus.Denied;
                }
            }
        }

        protected override void IOSRequestAccess(Action<UM_AuthorizationStatus> callback)
        {
            ISN_AVCaptureDevice.RequestAccess(ISN_AVMediaType.Video, (status) =>
            {
                if (status == ISN_AVAuthorizationStatus.Authorized)
                    callback.Invoke(UM_AuthorizationStatus.Granted);
                else
                    callback.Invoke(UM_AuthorizationStatus.Denied);
            });
        }

        protected override AMM_ManifestPermission[] AndroidPermissions
        {
            get { return new[] { AMM_ManifestPermission.CAMERA, AMM_ManifestPermission.WRITE_EXTERNAL_STORAGE, AMM_ManifestPermission.READ_EXTERNAL_STORAGE }; }
        }
    }
}
