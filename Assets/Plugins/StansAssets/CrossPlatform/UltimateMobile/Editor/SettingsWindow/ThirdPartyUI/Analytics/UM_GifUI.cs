using UnityEngine;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_GifUI : UM_PluginSettingsUI
    {
        UM_AnalyticsResolver m_serviceResolver;

        public override void OnAwake()
        {
            base.OnAwake();
            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Analytics)");
        }

        public override string Title => "Gif Record & Share";

        protected override string Description => "Service allows you to record your gameplay as a GIF image and the share it";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_gif_icon.png");

        protected override SA_iAPIResolver Resolver
        {
            get
            {
                if (m_serviceResolver == null) m_serviceResolver = new UM_AnalyticsResolver();

                return m_serviceResolver;
            }
        }

        protected override void OnServiceUI() { }
    }
}
