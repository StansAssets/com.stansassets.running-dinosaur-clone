using SA.Android.Editor;
using SA.Android.Manifest;
using SA.CrossPlatform.Advertisement;
using UnityEditor;

namespace SA.CrossPlatform.Editor.Advertisement
{
    public class UM_AndroidAdMobResolver : AN_APIResolver
    {
        [InitializeOnLoadMethod]
        private static void ResolverRegistration()
        {
            AN_Preprocessor.RegisterResolver(new UM_AndroidAdMobResolver());
        }

        public override bool IsSettingsEnabled
        {
            get => true;
            set { }
        }

        protected override void AppendBuildRequirements(AN_AndroidBuildRequirements buildRequirements)
        {
            var androidAppId = UM_GoogleAdsSettings.Instance.AndroidIds.AppId;
            if (string.IsNullOrEmpty(androidAppId)) return;

            var applicationId = new AMM_PropertyTemplate("meta-data");
            applicationId.SetValue("android:name", "com.google.android.gms.ads.APPLICATION_ID");
            applicationId.SetValue("android:value", androidAppId);
            buildRequirements.AddApplicationProperty(applicationId);
        }
    }
}

