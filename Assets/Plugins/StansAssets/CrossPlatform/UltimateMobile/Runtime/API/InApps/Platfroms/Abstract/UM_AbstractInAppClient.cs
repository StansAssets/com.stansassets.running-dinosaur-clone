using System;
using System.Collections.Generic;
using UnityEngine;
using SA.Foundation.Templates;
using SA.CrossPlatform.Analytics;

namespace SA.CrossPlatform.InApp
{
    abstract class UM_AbstractInAppClient
    {
        bool m_IsConnected;
        bool m_IsConnectionInProgress;
        bool m_IsObserverRegistered;
        event Action<SA_iResult> OnConnect = delegate { };
        Dictionary<string, UM_iProduct> m_Products = new Dictionary<string, UM_iProduct>();
        protected UM_iTransactionObserver m_Observer;

        //--------------------------------------
        //  Abstract
        //--------------------------------------

        protected abstract void ConnectToService(Action<SA_iResult> callback);
        protected abstract void ConnectToService(IEnumerable<UM_ProductTemplate> products, Action<SA_iResult> callback);
        protected abstract void ObserveTransactions();

        /// <summary>
        /// Will update products list based on information retried from server
        /// </summary>
        protected abstract Dictionary<string, UM_iProduct> GetServerProductsInfo();

        //--------------------------------------
        //  Public Methods
        //--------------------------------------

        public void Connect(Action<SA_iResult> callback)
        {
            Connect(null, callback);
        }

        public void Connect(IEnumerable<UM_ProductTemplate> products, Action<SA_iResult> callback)
        {
            if (m_IsConnected)
            {
                callback.Invoke(new SA_Result());
                return;
            }

            OnConnect += callback;
            if (m_IsConnectionInProgress) return;
            m_IsConnectionInProgress = true;

            ConnectToTheBillingService(products, result =>
            {
                if (result.IsSucceeded)
                {
                    m_IsConnected = true;
                    m_Products = GetServerProductsInfo();
                }

                m_IsConnectionInProgress = false;
                OnConnect.Invoke(result);

                //Checking if we should add an observer
                //In case user added it before service was connected
                if (m_Observer != null && !m_IsObserverRegistered)
                {
                    m_IsObserverRegistered = true;
                    ObserveTransactions();
                }

                OnConnect = delegate { };
            });
        }

        void ConnectToTheBillingService(IEnumerable<UM_ProductTemplate> products, Action<SA_iResult> callback)
        {
            if (products == null)
                ConnectToService(callback);
            else
                ConnectToService(products, callback);
        }

        public void SetTransactionObserver(UM_iTransactionObserver observer)
        {
            if (m_Observer != null)
            {
                Debug.LogWarning("UM_AbstractInAppClient::SetTransactionObserver you can only set one Transactions Observer");
                return;
            }

            m_Observer = observer;

            // Make sure we adding actual observer only when connect to the service.
            // Otherwise we will wait for a successful connection
            if (IsConnected)
            {
                m_IsObserverRegistered = true;
                ObserveTransactions();
            }
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="productIdentifier">Product identifier.</param>
        public UM_iProduct GetProductById(string productIdentifier)
        {
            return m_Products.TryGetValue(productIdentifier, out var product) ? product : null;
        }

        //--------------------------------------
        //  Get / Set
        //--------------------------------------

        /// <summary>
        /// Returns <c>true</c> if we are currently connected to the store services. Otherwise <c>false</c>
        /// </summary>
        public bool IsConnected => m_IsConnected;

        /// <summary>
        /// A list of products, one product for each valid product identifier provided in the original init request.
        /// only valid to use when <see cref="IsConnected"/> is <c>true</c>
        /// </summary>
        public IEnumerable<UM_iProduct> Products => new List<UM_iProduct>(m_Products.Values);

        //--------------------------------------
        //  Protected Methods
        //--------------------------------------

        protected void UpdateTransaction(UM_iTransaction transaction)
        {
            if (m_Observer == null)
            {
                Debug.LogError("UpdateTransaction has been called before m_observer is set");
                return;
            }

            UM_AnalyticsInternal.OnTransactionUpdated(transaction);
            m_Observer.OnTransactionUpdated(transaction);
        }

        protected void SetRestoreTransactionsResult(SA_Result result)
        {
            if (m_Observer == null)
            {
                Debug.LogError("SetRestoreTransactionsResult has been called before m_observer is set");
                return;
            }

            UM_AnalyticsInternal.OnRestoreTransactions();
            m_Observer.OnRestoreTransactionsComplete(result);
        }
    }
}
