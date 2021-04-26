using UnityEngine;

namespace SA.CrossPlatform.InApp
{
    abstract class UM_AbstractProduct<T>
    {
        [SerializeField]
        protected string m_id;
        [SerializeField]
        protected long m_priceInMicros;
        [SerializeField]
        protected string m_price;
        [SerializeField]
        protected string m_title;
        [SerializeField]
        protected string m_description;
        [SerializeField]
        protected string m_priceCurrencyCode;
        [SerializeField]
        protected Texture2D m_icon;
        [SerializeField]
        protected UM_ProductType m_type;

        bool m_isActive = false;
        public object NativeTemplate { get; private set; }

        protected abstract void OnOverride(T productTemplate);

        public void Override(T productTemplate)
        {
            OnOverride(productTemplate);
            m_isActive = true;
            NativeTemplate = productTemplate;
        }

        public string Id => m_id;

        public string Price => m_price;

        public long PriceInMicros => m_priceInMicros;

        public string Title => m_title;

        public string Description => m_description;

        public string PriceCurrencyCode => m_priceCurrencyCode;

        public Texture2D Icon => m_icon;

        public UM_ProductType Type => m_type;

        public bool IsActive => m_isActive;
    }
}
