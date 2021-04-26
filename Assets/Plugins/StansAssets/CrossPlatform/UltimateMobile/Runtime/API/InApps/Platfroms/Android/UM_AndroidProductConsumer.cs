using System;
using System.Collections.Generic;
using SA.Android.Utilities;
using SA.Android.Vending.BillingClient;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.InApp
{
    class UM_AndroidProductConsumer : AN_iConsumeResponseListener
    {
        readonly AN_BillingClient m_BillingClient;
        readonly Dictionary<string, Action<SA_iResult>> m_PendingCallbacks;

        public UM_AndroidProductConsumer(AN_BillingClient billingClient)
        {
            m_BillingClient = billingClient;
            m_PendingCallbacks = new Dictionary<string, Action<SA_iResult>>();
        }

        public void Consume(AN_Purchase purchase, Action<SA_iResult> callback)
        {
            Consume(purchase.PurchaseToken, callback);
        }

        public void Consume(string purchaseToken, Action<SA_iResult> callback)
        {
            m_PendingCallbacks.Add(purchaseToken, callback);
            var paramsBuilder = AN_ConsumeParams.NewBuilder();
            paramsBuilder.SetPurchaseToken(purchaseToken);
            m_BillingClient.ConsumeAsync(paramsBuilder.Build(), this);
        }

        public void OnConsumeResponse(SA_iResult billingResult, string purchaseToken)
        {
            Action<SA_iResult> callback;
            if (m_PendingCallbacks.TryGetValue(purchaseToken, out callback))
            {
                callback.Invoke(billingResult);
                m_PendingCallbacks.Remove(purchaseToken);
            }
            else
            {
                AN_Logger.LogError("OnConsumeResponse called with unknown purchaseToken: " + purchaseToken);
            }
        }
    }
}
