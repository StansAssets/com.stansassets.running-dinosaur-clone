using System;
using UnityEngine;

namespace SA.CrossPlatform.Advertisement
{
    class UM_EditorRewardedAds : UM_EditorBaseAds, UM_iRewardedAds
    {
        public void Show(Action<UM_RewardedAdsResult> callback)
        {
            if (!IsReady)
            {
                const string message = "Failed to show rewarded, content is not ready yet!";
                Debug.LogError(message);
                throw new InvalidOperationException(message);
            }

            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                callback.Invoke(UM_RewardedAdsResult.Finished);
                m_IsReady = false;
            });
        }
    }
}
