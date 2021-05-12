using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.Android.Editor
{
    class AN_SettingsWindow : SA_PluginSettingsWindow<AN_SettingsWindow>
    {
        public const string DESCRIPTION = "The plugin gives you an ability to work with Android Native API. " +
            "Every service that has additional project requirement can be disabled. " +
            "Enable only services you need for the current project.";

        [SerializeField]
        AN_ServicesTab m_servicesTab;
        [SerializeField]
        IMGUIHyperLabel m_baclLink;

        protected override void OnAwake()
        {
            SetHeaderTitle(AN_Settings.PLUGIN_NAME);
            SetHeaderVersion(AN_Settings.FormattedVersion);

            SetHeaderDescription(DESCRIPTION);
            SetDocumentationUrl(AN_Settings.DOCUMENTATION_URL);

            m_servicesTab = CreateInstance<AN_ServicesTab>();

            AddMenuItem("SERVICES", m_servicesTab);
            AddMenuItem("MANIFEST", CreateInstance<AN_ManifestTab>());
            AddMenuItem("SETTINGS", CreateInstance<AN_SettingsTab>());
            AddMenuItem("ABOUT", CreateInstance<IMGUIAboutTab>());

            var backIcon = PluginsEditorSkin.GetGenericIcon("back.png");
            m_baclLink = new IMGUIHyperLabel(new GUIContent("Back To Services", backIcon), EditorStyles.miniLabel);
            m_baclLink.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
        }

        protected override void BeforeGUI()
        {
            EditorGUI.BeginChangeCheck();
        }

        protected override void OnLayoutGUI()
        {
            var selectedService = m_servicesTab.SelectedService;
            if (selectedService == null)
            {
                base.OnLayoutGUI();
                return;
            }

            DrawTopbar(() =>
            {
                var backClick = m_baclLink.Draw();
                if (backClick) selectedService.UnSelect();
            });
            selectedService.DrawHeaderUI();
            DrawScrollView(() =>
            {
                selectedService.DrawServiceUI();
            });
        }

        protected override void AfterGUI()
        {
            if (EditorGUI.EndChangeCheck()) SaveSettins();
        }

        public static void SaveSettins()
        {
            AN_Settings.Save();
        }
    }
}
