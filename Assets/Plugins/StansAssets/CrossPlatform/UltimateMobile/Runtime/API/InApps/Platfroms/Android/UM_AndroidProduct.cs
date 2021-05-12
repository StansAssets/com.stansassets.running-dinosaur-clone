using System;
using System.Text.RegularExpressions;
using SA.Android.Vending.BillingClient;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    class UM_AndroidProduct : UM_AbstractProduct<AN_SkuDetails>, UM_iProduct
    {
        protected override void OnOverride(AN_SkuDetails productTemplate)
        {
            m_id = productTemplate.Sku;
            m_price = productTemplate.Price;
            m_priceInMicros = productTemplate.PriceAmountMicros;
            m_priceCurrencyCode = productTemplate.PriceCurrencyCode;

            m_title = productTemplate.Title;
            if (!string.IsNullOrEmpty(m_title))
                m_title = Regex.Replace(m_title, "\\ \\(.*\\)", string.Empty);

            m_description = productTemplate.Description;
            switch (productTemplate.Type)
            {
                case AN_BillingClient.SkuType.inapp:
                    m_type = productTemplate.IsConsumable ? UM_ProductType.Consumable : UM_ProductType.NonConsumable;
                    break;
                case AN_BillingClient.SkuType.subs:
                    m_type = UM_ProductType.Subscription;
                    break;
            }

            m_icon = productTemplate.Icon;
        }
    }
}
