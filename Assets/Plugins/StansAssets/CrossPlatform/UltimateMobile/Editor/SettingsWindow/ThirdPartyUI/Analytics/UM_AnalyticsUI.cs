using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_AnalyticsUI : UM_PluginSettingsUI
    {
        UM_AnalyticsResolver m_ServiceResolver;
        UM_AdvertisementPlatformUI m_UnityBlock;
        UM_AdvertisementPlatformUI m_FacebookBlock;
        UM_AdvertisementPlatformUI m_FirebaseBlock;


        const int k_ToggleWidth = 150;

        public override void OnAwake()
        {
            base.OnAwake();
            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Analytics)");
            AddFeatureUrl("Analytics API", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Analytics-API");
            AddFeatureUrl("Automatic Tracking", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Automatic-Tracking");
            AddFeatureUrl("Unity Analytics", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Unity-Analytics");
            AddFeatureUrl("Google Analytics", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Firebase-Analytics");
            AddFeatureUrl("Facebook Analytics ", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Facebook-Analytics");
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();

            m_UnityBlock = new UM_AdvertisementPlatformUI("Unity Analytics", "unity_icon.png",
                new UM_AnalyticsResolver(), UM_UnityAnalyticsUI.OnGUI);

            m_FacebookBlock = new UM_AdvertisementPlatformUI("Facebook Analytics", "facebook_icon.png", new UM_FacebookResolver(), () =>
            {
                EditorGUILayout.HelpBox("No additional settings required.", MessageType.Info);
            });

            m_FirebaseBlock = new UM_AdvertisementPlatformUI("Firebase Analytics", "firebase_icon.png", new UM_FirebaseResolver(), () =>
            {
                EditorGUILayout.HelpBox("No additional settings required.", MessageType.Info);
            });
        }

        public override string Title => "Analytics";

        protected override string Description =>
            "Service allows you to submit an analytics data " +
            "to the different analytics services using one unified API.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_analytics_icon.png");

        protected override SA_iAPIResolver Resolver => m_ServiceResolver ?? (m_ServiceResolver = new UM_AnalyticsResolver());

        protected override void OnServiceUI()
        {
            m_UnityBlock.OnGUI();
            m_FacebookBlock.OnGUI();
            m_FirebaseBlock.OnGUI();
            AutomationUI();
        }

        void AutomationUI()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Automation")))
            {
                EditorGUILayout.HelpBox("Analytics service can automate some analytics event propagation " +
                    "based on using Ultimate Mobile & Unity API.", MessageType.Info);
                var automation = UM_Settings.Instance.Analytics.Automation;
                using (new SA_H2WindowBlockWithSpace(new GUIContent("GENERAL")))
                {
                    using (new IMGUIBeginHorizontal())
                    {
                        automation.Exceptions = GUILayout.Toggle(automation.Exceptions, " Exceptions", GUILayout.Width(k_ToggleWidth));
                    }
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("GAME SERVICES")))
                {
                    using (new IMGUIBeginHorizontal())
                    {
                        automation.Scores = GUILayout.Toggle(automation.Scores, " Scores", GUILayout.Width(k_ToggleWidth));
                        automation.Achievements = GUILayout.Toggle(automation.Achievements, " Achievements", GUILayout.Width(k_ToggleWidth));
                    }

                    using (new IMGUIBeginHorizontal())
                    {
                        automation.GameSaves = GUILayout.Toggle(automation.GameSaves, " GameSaves", GUILayout.Width(k_ToggleWidth));
                        automation.PlayerIdTracking = GUILayout.Toggle(automation.PlayerIdTracking, " Player Id Tracking", GUILayout.Width(k_ToggleWidth));
                    }
                }

                using (new SA_H2WindowBlockWithSpace(new GUIContent("IN-APP PURCHASING")))
                {
                    using (new IMGUIBeginHorizontal())
                    {
                        automation.SuccessfulTransactions = GUILayout.Toggle(automation.SuccessfulTransactions, " Payments", GUILayout.Width(k_ToggleWidth));
                        automation.FailedTransactions = GUILayout.Toggle(automation.FailedTransactions, " Failed Transactions", GUILayout.Width(k_ToggleWidth));
                    }

                    using (new IMGUIBeginHorizontal())
                    {
                        automation.RestoreRequests = GUILayout.Toggle(automation.RestoreRequests, " Restore Requests", GUILayout.Width(k_ToggleWidth));
                    }
                }
            }
        }
    }
}
