using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// A base client to interact advertisement.
    /// </summary>
    public interface UM_IAdvertisement
    {
        /// <summary>
        /// Indicates if banner ad is ready to be shown
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Load rewarded ads.
        /// Plugin will use rewarded ads id configured from the settings.
        /// </summary>
        /// <param name="callback">Callback with the load result.</param>
        void Load(Action<SA_Result> callback);

        /// <summary>
        /// Load non-rewarded ads.
        /// </summary>
        /// <param name="id">Rewarded ads id from the ad provider dashboard.</param>
        /// <param name="callback">Callback with the load result.</param>
        void Load(string id, Action<SA_Result> callback);
    }
}
