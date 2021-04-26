using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.iOS.XCode
{
    public static class ISD_Skin
    {
        public const string ICONS_PATH = ISD_Settings.IOS_DEPLOY_FOLDER + "Editor/Art/Icons/";
        public const string CAPABILITY_ICONS_PATH = ISD_Settings.IOS_DEPLOY_FOLDER + "Editor/Art/CapabilityIcon/";

        public static Texture2D WindowIcon
        {
            get
            {
                if (EditorGUIUtility.isProSkin)
                    return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + "isd_pro.png");
                else
                    return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + "isd.png");
            }
        }

        public static Texture2D GetIcon(string iconName)
        {
            return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + iconName);
        }
    }
}
