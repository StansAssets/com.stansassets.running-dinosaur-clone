using System;
using UnityEngine;

namespace SA.CrossPlatform.Advertisement
{
    class UM_EditorNonRewardedAds : UM_EditorBaseAds, UM_iNonRewardedAds
    {
        public void Show(Action callback)
        {
            if (!IsReady)
            {
                const string message = "Failed to show non-rewarded, contnet is not ready yet!";
                Debug.LogError(message);
                throw new InvalidOperationException(message);
            }

            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                callback.Invoke();
                m_IsReady = false;
            });
        }
    }
}
