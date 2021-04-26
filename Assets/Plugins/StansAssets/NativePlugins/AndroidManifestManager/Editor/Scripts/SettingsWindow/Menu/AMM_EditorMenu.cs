////////////////////////////////////////////////////////////////////////////////
//  
// @module Manifest Manager
// @author Alex Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using SA.Foundation.Config;

namespace SA.Android.Manifest
{
    public static class AMM_EditorMenu
    {
        const int PRIORITY = SA_Config.ProductivityNativeUtilityMenuIndex + 20;

        [MenuItem(SA_Config.EditorProductivityNativeUtilityMenuRoot + "Android Manifest/Settings", false, PRIORITY)]
        public static void Setting()
        {
            AMM_SettingsWindow.ShowTowardsInspector("Manifest", AMM_Skin.SettingsWindowIcon);
        }

        [MenuItem(SA_Config.EditorProductivityNativeUtilityMenuRoot + "Android Manifest/Documentation", false, PRIORITY)]
        public static void ISDSetupPluginSetUp()
        {
            Application.OpenURL(AMM_Settings.DOCUMENTATION_URL);
        }
    }
}
