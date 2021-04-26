using System;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// A client to interact with the rewarded advertisement.
    /// </summary>
    public interface UM_iRewardedAds : UM_IAdvertisement
    {
        /// <summary>
        /// Show rewarded ads.
        /// </summary>
        /// <param name="callback">Callback is called when ad is closed.</param>
        void Show(Action<UM_RewardedAdsResult> callback);
    }
}
