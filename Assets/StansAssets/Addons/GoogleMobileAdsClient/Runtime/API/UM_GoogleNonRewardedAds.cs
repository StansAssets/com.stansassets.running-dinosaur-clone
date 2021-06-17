using System;
using SA.Foundation.Templates;
using GoogleMobileAds.Api;

namespace SA.CrossPlatform.Advertisement
{
    public class UM_GoogleNonRewardedAds : UM_GoogleBaseAds, UM_iNonRewardedAds
    {
        InterstitialAd m_Interstitial;
        Action m_ShowCallback;

        public void Load(Action<SA_Result> callback)
        {
            Load(UM_GoogleAdsSettings.Instance.Platform.NonRewardedId, callback);
        }

        public void Load(string id, Action<SA_Result> callback)
        {
            m_LoadCallback = callback;
            m_Interstitial = new InterstitialAd(id);

            m_Interstitial.OnAdLoaded += HandleAdLoaded;
            m_Interstitial.OnAdFailedToLoad += HandleAdFailedToLoad;
            m_Interstitial.OnAdClosed += HandleAdClosed;
            m_Interstitial.LoadAd(UM_GoogleAdsClient.BuildAdRequest());
        }

        public void Show(Action callback)
        {
            m_ShowCallback = callback;
            m_Interstitial.Show();
            m_IsReady = false;
        }

        void HandleAdClosed(object sender, EventArgs e)
        {
            m_ShowCallback?.Invoke();
        }
    }
}
