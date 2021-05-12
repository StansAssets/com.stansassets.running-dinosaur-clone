#if SA_FACEBOOK
using System.Collections.Generic;
using StansAssets.Facebook;

namespace SA.CrossPlatform.Analytics
{
    class UM_FacebookAnalyticsClient : UM_BaseAnalyticsClient, UM_IAnalyticsClient
    {
        public void Event(string eventName)
        {
            FbAnalytics.LogAppEvent(eventName);
        }

        public void Event(string eventName, IDictionary<string, object> data)
        {
            var fbData = (Dictionary<string, object>)data;
            FbAnalytics.LogAppEvent(eventName, null, fbData);
        }

        public void Transaction(string productId, float amount, string currency)
        {
            var data = new Dictionary<string, object>();
            data.Add("productId", productId);

            FbAnalytics.LogPurchase(amount, currency, data);
        }

        public void SetUserId(string userId)
        {
            //Not supported
        }

        public void SetUserBirthYear(int birthYear)
        {
            //Not supported
        }

        public void SetUserGender(UM_Gender gender)
        {
            //Not supported
        }
    }
}
#endif