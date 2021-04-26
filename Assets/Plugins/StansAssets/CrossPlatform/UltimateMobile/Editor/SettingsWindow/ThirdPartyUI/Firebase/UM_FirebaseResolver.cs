using SA.Android.Firebase.Analytics;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_FirebaseResolver : SA_iAPIResolver
    {
        public bool IsSettingsEnabled {
            get => AN_FirebaseAnalytics.IsAvailable;

            set { }
        }

        public void ResetRequirementsCache() { }
    }
}
