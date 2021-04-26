using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.Advertisement
{
    class UM_EditorAdsClient : UM_AbstractAdsClient, UM_iAdsClient
    {
        public void Initialize(Action<SA_Result> callback)
        {
            Initialize("editor_client_id", callback);
        }

        protected override void ConnectToService(string appId, Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                callback.Invoke(new SA_Result());
            });
        }

        public UM_iBannerAds Banner => m_Banner ?? (m_Banner = new UM_EditorBannerAds());
        public UM_iRewardedAds RewardedAds => m_RewardedAds ?? (m_RewardedAds = new UM_EditorRewardedAds());
        public UM_iNonRewardedAds NonRewardedAds => m_NonRewardedAds ?? (m_NonRewardedAds = new UM_EditorNonRewardedAds());

        public void SetUserId(string id)
        {
           // Do nothing
        }
    }
}
