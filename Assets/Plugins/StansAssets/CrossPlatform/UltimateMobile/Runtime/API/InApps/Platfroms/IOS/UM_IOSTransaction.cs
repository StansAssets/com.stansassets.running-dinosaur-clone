using System;
using SA.iOS.StoreKit;
using StansAssets.Foundation;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    class UM_IOSTransaction : UM_AbstractTransaction<ISN_iSKPaymentTransaction>, UM_iTransaction
    {
        public UM_IOSTransaction(ISN_iSKPaymentTransaction transaction)
        {
            m_id = transaction.TransactionIdentifier;
            m_productId = transaction.ProductIdentifier;
            m_unixTimestamp = TimeUtility.ToUnixTime(transaction.Date);

            switch (transaction.State)
            {
                case ISN_SKPaymentTransactionState.Deferred:
                    m_state = UM_TransactionState.Pending;
                    break;
                case ISN_SKPaymentTransactionState.Failed:
                    m_state = UM_TransactionState.Failed;
                    break;
                case ISN_SKPaymentTransactionState.Restored:
                    m_state = UM_TransactionState.Restored;
                    break;
                case ISN_SKPaymentTransactionState.Purchased:
                    m_state = UM_TransactionState.Purchased;
                    break;
                case ISN_SKPaymentTransactionState.Purchasing:
                    m_state = UM_TransactionState.Failed;
                    m_id = UM_IOSInAppClient.k_UndefinedTransactionId;
                    break;
                default:
                    m_state = UM_TransactionState.Unspecified;
                    m_id = UM_IOSInAppClient.k_UndefinedTransactionId;
                    break;
            }

            m_error = transaction.Error;
            SetNativeTransaction(transaction);
        }

        public ISN_iSKPaymentTransaction IosTransaction => (ISN_iSKPaymentTransaction)NativeTemplate;
    }
}
