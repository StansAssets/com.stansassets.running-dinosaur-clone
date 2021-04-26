using SA.Foundation.Config;
using UnityEditor;
using UnityEngine;

namespace SA.Foundation.Publisher
{
    public class SA_Internal_EditorMenu
    {
        const string k_SamplesUI = SA_Config.EditorFoundationLibMenuRoot + "Samples UI/";
        const string k_SamplesUIContext = "GameObject/Stan's Assets UI/";

        [MenuItem(SA_Config.EditorFoundationLibMenuRoot + "Publisher Tools", false, SA_Config.FoundationMenuIndex - 2)]
        public static void PackageExport()
        {
            SA_PublisherWindow.ShowTowardsInspector(WindowTitle);
        }

        static GUIContent WindowTitle => new GUIContent(" Publisher", SA_PublisherSkin.WindowIcon);

        [MenuItem(k_SamplesUIContext + "Tabs", false, 10)]
        [MenuItem(k_SamplesUI + "Tabs", false, SA_Config.FoundationMenuIndex - 6)]
        public static void CreateTabsViewPanel()
        {
            //  new TabsBuilder(Selection.activeGameObject).Build();
        }

        [MenuItem(k_SamplesUI + "Title", false, SA_Config.FoundationMenuIndex - 5)]
        [MenuItem(k_SamplesUIContext + "Title", false, SA_Config.FoundationMenuIndex - 5)]
        public static void CreateTitlePanel()
        {
            //  new TitlePanelBuilder(Selection.activeGameObject).Build();
        }

        [MenuItem(k_SamplesUI + "Description", false, SA_Config.FoundationMenuIndex - 4)]
        [MenuItem(k_SamplesUIContext + "Description", false, SA_Config.FoundationMenuIndex - 4)]
        public static void CreateDescriptionPanel()
        {
            // new DescriptionPanelBuilder(Selection.activeGameObject).Build();
        }

        [MenuItem(k_SamplesUI + "Buttons Panel", false, SA_Config.FoundationMenuIndex - 3)]
        [MenuItem(k_SamplesUIContext + "Buttons Panel", false, SA_Config.FoundationMenuIndex - 3)]
        public static void CreateButtonsPanel()
        {
            // new ButtonsPanelBuilder(Selection.activeGameObject).Build();
        }
    }
}
