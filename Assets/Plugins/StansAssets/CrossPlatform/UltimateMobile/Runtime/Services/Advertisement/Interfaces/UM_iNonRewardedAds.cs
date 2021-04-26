using System;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// A client to interact with the non-rewarded advertisement.
    /// </summary>
    public interface UM_iNonRewardedAds : UM_IAdvertisement
    {
        /// <summary>
        /// Show non-rewarded ads.
        /// </summary>
        /// <param name="callback">Callback is called when ad is closed.</param>
        void Show(Action callback);
    }
}
