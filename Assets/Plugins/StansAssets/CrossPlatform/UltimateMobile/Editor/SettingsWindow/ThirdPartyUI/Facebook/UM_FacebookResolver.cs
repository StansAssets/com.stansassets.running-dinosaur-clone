using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_FacebookResolver : SA_iAPIResolver
    {
        public bool IsSettingsEnabled
        {
            get => IsFacebookPackageInstalled;

            set { }
        }

        public void ResetRequirementsCache() { }

        public static bool IsFacebookPackageInstalled
        {
            get
            {
#if SA_FACEBOOK
                return false;
#else
                return false;
#endif
            }
        }
    }
}
