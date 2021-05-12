using SA.iOS.Foundation;

namespace SA.CrossPlatform.App
{
    class UM_IOSBuildInfo : UM_AbstractBuildInfo, UM_iBuildInfo
    {
        public override string Version
        {
            get
            {
                var buildInfo = ISN_NSBundle.BuildInfo;
                return buildInfo.AppVersion;
            }
        }
    }
}
