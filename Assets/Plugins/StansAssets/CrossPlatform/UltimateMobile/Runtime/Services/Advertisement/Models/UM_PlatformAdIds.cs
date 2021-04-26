using System;

namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// Advertisement platform Ids config object.
    /// </summary>
    [Serializable]
    public class UM_PlatformAdIds
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        public string AppId = string.Empty;

        /// <summary>
        /// Banner advertisement id.
        /// </summary>
        public string BannerId = string.Empty;

        /// <summary>
        /// Non Rewarded advertisement id.
        /// </summary>
        public string NonRewardedId = string.Empty;

        /// <summary>
        /// Rewarded advertisement id.
        /// </summary>
        public string RewardedId = string.Empty;
    }
}
