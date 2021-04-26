using System;
using System.Collections.Generic;
using SA.Android.Vending.BillingClient;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.InApp
{
    class UM_AndroidSkuPurchaseHistoryLoader : AN_iPurchaseHistoryResponseListener
    {
        readonly AN_BillingClient m_Client;
        readonly List<AN_PurchaseHistoryRecord> m_PurchaseHistoryRecords;

        Action<List<AN_PurchaseHistoryRecord>> m_OnHistoryLoaded;

        bool m_InAppsResultReceived;

        public UM_AndroidSkuPurchaseHistoryLoader(AN_BillingClient client)
        {
            m_Client = client;
            m_PurchaseHistoryRecords = new List<AN_PurchaseHistoryRecord>();
        }

        public void Load(Action<List<AN_PurchaseHistoryRecord>> onHistoryLoaded)
        {
            m_OnHistoryLoaded = onHistoryLoaded;
            m_Client.QueryPurchaseHistoryAsync(AN_BillingClient.SkuType.inapp, this);
        }

        public void OnConsumeResponse(SA_iResult billingResult, List<AN_PurchaseHistoryRecord> purchaseHistoryRecordList)
        {
            if (purchaseHistoryRecordList != null)
                m_PurchaseHistoryRecords.AddRange(purchaseHistoryRecordList);

            if (!m_InAppsResultReceived)
            {
                m_InAppsResultReceived = true;
                m_Client.QueryPurchaseHistoryAsync(AN_BillingClient.SkuType.subs, this);
            }
            else
            {
                m_OnHistoryLoaded.Invoke(m_PurchaseHistoryRecords);
            }
        }
    }
}
