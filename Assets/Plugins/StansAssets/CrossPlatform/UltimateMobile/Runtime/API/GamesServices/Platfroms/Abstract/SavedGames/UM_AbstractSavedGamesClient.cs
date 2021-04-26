using SA.Foundation.Templates;
using SA.CrossPlatform.Analytics;

namespace SA.CrossPlatform.GameServices
{
    class UM_AbstractSavedGamesClient
    {
        protected void ReportGameSave(string name, SA_Result result)
        {
            UM_AnalyticsInternal.OnGameSave(name, result);
        }
    }
}
