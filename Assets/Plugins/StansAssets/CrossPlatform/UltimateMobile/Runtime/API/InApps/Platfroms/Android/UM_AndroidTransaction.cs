using System;
using SA.Android.Utilities;
using SA.Foundation.Templates;
using SA.Android.Vending.BillingClient;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    class UM_AndroidTransaction : UM_AbstractTransaction<AN_Purchase>, UM_iTransaction
    {
        public UM_AndroidTransaction(SA_iResult billingResult, AN_Purchase purchase)
        {
            if (billingResult.IsSucceeded)
            {
                SetPurchase(purchase, false);
            }
            else
            {
                m_state = UM_TransactionState.Failed;
                m_error = billingResult.Error;
            }
        }

        public UM_AndroidTransaction(AN_Purchase purchase, bool isRestored)
        {
            SetPurchase(purchase, isRestored);
        }

        void SetPurchase(AN_Purchase purchase, bool isRestored)
        {
            m_id = purchase.PurchaseToken;
            m_productId = purchase.Sku;

            //convert from ms to sec
            try
            {
                m_unixTimestamp = (long)TimeSpan.FromMilliseconds(purchase.PurchaseTime).TotalSeconds;
            }
            catch (Exception exception)
            {
                m_unixTimestamp = purchase.PurchaseTime;
                AN_Logger.LogWarning("Failed to convert ms : " + purchase.PurchaseTime + " to seconds");
                AN_Logger.LogWarning("Exception Message: " + exception.Message);
            }

            if (isRestored)
                m_state = UM_TransactionState.Restored;
            else
                switch (purchase.PurchaseState)
                {
                    case AN_Purchase.State.Purchased:
                        m_state = UM_TransactionState.Purchased;
                        break;
                    case AN_Purchase.State.Pending:
                        m_state = UM_TransactionState.Pending;
                        break;
                    default:
                        m_state = UM_TransactionState.Unspecified;
                        break;
                }

            SetNativeTransaction(purchase);
        }

        public AN_Purchase Purchase => (AN_Purchase)NativeTemplate;
    }
}
