using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_AnalyticsResolver : SA_iAPIResolver
    {
        public bool IsSettingsEnabled
        {
            get => true;
            set { }
        }

        public void ResetRequirementsCache() { }
    }
}
