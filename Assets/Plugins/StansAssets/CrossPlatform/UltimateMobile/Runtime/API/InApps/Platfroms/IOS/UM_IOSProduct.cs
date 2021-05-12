using System;
using SA.iOS.StoreKit;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    class UM_IOSProduct : UM_AbstractProduct<ISN_SKProduct>, UM_iProduct
    {
        protected override void OnOverride(ISN_SKProduct productTemplate)
        {
            m_id = productTemplate.ProductIdentifier;
            m_price = productTemplate.LocalizedPrice;
            m_priceInMicros = productTemplate.PriceInMicros;

            if (productTemplate.PriceLocale != null && !string.IsNullOrEmpty(productTemplate.PriceLocale.Identifier))
                m_priceCurrencyCode = productTemplate.PriceLocale.CurrencyCode;
            else
                m_priceCurrencyCode = "USD";

            m_title = productTemplate.LocalizedTitle;
            m_description = productTemplate.LocalizedDescription;
            switch (productTemplate.Type)
            {
                case ISN_SKProductType.Consumable:
                    m_type = UM_ProductType.Consumable;
                    break;
                case ISN_SKProductType.NonConsumable:
                    m_type = UM_ProductType.NonConsumable;
                    break;
                case ISN_SKProductType.AutoRenewingSubscription:
                case ISN_SKProductType.NonRenewingSubscription:
                    m_type = UM_ProductType.Subscription;
                    break;
            }

            m_icon = productTemplate.Icon;
        }
    }
}
