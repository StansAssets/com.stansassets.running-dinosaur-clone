using System.Collections.Generic;

#if UNITY_ANALYTICS
using System;
using UnityAnalytics = UnityEngine.Analytics;
#endif

namespace SA.CrossPlatform.Analytics
{
    class UM_UnityAnalyticsClient : UM_BaseAnalyticsClient, UM_IAnalyticsClient
    {
        public UM_UnityAnalyticsClient()
        {
#if UNITY_ANALYTICS
            var unityClient = UM_Settings.Instance.Analytics.UnityClient;

            UnityAnalytics.Analytics.limitUserTracking = unityClient.LimitUserTracking;
            UnityAnalytics.Analytics.deviceStatsEnabled = unityClient.DeviceStatsEnabled;
#endif
        }

        public void Event(string eventName)
        {
#if UNITY_ANALYTICS
            UnityAnalytics.Analytics.CustomEvent(eventName);
#endif
        }

        public void Event(string eventName, IDictionary<string, object> data)
        {
#if UNITY_ANALYTICS
            UnityAnalytics.Analytics.CustomEvent(eventName, data);
#endif
        }

        public void Transaction(string productId, float amount, string currency)
        {
#if UNITY_ANALYTICS
            //float(32 bit) to decimal(128bit) is safe
            //decimal(128bit) to float(32 bit) is not
            UnityAnalytics.Analytics.Transaction(productId, Convert.ToDecimal(amount), currency);
#endif
        }

        public void SetUserId(string userId)
        {
#if UNITY_ANALYTICS
            UnityAnalytics.Analytics.SetUserId(userId);
#endif
        }

        public void SetUserBirthYear(int birthYear)
        {
#if UNITY_ANALYTICS
            UnityAnalytics.Analytics.SetUserBirthYear(birthYear);
#endif
        }

        public void SetUserGender(UM_Gender gender)
        {
#if UNITY_ANALYTICS
            var unityGender = UnityAnalytics.Gender.Unknown;
            switch (gender)
            {
                case UM_Gender.Male:
                    unityGender = UnityAnalytics.Gender.Male;
                    break;
                case UM_Gender.Female:
                    unityGender = UnityAnalytics.Gender.Female;
                    break;
            }

            UnityAnalytics.Analytics.SetUserGender(unityGender);
#endif
        }
    }
}
