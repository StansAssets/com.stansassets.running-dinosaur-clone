using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// Base class to simplify an implementation of the advertisement plugin client.
    /// </summary>
    public abstract class UM_AbstractAdsClient
    {
        bool m_IsConnectionInProgress;

        protected UM_iBannerAds m_Banner;
        protected UM_iRewardedAds m_RewardedAds;
        protected UM_iNonRewardedAds m_NonRewardedAds;

        event Action<SA_Result> OnConnect = delegate { };

        /// <summary>
        /// Returns `true` if plugin is initialized and `false` otherwise.
        /// </summary>
        public bool IsInitialized { get; set; }

        //--------------------------------------
        //  Abstract
        //--------------------------------------

        protected abstract void ConnectToService(string appId, Action<SA_Result> callback);

        //--------------------------------------
        //  Public Methods
        //--------------------------------------

        public void Initialize(string appId, Action<SA_Result> callback)
        {
            if (IsInitialized)
            {
                callback.Invoke(new SA_Result());
                return;
            }

            OnConnect += callback;
            if (m_IsConnectionInProgress)
                return;

            m_IsConnectionInProgress = true;

            ConnectToService(appId, (result) =>
            {
                IsInitialized = true;
                m_IsConnectionInProgress = false;
                OnConnect.Invoke(result);
                OnConnect = delegate { };
            });
        }
    }
}
