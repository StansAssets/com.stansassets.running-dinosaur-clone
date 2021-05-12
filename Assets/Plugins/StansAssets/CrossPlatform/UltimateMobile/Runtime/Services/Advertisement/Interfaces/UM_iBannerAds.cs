using System;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// A client to interact with the banner advertisement.
    /// </summary>
    public interface UM_iBannerAds : UM_IAdvertisement
    {
        /// <summary>
        /// Show banner. Make sure to only call this method when the ad is ready.
        /// </summary>
        /// <param name="callback">Callback is called when banner is shown.</param>
        void Show(Action callback);

        /// <summary>
        /// Hides banner active banner. Should only be called when active banner exists.
        /// </summary>
        void Hide();

        /// <summary>
        /// Destroy banner instance. Make sure that banner instance exists before calling this method.
        /// </summary>
        void Destroy();
    }
}
