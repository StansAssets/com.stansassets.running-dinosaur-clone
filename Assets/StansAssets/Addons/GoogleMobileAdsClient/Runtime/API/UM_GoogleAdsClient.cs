using System;
using UnityEngine;
using SA.Foundation.Templates;
using GoogleMobileAds.Api;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.Advertisement
{
    class UM_GoogleAdsClient : UM_AbstractAdsClient, UM_iAdsClient
    {
        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitializeOnLoadMethod()
        {
            UM_AdsClientProxy.RegisterGoogleAdsClient(new UM_GoogleAdsClient());
        }

        public void Initialize(Action<SA_Result> callback)
        {
            Initialize(UM_GoogleAdsSettings.Instance.Platform.AppId, callback);
        }

        public void SetUserId(string id)
        {
            (RewardedAds as UM_GoogleRewardedAds)?.SetUserId(id);
        }

        protected override void ConnectToService(string appId, Action<SA_Result> callback)
        {
            MainThreadDispatcher.Init();
            MobileAds.Initialize(status =>
            {
                callback.Invoke(new SA_Result());
            });
        }

        public static AdRequest BuildAdRequest()
        {
            var builder = new AdRequest.Builder();

            // foreach (var deviceId in UM_GoogleAdsSettings.Instance.TestDevices)
            // {
            //     builder.AddTestDevice(deviceId);
            // }

            foreach (var keyword in UM_GoogleAdsSettings.Instance.Keywords)
            {
                builder.AddKeyword(keyword);
            }

            //builder.TagForChildDirectedTreatment(UM_GoogleAdsSettings.Instance.TagForChildDirectedTreatment);

            if (UM_GoogleAdsSettings.Instance.NPA)
            {
                builder.AddExtra("npa", "1");
            }

            // if (UM_GoogleAdsSettings.Instance.Gender != Gender.Unknown)
            // {
            //     builder.SetGender(UM_GoogleAdsSettings.Instance.Gender);
            // }

            // if (UM_GoogleAdsSettings.Instance.Birthday != DateTime.MinValue)
            // {
            //     builder.SetBirthday(UM_GoogleAdsSettings.Instance.Birthday);
            // }

            return builder.Build();
        }

        public UM_iBannerAds Banner => m_Banner ?? (m_Banner = new UM_GoogleBannerAds());
        public UM_iRewardedAds RewardedAds => m_RewardedAds ?? (m_RewardedAds = new UM_GoogleRewardedAds());
        public UM_iNonRewardedAds NonRewardedAds => m_NonRewardedAds ?? (m_NonRewardedAds = new UM_GoogleNonRewardedAds());
    }
}
