using System;
using System.Collections.Generic;
using System.ComponentModel;
using SA.Android;
using SA.Android.Utilities;
using SA.Foundation.Templates;
using SA.Android.Vending.BillingClient;
using UnityEngine.Assertions;

namespace SA.CrossPlatform.InApp
{
    class UM_AndroidInAppClient : UM_AbstractInAppClient,
        UM_iInAppClient,
        AN_iBillingClientStateListener,
        AN_iPurchasesUpdatedListener
    {
        AN_BillingClient m_BillingClient;
        Action<SA_iResult> m_ConnectToServiceCallback;
        readonly List<AN_SkuDetails> m_Products = new List<AN_SkuDetails>();
        UM_AndroidProductConsumer m_ProductConsumer;

        IEnumerable<UM_ProductTemplate> m_InitialProducts;

        string m_CurrentBillingFlowSku = string.Empty;

        internal AN_BillingClient BillingClient => m_BillingClient;

        //--------------------------------------
        //  UM_AbstractInAppClient
        //--------------------------------------

        protected override void ConnectToService(Action<SA_iResult> callback)
        {
            ConnectToService(null, callback);
        }

        protected override void ConnectToService(IEnumerable<UM_ProductTemplate> products, Action<SA_iResult> callback)
        {
            m_InitialProducts = products;
            m_ConnectToServiceCallback = callback;
            using (var builder = AN_BillingClient.NewBuilder())
            {
                builder.SetListener(this);
                builder.EnablePendingPurchases();

                m_BillingClient = builder.Build();
                m_BillingClient.StartConnection(this);

                m_ProductConsumer = new UM_AndroidProductConsumer(m_BillingClient);
            }
        }

        //--------------------------------------
        //  AN_iBillingClientStateListener
        //--------------------------------------

        public void OnBillingSetupFinished(SA_iResult billingResult)
        {
            if (billingResult.IsSucceeded)
            {
                UM_AndroidSkuDetailsLoader skuDetailsLoader;
                if (m_InitialProducts == null)
                    skuDetailsLoader = new UM_AndroidSkuDetailsLoader(AN_Settings.Instance.InAppProducts);
                else
                    skuDetailsLoader = new UM_AndroidSkuDetailsLoader(ConvertToAndroidTemplates(m_InitialProducts));

                skuDetailsLoader.LoadSkuDetails(m_BillingClient, AN_BillingClient.SkuType.inapp, inapps =>
                {
                    m_Products.AddRange(inapps);
                    skuDetailsLoader.LoadSkuDetails(m_BillingClient, AN_BillingClient.SkuType.subs, subs =>
                    {
                        m_Products.AddRange(subs);
                        if (m_Products.Count == 0)
                        {
                            var errorMessage =
                                "UM_AndroidInAppClient: Connections successful but no products found. " +
                                "Cloud be due to application misconfiguration or missing internet connection or app has not been published yet.";
                            AN_Logger.Log(errorMessage);
                            var failedResult = new SA_Result(new SA_Error(1001, errorMessage));
                            m_ConnectToServiceCallback.Invoke(failedResult);
                        }
                        else
                        {
                            AN_Logger.Log("UM_AndroidInAppClient: Billing service initialized with " + m_Products.Count + " available products");
                            m_ConnectToServiceCallback.Invoke(billingResult);
                        }
                    });
                });
            }
            else
            {
                m_ConnectToServiceCallback.Invoke(billingResult);
            }
        }

        internal static List<AN_SkuDetails> ConvertToAndroidTemplates(IEnumerable<UM_ProductTemplate> productTemplates)
        {
            var result = new List<AN_SkuDetails>();
            foreach (var product in productTemplates)
            {
                AN_SkuDetails sku = null;
                switch (product.Type)
                {
                    case UM_ProductType.Consumable:
                        sku = new AN_SkuDetails(product.Id, AN_BillingClient.SkuType.inapp);
                        sku.IsConsumable = true;
                        break;
                    case UM_ProductType.NonConsumable:
                        sku = new AN_SkuDetails(product.Id, AN_BillingClient.SkuType.inapp);
                        sku.IsConsumable = false;
                        break;
                    case UM_ProductType.Subscription:
                        sku = new AN_SkuDetails(product.Id, AN_BillingClient.SkuType.subs);
                        break;
                }

                result.Add(sku);
            }

            return result;
        }

        void GetPurchaseRecords(Action<List<UM_AndroidPurchaseRecord>> callback)
        {
            var availableRecords = new List<string>();
            var records = new List<UM_AndroidPurchaseRecord>();
            var localPurchasesCache = new List<AN_Purchase>();

            var purchases = m_BillingClient.QueryPurchases(AN_BillingClient.SkuType.inapp);
            if (purchases.Purchases != null)
                localPurchasesCache.AddRange(purchases.Purchases);

            purchases = m_BillingClient.QueryPurchases(AN_BillingClient.SkuType.subs);
            if (purchases.Purchases != null)
                localPurchasesCache.AddRange(purchases.Purchases);

            foreach (var purchase in localPurchasesCache)
            {
                var record = new UM_AndroidPurchaseRecord(purchase);
                if (record.IsValid)
                {
                    records.Add(record);
                    availableRecords.Add(record.SkuDetails.Sku);
                }
            }

            var loader = new UM_AndroidSkuPurchaseHistoryLoader(m_BillingClient);
            loader.Load(purchaseHistoryRecords =>
            {
                foreach (var historyRecord in purchaseHistoryRecords)
                {
                    if (availableRecords.Contains(historyRecord.Sku))
                        continue;

                    var record = new UM_AndroidPurchaseRecord(historyRecord);
                    if (record.IsValid)
                        records.Add(record);
                }

                callback.Invoke(records);
            });
        }

        protected override void ObserveTransactions()
        {
            GetPurchaseRecords(records =>
            {
                //Processed local records only
                foreach (var record in records)
                {
                    if (!record.IsLocal)
                        continue;

                    //We have the local transaction record, but it wasn't yet completed, let's restore the transaction.
                    //Do not mix with "restored" transactions via RestoreTransactions method.

                    if (!record.WasProcessedLocally)
                    {
                        AN_Logger.Log("UM_AndroidInAppClient: Found unprocessed local transaction. Transaction restarted for: " + record.SkuDetails.Sku);
                        UpdateTransaction(new UM_AndroidTransaction(record.Purchase, false));
                    }
                    else
                    {
                        //Transaction was completed locally, in this case,
                        //let's make sure it was finished properly on the native part as well.
                        AN_Logger.Log("UM_AndroidInAppClient: Local transaction for product: " + record.SkuDetails.Sku + "was finished, just make sure it's also completed on a native part");
                        FinishNativeTransaction(record.Purchase);
                    }
                }

                //This is an additional testing layer, made to prevent lost transaction if local cache was harmed.
                //All records that we will have there wasn't present in the local cache.
                foreach (var record in records)
                {
                    if (record.IsLocal)
                        continue;

                    AN_Logger.Log("UM_AndroidInAppClient: History record for: " + record.SkuDetails.Sku);

                    //This application instance already aware of this purchase
                    if (record.WasProcessedLocally)
                        continue;

                    AN_Logger.Log("UM_AndroidInAppClient: History transaction wasn't processed locally");

                    switch (record.SkuDetails.Type)
                    {
                        case AN_BillingClient.SkuType.inapp:
                            if (record.SkuDetails.IsConsumable)
                            {
                                AN_Logger.Log("UM_AndroidInAppClient: Found unprocessed consumable purchase history record for + " + record.SkuDetails.Sku + ". Consuming...");

                                //This is again a bit risky. The only way to check if item wasn't consumed yet
                                //is to actually consume it.
                                //The risk is, if we will consume it and then cline app will fail to reward the user
                                //this purchase will be permanently lost
                                m_ProductConsumer.Consume(record.Purchase.PurchaseToken, result =>
                                {
                                    AN_Logger.Log("UM_AndroidInAppClient: History transaction consume: " + result.IsSucceeded);
                                    var transaction = new UM_AndroidTransaction(record.Purchase, false);
                                    if (result.IsSucceeded)
                                        UpdateTransaction(transaction);
                                    else
                                        UM_AndroidInAppTransactions.RegisterCompleteTransaction(transaction.Id);
                                });
                            }
                            else
                            {
                                AN_Logger.Log("UM_AndroidInAppClient: Restore history transaction");
                                Restore(record);
                            }

                            break;
                        case AN_BillingClient.SkuType.subs:
                            Restore(record);
                            break;
                    }
                }
            });
        }

        void Restore(UM_AndroidPurchaseRecord record)
        {
            //This can be dangerous since we can't know if history record contains expired / refunded or invalid purchase
            //it can only be verified by sending details to the server.
            //What we are trying to do here, is to run Acknowledge method and see if it fails
            //If it fails this is probably expired or refunded transaction, otherwise is is valid transaction and we can restore it

            //Another issue that if we will Acknowledge not previously Acknowledged transaction, we can't go back from this state.

            AN_Logger.Log("UM_AndroidInAppClient: Trying to restore Acknowledged transaction.");
            Acknowledge(record.Purchase.PurchaseToken, result =>
            {
                var transaction = new UM_AndroidTransaction(record.Purchase, false);
                if (result.IsSucceeded)
                    UpdateTransaction(transaction);
                else

                    //To make sure we will not run it again in case of failure
                    UM_AndroidInAppTransactions.RegisterCompleteTransaction(transaction.Id);
            });
        }

        internal AN_SkuDetails GetProduct(string sku)
        {
            foreach (var product in m_Products)
                if (product.Sku.Equals(sku))
                    return product;

            return null;
        }

        void RestartTransaction(AN_Purchase purchase)
        {
            var transaction = new UM_AndroidTransaction(new SA_Result(), purchase);
            UpdateTransaction(transaction);
        }

        public void OnBillingServiceDisconnected() { }

        protected override Dictionary<string, UM_iProduct> GetServerProductsInfo()
        {
            var products = new Dictionary<string, UM_iProduct>();
            foreach (var product in m_Products)
            {
                var p = new UM_AndroidProduct();
                p.Override(product);

                if (products.ContainsKey(p.Id))
                {
                    AN_Logger.LogError("UM_AndroidInAppClient: Skipping duplicated id for product " + p.Id + " check your settings!");
                    continue;
                }

                products.Add(p.Id, p);
            }

            return products;
        }

        //--------------------------------------
        //  UM_iInAppClient
        //--------------------------------------

        public void AddPayment(string productId)
        {
            var skuDetails = GetSkuDetails(productId);
            if (skuDetails == null)
            {
                AN_Logger.LogError("Product with id: " + productId + " wasn't recognized by the payment service, please make sure this product is available for your application");
                return;
            }

            m_CurrentBillingFlowSku = productId;
            var paramsBuilder = AN_BillingFlowParams.NewBuilder();
            paramsBuilder.SetSkuDetails(skuDetails);
            m_BillingClient.LaunchBillingFlow(paramsBuilder.Build());
        }

        //--------------------------------------
        //  AN_iPurchasesUpdatedListener
        //--------------------------------------

        public void onPurchasesUpdated(SA_iResult billingResult, List<AN_Purchase> purchases)
        {
            foreach (var purchase in purchases) SendTransactionUpdate(billingResult, purchase);

            if (purchases.Count == 0)
            {
                if (billingResult.IsFailed)
                    TryResolve(billingResult, () =>
                    {
                        SendTransactionUpdate(billingResult, null);
                    });
                else
                    throw new InvalidEnumArgumentException("UM_AndroidInAppClient:  billingResult is Succeeded, but no products provided.");
            }
        }

        bool m_FixAttemptInProgress;

        void TryResolve(SA_iResult billingResult, Action onResolveFailed)
        {
            AN_Logger.Log("UM_AndroidInAppClient: Purchase failed, let's see if we can resolve");

            AN_Logger.Log("UM_AndroidInAppClient: Resolve condition: ");
            AN_Logger.Log("UM_AndroidInAppClient: m_FixAttemptInProgress: " + !m_FixAttemptInProgress);
            AN_Logger.Log("UM_AndroidInAppClient: billingResult.Error.Code == (int) AN_BillingClient.BillingResponseCode.ItemAlreadyOwned: " + (billingResult.Error.Code == (int)AN_BillingClient.BillingResponseCode.ItemAlreadyOwned));
            AN_Logger.Log("UM_AndroidInAppClient: GetProduct(m_CurrentBillingFlowSku).Type == AN_BillingClient.SkuType.inapp: " + (GetProduct(m_CurrentBillingFlowSku).Type == AN_BillingClient.SkuType.inapp));
            AN_Logger.Log("UM_AndroidInAppClient: GetProduct(m_CurrentBillingFlowSku).IsConsumable: " + GetProduct(m_CurrentBillingFlowSku).IsConsumable);

            if (!m_FixAttemptInProgress
                && billingResult.Error.Code == (int)AN_BillingClient.BillingResponseCode.ItemAlreadyOwned
                && GetProduct(m_CurrentBillingFlowSku).Type == AN_BillingClient.SkuType.inapp
                && GetProduct(m_CurrentBillingFlowSku).IsConsumable)
            {
                AN_Logger.LogWarning("UM_AndroidInAppClient: attempting to fix ItemAlreadyOwned error for: " + m_CurrentBillingFlowSku);
                GetPurchaseRecords(records =>
                {
                    UM_AndroidPurchaseRecord purchaseRecord = null;
                    foreach (var record in records)
                        if (record.SkuDetails.Sku.Equals(m_CurrentBillingFlowSku))
                        {
                            purchaseRecord = record;
                            break;
                        }

                    //We cna nothing to do here. Google tells that item is owned, but no purchase info can be found
                    if (purchaseRecord == null)
                    {
                        AN_Logger.LogWarning("UM_AndroidInAppClient: Failed to fix ItemAlreadyOwned error for: "
                            + m_CurrentBillingFlowSku + " no purchase info found.");

                        onResolveFailed.Invoke();
                        return;
                    }

                    AN_Logger.Log("UM_AndroidInAppClient: Unresolved record found purchaseRecord.IsLocal: " + purchaseRecord.IsLocal);
                    m_FixAttemptInProgress = true;
                    if (purchaseRecord.WasProcessedLocally)
                    {
                        AN_Logger.LogWarning("UM_AndroidInAppClient: The client app tried to finish this transaction earlier. "
                            + " Let's try to finish the transaction.");
                        m_ProductConsumer.Consume(purchaseRecord.Purchase, result =>
                        {
                            if (result.IsSucceeded)
                                AddPayment(m_CurrentBillingFlowSku);
                            else
                                onResolveFailed.Invoke();
                        });
                    }
                    else
                    {
                        AN_Logger.LogWarning("UM_AndroidInAppClient: The client app missed this transaction. "
                            + " Let's try to re-run the transaction.");
                        UpdateTransaction(new UM_AndroidTransaction(purchaseRecord.Purchase, false));
                    }
                });
            }
            else
            {
                onResolveFailed.Invoke();
            }
        }

        void SendTransactionUpdate(SA_iResult billingResult, AN_Purchase purchase)
        {
            m_FixAttemptInProgress = false;
            var transaction = new UM_AndroidTransaction(billingResult, purchase);
            UpdateTransaction(transaction);
        }

        public void FinishTransaction(UM_iTransaction transaction)
        {
            if (transaction.State == UM_TransactionState.Failed)

                //noting to finish since it's failed
                //it will not have product or transaction id
                return;

            if (!(transaction is UM_AndroidTransaction))
                throw new ArgumentException("UM_AndroidInAppClient: Wrong transaction class", "transaction");

            var purchase = ((UM_AndroidTransaction)transaction).Purchase;

            FinishNativeTransaction(purchase);
            UM_AndroidInAppTransactions.RegisterCompleteTransaction(transaction.Id);
        }

        void FinishNativeTransaction(AN_Purchase purchase)
        {
            var skuDetails = GetSkuDetails(purchase.Sku);
            if (skuDetails != null)
            {
                Assert.IsNotNull(purchase);
                switch (skuDetails.Type)
                {
                    case AN_BillingClient.SkuType.inapp:
                        if (skuDetails.IsConsumable)
                            m_ProductConsumer.Consume(purchase, result => { });
                        else if (!purchase.IsAcknowledged)
                            Acknowledge(purchase);
                        break;
                    case AN_BillingClient.SkuType.subs:
                        if (!purchase.IsAcknowledged)
                            Acknowledge(purchase);
                        break;
                }
            }
            else
            {
                AN_Logger.LogError("UM_AndroidInAppClient: Transaction is finished, but no product found for id: " + purchase.Sku);
            }
        }

        void Acknowledge(AN_Purchase purchase)
        {
            Acknowledge(purchase.PurchaseToken, (result) => { });
        }

        void Acknowledge(string purchaseToken, Action<SA_iResult> callback)
        {
            var paramsBuilder = AN_AcknowledgePurchaseParams.NewBuilder();
            paramsBuilder.SetPurchaseToken(purchaseToken);
            m_BillingClient.AcknowledgePurchase(paramsBuilder.Build(), callback);
        }

        public void RestoreCompletedTransactions()
        {
            //Isn't relevant for an Android platform.
        }

        AN_SkuDetails GetSkuDetails(string sku)
        {
            foreach (var product in m_Products)
                if (product.Sku.Equals(sku))
                    return product;

            return null;
        }
    }
}
