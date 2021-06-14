using System;
using SA.CrossPlatform.Advertisement;
using UnityEngine;

namespace StansAssets.ProjectSample.Ads
{
    public class AdsManager
    {
        UM_iAdsClient m_Client;
        UM_AdPlatform m_Platform;

        public AdsManager()
        {
            Platform = UM_AdPlatform.Google;

            // var settings = UM_GoogleAdsSettings.Instance;
            // var android = settings.AndroidIds;
            //
            // android.AppId = "ca-app-pub-6537319135845708~1792712521";
            // android.BannerId = "ca-app-pub-6537319135845708/4569003383";
            // android.RewardedId = "ca-app-pub-6537319135845708/7003595030";
        }

        public UM_AdPlatform Platform {
            get => m_Platform;
            set {
                m_Platform = value;
                m_Client = UM_AdvertisementService.GetClient(Platform);
            }
        }

        public void ShowRewardedAds(Action<bool> callback)
        {
            m_Client.RewardedAds.Load(result =>
            {
                if (result.IsSucceeded)
                {
                    Debug.Log("Rewarded ad loaded");
                    m_Client.RewardedAds.Show(
                                              adsResult => {

                                              });
                }
                else
                {
                    Debug.Log("Failed to load banner ads: " + result.Error.Message);
                }
            });
        }

        public void ShowBanner(Action callback)
        {
            m_Client.Banner.Load(result =>
            {
                if (result.IsSucceeded)
                {
                    Debug.Log("Banner ad loaded");
                    m_Client.Banner.Show(callback);
                }
                else
                {
                    Debug.Log("Failed to load banner ads: " + result.Error.Message);
                }
            });
        }

        public void HideBanner()
        {
            m_Client.Banner.Destroy();
        }
    }
}
