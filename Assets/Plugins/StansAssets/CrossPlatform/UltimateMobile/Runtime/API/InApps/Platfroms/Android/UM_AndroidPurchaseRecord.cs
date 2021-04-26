using System;
using SA.Android.Utilities;
using SA.Android.Vending.BillingClient;
using UnityEngine;

namespace SA.CrossPlatform.InApp
{
    class UM_AndroidPurchaseRecord
    {
        readonly string m_Sku;
        readonly string m_OrderId;
        readonly string m_PurchaseToken;
        readonly bool m_IsLocal = false;

        AN_SkuDetails m_SkuDetails;
        readonly AN_Purchase m_Purchase;

        public UM_AndroidPurchaseRecord(AN_Purchase purchase)
        {
            m_Sku = purchase.Sku;
            m_OrderId = purchase.OrderId;
            m_PurchaseToken = purchase.PurchaseToken;
            m_IsLocal = true;
            m_Purchase = purchase;
            Init();
        }

        public UM_AndroidPurchaseRecord(AN_PurchaseHistoryRecord purchaseHistoryRecord)
        {
            m_Sku = purchaseHistoryRecord.Sku;
            m_OrderId = "undefined";
            m_PurchaseToken = purchaseHistoryRecord.PurchaseToken;
            Init();

            if (IsValid)
            {
                m_Purchase = new AN_Purchase(purchaseHistoryRecord.Sku, SkuDetails.Type);
                var recordJSON = JsonUtility.ToJson(purchaseHistoryRecord);
                JsonUtility.FromJsonOverwrite(recordJSON, m_Purchase);
            }
        }

        public AN_Purchase Purchase => m_Purchase;

        public bool IsValid => SkuDetails != null;

        public bool IsLocal => m_IsLocal;

        public bool WasProcessedLocally => IsTransactionCompleted(m_PurchaseToken) || IsTransactionCompleted(m_OrderId);

        public AN_SkuDetails SkuDetails => m_SkuDetails;

        void Init()
        {
            var client = UM_InAppService.Client as UM_AndroidInAppClient;
            if (client == null)
                throw new InvalidOperationException("UM_AndroidPurchaseRecord can only be used when UM_AndroidInAppClient os active");

            m_SkuDetails = client.GetProduct(m_Sku);
            if (m_SkuDetails == null)
                AN_Logger.LogWarning("Purchase record exists, but SkuDetails is not available for: " + m_Sku);
        }

        bool IsTransactionCompleted(string transactionId)
        {
            return UM_AndroidInAppTransactions.IsTransactionCompleted(transactionId);
        }
    }
}
