////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Deploy
// @author Stanislav Osipov (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using SA.Foundation.Config;

namespace SA.iOS.XCode
{
    public class ISD_EditorMenu : EditorWindow
    {
        // WARNING: same menu item path is duplicated for settings UI.
        // if you need to chnage it here, make a proper config firts.
        // do not chnage MenuItem path before you 100% what is mean by a statement above.

        const int PRIORITY = SA_Config.ProductivityNativeUtilityMenuIndex + 10;

        [MenuItem(SA_Config.EditorProductivityNativeUtilityMenuRoot + "IOS Deploy/Settings", false, PRIORITY)]
        public static void OpenMainPage()
        {
            ISD_SettingsWindow.ShowTowardsInspector("IOS Deploy", ISD_Skin.WindowIcon);
        }

        [MenuItem(SA_Config.EditorProductivityNativeUtilityMenuRoot + "IOS Deploy/Documentation", false, PRIORITY)]
        public static void ISDSetupPluginSetUp()
        {
            var url = "https://unionassets.com/ios-deploy/manual";
            Application.OpenURL(url);
        }
    }
}
