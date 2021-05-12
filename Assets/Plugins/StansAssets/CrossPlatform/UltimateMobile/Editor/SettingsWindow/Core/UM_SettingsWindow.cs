using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using SA.iOS;
using SA.Android;
using System;
using System.Collections.Generic;
using SA.Android.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_SettingsWindow : SA_PluginSettingsWindow<UM_SettingsWindow>
    {
        [Serializable]
        public class SelectedBlockInfo
        {
            public UM_UIPlatform Platform;
            public string SettingsBlockTypeName;
        }

        SA_ServicesTab m_CurrentServiceTab;
        SA_ServicesTab m_3RdPartyServiceTab;

        const int k_ToolbarButtonsHeight = 19;
        const int k_ToolbarButtonsSpace = -10;

        const string k_Description = "The Ultimate plugin for the mobile development and more. " +
            "All the service and APIs for diffrent platfroms in one place. " +
            "Bound with unified API";

        [SerializeField]
        UM_UIPlatform m_SelectedPlatform = UM_UIPlatform.Unified;
        [SerializeField]
        IMGUIHyperToolbar m_PluginsToolbar;

        [SerializeField]
        IMGUIHyperLabel m_BackLink;
        [SerializeField]
        List<SelectedBlockInfo> m_History = new List<SelectedBlockInfo>();

        //--------------------------------------
        // Initialization
        //--------------------------------------

        protected override void OnAwake()
        {
            SetHeaderTitle(UM_Settings.PLUGIN_NAME);
            SetHeaderVersion(UM_Settings.FormattedVersion);

            SetHeaderDescription(k_Description);
            SetDocumentationUrl(UM_Settings.DOCUMENTATION_URL);

            var backIcon = PluginsEditorSkin.GetGenericIcon("back.png");
            m_BackLink = new IMGUIHyperLabel(new GUIContent("Back", backIcon), EditorStyles.miniLabel);
            m_BackLink.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);

            UpdateToolBarByPluginIndex();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_PluginsToolbar = new IMGUIHyperToolbar();
            m_PluginsToolbar.SetButtonsHeight(k_ToolbarButtonsHeight);
            m_PluginsToolbar.SetItemsSpace(k_ToolbarButtonsSpace);

            AddPlatform("Unified", UM_Skin.GetDefaultIcon("ultimate_icon_pro.png"));
            AddPlatform("Android", UM_Skin.GetPlatformIcon("android_icon.png"));
            AddPlatform("iOS", UM_Skin.GetPlatformIcon("ios_icon.png"));

            m_PluginsToolbar.SetSelectedIndex((int)UM_UIPlatform.Unified);
        }

        //--------------------------------------
        // Static Methods
        //--------------------------------------

        public static void SelectBlock(SelectedBlockInfo info)
        {
            var wnd = GetWindow<UM_SettingsWindow>();
            wnd.m_History.Add(info);
        }

        //--------------------------------------
        // Private Methods
        //--------------------------------------

        void UpdateToolBarByPluginIndex(bool forced = false)
        {
            if (forced)
            {
                m_menuToolbar = new IMGUIHyperToolbar();
                m_tabsLayout.Clear();
            }

            switch (m_PluginsToolbar.SelectionIndex)
            {
                case (int)UM_UIPlatform.IOS:
                    m_3RdPartyServiceTab = null;
                    m_CurrentServiceTab = CreateInstance<ISN_ServicesTab>();
                    AddMenuItem("SERVICES", m_CurrentServiceTab, forced);
                    AddMenuItem("XCODE", CreateInstance<ISN_XCodeTab>(), forced);
                    AddMenuItem("SETTINGS", CreateInstance<ISN_SettingsTab>(), forced);

                    SetHeaderTitle(ISN_Settings.PluginTittle);
                    SetHeaderVersion(ISN_Settings.FormattedVersion);
                    SetHeaderDescription(ISN_SettingsWindow.DESCRIPTION);
                    SetDocumentationUrl(ISN_Settings.DocumentationUrl);

                    break;
                case (int)UM_UIPlatform.Android:
                    m_3RdPartyServiceTab = null;
                    m_CurrentServiceTab = CreateInstance<AN_ServicesTab>();
                    AddMenuItem("SERVICES", m_CurrentServiceTab, forced);
                    AddMenuItem("MANIFEST", CreateInstance<AN_ManifestTab>(), forced);
                    AddMenuItem("SETTINGS", CreateInstance<AN_SettingsTab>(), forced);

                    SetHeaderTitle(AN_Settings.PLUGIN_NAME);
                    SetHeaderVersion(AN_Settings.FormattedVersion);

                    SetHeaderDescription(AN_SettingsWindow.DESCRIPTION);
                    SetDocumentationUrl(AN_Settings.DOCUMENTATION_URL);

                    break;
                case (int)UM_UIPlatform.Unified:

                    m_CurrentServiceTab = CreateInstance<UM_ServicesTab>();
                    m_3RdPartyServiceTab = CreateInstance<UM_3rdPartyServicesTab>();
                    AddMenuItem("SERVICES", m_CurrentServiceTab, forced);
                    AddMenuItem("3RD-PARTY", m_3RdPartyServiceTab, forced);

                    AddMenuItem("SUMMARY", CreateInstance<UM_SummaryTab>(), forced);

                    SetHeaderTitle(UM_Settings.PLUGIN_NAME);
                    SetHeaderVersion(UM_Settings.FormattedVersion);
                    SetHeaderDescription(k_Description);
                    SetDocumentationUrl(UM_Settings.DOCUMENTATION_URL);
                    break;
            }
            
            AddMenuItem("ABOUT", CreateInstance<IMGUIAboutTab>(), forced);

            foreach (var layout in m_tabsLayout) layout.OnLayoutEnable();
        }

        void AddPlatform(string platformName, Texture icon)
        {
            var style = new GUIStyle(EditorStyles.miniLabel);
            if (!EditorGUIUtility.isProSkin) style.normal.textColor = SA_PluginSettingsWindowStyles.GerySilverColor;
            var button = new IMGUIHyperLabel(new GUIContent(platformName, icon), style);
            button.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);

            if (!EditorGUIUtility.isProSkin) button.GuiColorOverride(true);
            m_PluginsToolbar.AddButtons(button);
        }

        protected override void OnLayoutGUI()
        {
            var newSelection = m_CurrentServiceTab.SelectedService;

            if (newSelection == null && m_3RdPartyServiceTab != null) newSelection = m_3RdPartyServiceTab.SelectedService;

            if (newSelection != null)
            {
                var info = new SelectedBlockInfo();
                info.Platform = m_SelectedPlatform;
                info.SettingsBlockTypeName = newSelection.GetType().Name;
                newSelection.UnSelect();

                SelectBlock(info);
            }

            if (m_History.Count > 0)
            {
                var activeBlockInfo = m_History[m_History.Count - 1];
                SetSelectedPlatform(activeBlockInfo.Platform);

                var block = m_CurrentServiceTab.GetBlockByTypeName(activeBlockInfo.SettingsBlockTypeName);

                //Probably that was another services block
                if (block == null && m_3RdPartyServiceTab != null) block = m_3RdPartyServiceTab.GetBlockByTypeName(activeBlockInfo.SettingsBlockTypeName);

                DrawBrowserToolbar();
                block.DrawHeaderUI();
                DrawScrollView(() =>
                {
                    block.DrawServiceUI();
                });
            }
            else
            {
                DrawToolbar();
                DrawHeader();

                var tabIndex = DrawMenu();

                if (!string.IsNullOrEmpty(m_SearchString))
                    DrawScrollView(() =>
                    {
                        m_CurrentServiceTab.OnSearchGUI(m_SearchString);
                        if (m_3RdPartyServiceTab != null) m_3RdPartyServiceTab.OnSearchGUI(m_SearchString);
                    });
                else
                    DrawScrollView(() =>
                    {
                        OnTabsGUI(tabIndex);
                    });
            }
        }

        protected override void BeforeGUI()
        {
            EditorGUI.BeginChangeCheck();
        }

        protected override void AfterGUI()
        {
            if (EditorGUI.EndChangeCheck())
            {
                AN_SettingsWindow.SaveSettins();
                ISN_SettingsWindow.SaveSettings();

                UM_Settings.Save();
            }
        }

        void DrawToolbar()
        {
            GUILayout.Space(2);
            using (new IMGUIBeginHorizontal())
            {
                using (new IMGUIBeginVertical())
                {
                    GUILayout.Space(-1);
                    var index = m_PluginsToolbar.Draw();
                    if (index != (int)m_SelectedPlatform)
                    {
                        m_SelectedPlatform = (UM_UIPlatform)index;
                        UpdateToolBarByPluginIndex(true);
                    }
                }

                GUILayout.FlexibleSpace();

                using (new IMGUIBeginVertical())
                {
                    DrawSearchBar();
                }
            }

            GUILayout.Space(5);
        }

        void DrawBrowserToolbar()
        {
            GUILayout.Space(2);
            using (new IMGUIBeginHorizontal())
            {
                var width = m_BackLink.CalcSize().x + 5f;
                var clicked = m_BackLink.Draw(GUILayout.Width(width));
                if (clicked) m_History.RemoveAt(m_History.Count - 1);
                GUILayout.FlexibleSpace();
                using (new IMGUIBeginVertical())
                {
                    GUILayout.Space(-1);
                    var currentSelectedButton = m_PluginsToolbar.Buttons[m_PluginsToolbar.SelectionIndex];
                    width = currentSelectedButton.CalcSize().x + k_ToolbarButtonsSpace;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.Space();
                        currentSelectedButton.Draw(GUILayout.Width(width), GUILayout.Height(k_ToolbarButtonsHeight));
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(5);
        }

        void SetSelectedPlatform(UM_UIPlatform platform)
        {
            if (m_SelectedPlatform == platform) return;

            m_SelectedPlatform = platform;
            m_PluginsToolbar.SetSelectedIndex((int)m_SelectedPlatform);
            UpdateToolBarByPluginIndex(true);
        }
    }
}
