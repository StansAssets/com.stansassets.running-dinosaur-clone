using System;
using SA.Android.Vending.BillingClient;
using SA.CrossPlatform.InApp;
using SA.CrossPlatform.UI;
using SA.Foundation.Templates;
using UnityEngine;

namespace StansAssets.ProjectSample.InApps
{
    public class TransactionObserverExample : UM_iTransactionObserver
    {
        internal event Action OnTransactionResult;
        
        readonly Action m_ConsumablePurchasedAction, m_PremiumPurchasedAction;

        internal TransactionObserverExample(Action consumableCallback, Action premiumCallback)
        {
            m_ConsumablePurchasedAction = consumableCallback;
            m_PremiumPurchasedAction = premiumCallback;
        }
        
        public void OnRestoreTransactionsComplete(SA_Result result)
        {
            //Restore transaction flow was completed.
            //If you've set any flags before starting the Restoration flow, this is the perfect spot to drop it.
        }

        public void OnTransactionUpdated(UM_iTransaction transaction)
        {
            //Transactions have been updated.
            //Let's act accordingly
            PrintTransactionInfo(transaction);

            switch (transaction.State) {
                case UM_TransactionState.Restored:
                case UM_TransactionState.Purchased:
                    ProcessCompletedTransaction(transaction);
                    break;
                case UM_TransactionState.Pending:

                    //Android  - Transaction is pending and the player will be informed when it's done.
                    break;
                case UM_TransactionState.Failed:
                    //Our purchase flow is failed.
                    //We can unlock interface and tell user that the purchase is failed.

                    UM_InAppService.Client.FinishTransaction(transaction);
                    break;
            }
        }

        void ProcessCompletedTransaction(UM_iTransaction transaction)
        {
            //On android all the info we need is inside the ordinal android transaction
            //So let's get one.
            var an_purchase = (AN_Purchase)transaction.NativeTemplate;

            //Now you have asses to the all data from the original goal transaction object
            //That you can send to your server side and validate the purchase.
            Debug.Log(an_purchase?.OriginalJson ?? "Transaction JSON not available.");

            UnlockProduct(transaction);

            //Once product is successfully unlocked we can finish the transaction
            UM_InAppService.Client.FinishTransaction(transaction);
        }

        void PrintTransactionInfo(UM_iTransaction transaction)
        {
            Debug.Log("transaction.Id: " + transaction.Id);
            Debug.Log("transaction.State: " + transaction.State);
            Debug.Log("transaction.ProductId: " + transaction.ProductId);
            Debug.Log("transaction.Timestamp: " + transaction.Timestamp);

            var title = "Transaction Updated.";
            var message = string.Format(
                                        "ProductId: {0}. TransactionId: {1}. State: {2}",
                                        transaction.ProductId,
                                        transaction.Id,
                                        transaction.State);

            UM_DialogsUtility.ShowMessage(title, message);
        }

        void UnlockProduct(UM_iTransaction transaction)
        {

            //Reward your used based on transaction.Id
            switch (transaction.ProductId) {
                case InApps.ConsumableProductId:
                    m_ConsumablePurchasedAction();
                    break;
                case InApps.PremiumProductId:
                    m_PremiumPurchasedAction();
                    break;
                default:
                    Debug.LogError("Unknown product Id: " + transaction.ProductId);
                    break;
            }
            
            OnTransactionResult?.Invoke();
        }
    }
}
