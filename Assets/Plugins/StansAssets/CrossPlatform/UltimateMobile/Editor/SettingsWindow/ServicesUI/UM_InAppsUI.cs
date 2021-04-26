using UnityEditor;
using UnityEngine;
using SA.iOS;
using SA.Foundation.Editor;
using Rotorz.ReorderableList;
using SA.Android.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_InAppsUI : UM_ServiceSettingsUI
    {
        IMGUIPluginActiveTextLink m_LearnMoreLink;

        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_StoreKitUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_StoreKitResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_VendingFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_VendingResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started");
            AddFeatureUrl("Connecting to Service", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Connecting-to-The-Service");
            AddFeatureUrl("Purchase flow", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Purchase-flow");
            AddFeatureUrl("Transactions Validation", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Transactions-Validation");
            AddFeatureUrl("Editor Testing", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Test-Inside-the-Editor");
            AddFeatureUrl("Advanced use cases", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Advanced-use-cases");
            
            m_LearnMoreLink = new IMGUIPluginActiveTextLink("[?] Learn More");
        }

        public override string Title => "In-App Purchasing";

        protected override string Description =>
            "Offer customers extra content and features using in-app purchases — including premium content, " +
            "digital goods, and subscriptions — directly within your app. ";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_market_icon.png");

        protected override void OnServiceUI()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Editor Testing")))
            {
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    var click = m_LearnMoreLink.DrawWithCalcSize();
                    if (click)
                        Application.OpenURL("https://unionassets.com/ultimate-mobile-pro/test-inside-the-editor-793#restore-purchases");
                }

                ReorderableListGUI.Title("Products Restore Emulation");
                ReorderableListGUI.ListField(UM_Settings.Instance.TestRestoreProducts,
                    (position, text) => { return EditorGUI.TextField(position, text); },
                    () => { EditorGUILayout.LabelField("All configured products will be restored by default."); }
                );
            }
        }
    }
}
