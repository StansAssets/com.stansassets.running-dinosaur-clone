using System;
using System.Collections.Generic;
using UnityEngine;
using SA.Foundation.Templates;

namespace SA.iOS.StoreKit
{
    /// <summary>
    /// The <see cref="ISN_SKPaymentQueue"/> init result model.
    /// </summary>
    [Serializable]
    public class ISN_SKInitResult : SA_Result
    {
        [SerializeField]
        List<ISN_SKProduct> m_Products = new List<ISN_SKProduct>();
        [SerializeField]
        List<string> m_InvalidProductIdentifiers = new List<string>();

        public ISN_SKInitResult(List<ISN_SKProduct> products)
        {
            m_Products = products;
        }

        public ISN_SKInitResult(SA_Error error)
            : base(error) { }

        /// <summary>
        /// A list of products, one product for each valid product identifier provided in the original request.
        /// </summary>
        public List<ISN_SKProduct> Products => m_Products;

        /// <summary>
        /// An array of product identifier strings that were not recognized by the App Store.
        /// </summary>
        public List<string> InvalidProductIdentifiers => m_InvalidProductIdentifiers;
    }
}
