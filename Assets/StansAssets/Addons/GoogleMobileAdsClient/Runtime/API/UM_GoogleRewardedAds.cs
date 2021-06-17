using System;
using SA.Foundation.Templates;
using GoogleMobileAds.Api;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.Advertisement
{
    public class UM_GoogleRewardedAds : UM_GoogleBaseAds, UM_iRewardedAds
    {
        RewardedAd m_RewardedVideo;
        Action<UM_RewardedAdsResult> m_ShowCallback;

        bool m_IsInitialized;
        bool m_IsRewarded;
        string m_UserId;

        public void Load(Action<SA_Result> callback)
        {
            Load(UM_GoogleAdsSettings.Instance.Platform.RewardedId, callback);
        }

        public void Load(string id, Action<SA_Result> callback)
        {
            m_LoadCallback = callback;

            if (!m_IsInitialized)
            {
                m_IsInitialized = true;
                m_RewardedVideo = new RewardedAd(id);
                m_RewardedVideo.OnAdLoaded += HandleAdLoaded;
                m_RewardedVideo.OnAdFailedToLoad += HandleAdFailedToLoad;

                m_RewardedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
                m_RewardedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            }

            m_RewardedVideo.LoadAd(UM_GoogleAdsClient.BuildAdRequest());
        }

        public void SetUserId(string id)
        {
            m_UserId = id;
        }

        public void Show(Action<UM_RewardedAdsResult> callback)
        {
            m_IsReady = false;
            m_IsRewarded = false;
            m_ShowCallback = callback;
            m_RewardedVideo.Show();
        }

        void HandleRewardBasedVideoRewarded(object sender, Reward e)
        {
            m_IsRewarded = true;
        }

        void HandleRewardBasedVideoClosed(object sender, EventArgs e)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                m_ShowCallback.Invoke(m_IsRewarded ? UM_RewardedAdsResult.Finished : UM_RewardedAdsResult.Skipped);

                m_IsRewarded = false;
                m_ShowCallback = null;
            });
        }
    }
}
