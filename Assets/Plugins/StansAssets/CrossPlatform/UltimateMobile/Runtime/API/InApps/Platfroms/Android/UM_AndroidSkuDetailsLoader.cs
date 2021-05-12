using System;
using System.Collections.Generic;
using System.Linq;
using SA.Android.Utilities;
using SA.Android.Vending.BillingClient;
using SA.Foundation.Templates;
using UnityEngine;

namespace SA.CrossPlatform.InApp
{
    class UM_AndroidSkuDetailsLoader : AN_iSkuDetailsResponseListener
    {
        event Action<List<AN_SkuDetails>> m_Callback;
        readonly List<AN_SkuDetails> m_SettingsInAppProducts;

        public UM_AndroidSkuDetailsLoader(List<AN_SkuDetails> products)
        {
            m_SettingsInAppProducts = products;
        }

        public void LoadSkuDetails(AN_BillingClient client, AN_BillingClient.SkuType skuType, Action<List<AN_SkuDetails>> callback)
        {
            m_Callback = callback;
            var paramsBuilder = AN_SkuDetailsParams.NewBuilder();
            paramsBuilder.SetType(skuType);

            var skusList = new List<string>();
            foreach (var product in m_SettingsInAppProducts)
                if (product.Type == skuType)
                    skusList.Add(product.Sku);

            paramsBuilder.SetSkusList(skusList);
            client.QuerySkuDetailsAsync(paramsBuilder.Build(), this);
        }

        public void OnSkuDetailsResponse(SA_Result billingResult, List<AN_SkuDetails> skuDetailsList)
        {
            if (billingResult.IsSucceeded)
            {
                var result = new List<AN_SkuDetails>();
                foreach (var nativeProduct in skuDetailsList)
                {
                    var settingsProduct = GetProductFromSettings(nativeProduct.Sku);
                    AN_BillingClient.OverrideLocalSkuWithNativeData(settingsProduct, nativeProduct);
                    result.Add(settingsProduct);
                }

                m_Callback.Invoke(result);
            }
            else
            {
                m_Callback.Invoke(new List<AN_SkuDetails>());
            }
        }

        AN_SkuDetails GetProductFromSettings(string sku)
        {
            foreach (var product in m_SettingsInAppProducts)
                if (product.Sku.Equals(sku))
                    return product;

            return null;
        }
    }
}
