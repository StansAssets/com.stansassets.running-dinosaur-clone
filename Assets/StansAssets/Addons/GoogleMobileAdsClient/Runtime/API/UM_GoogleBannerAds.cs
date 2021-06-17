using System;
using GoogleMobileAds.Api;
using SA.Foundation.Templates;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.Advertisement
{
    public class UM_GoogleBannerAds : UM_GoogleBaseAds, UM_iBannerAds
    {
        BannerView m_Banner;
        Action m_ShowCallback;

        public void Load(Action<SA_Result> callback)
        {
            Load(UM_GoogleAdsSettings.Instance.Platform.BannerId, callback);
        }

        public void Load(string id, Action<SA_Result> callback)
        {
            AdSize size;
            switch (UM_GoogleAdsSettings.Instance.BannerSize)
            {
                case UM_GoogleBannerSize.Banner:
                    size = AdSize.Banner;
                    break;

                case UM_GoogleBannerSize.IABBanner:
                    size = AdSize.IABBanner;
                    break;

                case UM_GoogleBannerSize.Leaderboard:
                    size = AdSize.Leaderboard;
                    break;

                case UM_GoogleBannerSize.MediumRectangle:
                    size = AdSize.MediumRectangle;
                    break;

                case UM_GoogleBannerSize.SmartBanner:
                    size = AdSize.SmartBanner;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AdPosition position = UM_GoogleAdsSettings.Instance.BannerPosition;

            m_IsReady = false;
            m_LoadCallback = callback;

            m_Banner = new BannerView(id, size, position);

            m_Banner.OnAdLoaded += HandleBannerLoaded;
            m_Banner.OnAdFailedToLoad += HandleAdFailedToLoad;
            m_Banner.OnAdOpening += HandleBannerAdOpened;

            // Load a banner ad.
            m_Banner.LoadAd(UM_GoogleAdsClient.BuildAdRequest());
        }

        public bool IsExists => m_Banner != null;

        public void Show(Action callback)
        {
            m_ShowCallback = callback;
            if (m_Banner == null)
                throw new InvalidOperationException("Show must be called when the banner is null.");

            m_Banner.Show();
        }

        void HandleBannerAdOpened(object sender, EventArgs e)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                m_ShowCallback?.Invoke();
            });
        }

        protected void HandleBannerLoaded(object sender, EventArgs e)
        {
            // the OnAdLoaded callback will also be triggered on banner reload
            // so we need to make sure that we are waiting for a load callback, and ignore reload

            if (m_LoadCallback == null)
                return;

            // We are hiding because it will be showed automatically when loaded
            // but we need to prevent this.
            m_Banner.Hide();
            HandleAdLoaded(sender, e);
        }

        public void Hide()
        {
            m_Banner?.Hide();
        }

        public void Destroy()
        {
            m_IsReady = false;
            m_Banner?.Destroy();
            m_Banner = null;
        }
    }
}
