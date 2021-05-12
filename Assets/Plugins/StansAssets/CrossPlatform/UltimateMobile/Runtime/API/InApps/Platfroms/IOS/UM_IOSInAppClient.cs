using System;
using System.Collections.Generic;
using SA.Foundation.Templates;
using SA.iOS.StoreKit;
using UnityEngine;

namespace SA.CrossPlatform.InApp
{
    class UM_IOSInAppClient : UM_AbstractInAppClient, UM_iInAppClient, ISN_iSKPaymentTransactionObserver
    {
        public const string k_UndefinedTransactionId = "undefined";

        //--------------------------------------
        //  UM_AbstractInAppClient
        //--------------------------------------

        protected override void ConnectToService(Action<SA_iResult> callback)
        {
            ISN_SKPaymentQueue.Init(callback.Invoke);
        }

        protected override void ConnectToService(IEnumerable<UM_ProductTemplate> products, Action<SA_iResult> callback)
        {
            ISN_SKPaymentQueue.Init(ConvertToIOSTemplates(products), callback.Invoke);
        }

        internal static List<ISN_SKProduct> ConvertToIOSTemplates(IEnumerable<UM_ProductTemplate> productTemplates)
        {
            var result = new List<ISN_SKProduct>();
            foreach (var product in productTemplates) result.Add(new ISN_SKProduct { ProductIdentifier = product.Id });

            return result;
        }

        protected override Dictionary<string, UM_iProduct> GetServerProductsInfo()
        {
            var products = new Dictionary<string, UM_iProduct>();
            foreach (var product in ISN_SKPaymentQueue.Products)
            {
                var p = new UM_IOSProduct();
                p.Override(product);

                products.Add(p.Id, p);
            }

            return products;
        }

        protected override void ObserveTransactions()
        {
            ISN_SKPaymentQueue.AddTransactionObserver(this);
        }

        //--------------------------------------
        //  UM_iInAppClient
        //--------------------------------------

        public void AddPayment(string productId)
        {
            ISN_SKPaymentQueue.AddPayment(productId);
        }

        public void FinishTransaction(UM_iTransaction transaction)
        {
            if (transaction.Id.Equals(k_UndefinedTransactionId))
                return;

            var t = (UM_IOSTransaction)transaction;
            var skPaymentTransaction = t.IosTransaction;
            ISN_SKPaymentQueue.FinishTransaction(skPaymentTransaction);
        }

        public void RestoreCompletedTransactions()
        {
            ISN_SKPaymentQueue.RestoreCompletedTransactions();
        }

        //--------------------------------------
        //  ISN_TransactionObserver implementation
        //--------------------------------------

        public void OnTransactionUpdated(ISN_iSKPaymentTransaction transaction)
        {
            var um_transaction = new UM_IOSTransaction(transaction);
            switch (transaction.State)
            {
                case ISN_SKPaymentTransactionState.Purchasing:
                    if (transaction.HasError) //otherwise we aren't interested.
                        UpdateTransaction(um_transaction);
                    break;
                case ISN_SKPaymentTransactionState.Purchased:
                case ISN_SKPaymentTransactionState.Restored:
                    UpdateTransaction(um_transaction);
                    break;
                case ISN_SKPaymentTransactionState.Deferred:
                    UpdateTransaction(um_transaction);
                    break;
                case ISN_SKPaymentTransactionState.Failed:
                    UpdateTransaction(um_transaction);
                    break;
            }
        }

        public void OnTransactionRemoved(ISN_iSKPaymentTransaction result)
        {
            //Your application does not typically need to anything on this event,  
            //but it may be used to update user interface to reflect that a transaction has been completed.
        }

        public bool OnShouldAddStorePayment(ISN_SKProduct product)
        {
            AddPayment(product.ProductIdentifier);
            return true;
        }

        public void OnRestoreTransactionsComplete(SA_Result result)
        {
            SetRestoreTransactionsResult(result);
        }

        public void DidChangeStorefront()
        {
            // Do nothing
        }
    }
}
