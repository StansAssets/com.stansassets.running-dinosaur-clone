using UnityEngine;
using System;
using GoogleMobileAds.Api;
using SA.Foundation.Templates;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.Advertisement
{
    public abstract class UM_GoogleBaseAds
    {
        protected Action<SA_Result> m_LoadCallback;
        protected bool m_IsReady;

        protected void HandleAdLoaded(object sender, EventArgs e)
        {
            Debug.Log("Ad loaded here");

            MainThreadDispatcher.Enqueue(() =>
            {
                m_IsReady = true;
                m_LoadCallback.Invoke(new SA_Result());
                m_LoadCallback = null;
            });
        }

        protected void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                var error = new SA_Error(1, e.LoadAdError.GetMessage());
                m_LoadCallback.Invoke(new SA_Result(error));
                m_LoadCallback = null;
            });
        }

        public bool IsReady => m_IsReady;
    }
}
