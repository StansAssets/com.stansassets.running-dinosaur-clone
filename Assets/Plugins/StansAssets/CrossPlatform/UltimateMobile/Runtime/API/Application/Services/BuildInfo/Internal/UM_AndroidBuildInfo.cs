using SA.Android.App;

namespace SA.CrossPlatform.App
{
    class UM_AndroidBuildInfo : UM_AbstractBuildInfo, UM_iBuildInfo
    {
        public override string Version
        {
            get
            {
                var pm = AN_MainActivity.Instance.GetPackageManager();
                var packageInfo = pm.GetPackageInfo(Identifier, 0);
                return packageInfo.VersionName;
            }
        }
    }
}
