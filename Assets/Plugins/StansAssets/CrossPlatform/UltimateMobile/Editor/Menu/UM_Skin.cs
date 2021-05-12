using UnityEditor;
using UnityEngine;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    static class UM_Skin
    {
        const string k_IconsPath = UM_Settings.PLUGIN_FOLDER + "Editor/Art/Icons/";

        public static Texture2D SettingsWindowIcon => GetDefaultIcon(EditorGUIUtility.isProSkin 
            ? "ultimate_icon_pro.png" 
            : "ultimate_icon.png");

        public static Texture2D GetServiceIcon(string iconName)
        {
            return EditorAssetDatabase.GetTextureAtPath(k_IconsPath + "Services/" + iconName);
        }

        public static Texture2D GetPlatformIcon(string iconName)
        {
            return EditorAssetDatabase.GetTextureAtPath(k_IconsPath + "Platforms/" + iconName);
        }

        public static Texture2D GetDefaultIcon(string iconName)
        {
            return EditorAssetDatabase.GetTextureAtPath(k_IconsPath + "Default/" + iconName);
        }

        static GUIStyle s_PlatformBlockHeader = null;
        public static GUIStyle PlatformBlockHeader
        {
            get
            {
                if (s_PlatformBlockHeader == null)
                {
                    s_PlatformBlockHeader = new GUIStyle(SettingsWindowStyles.ServiceBlockHeader);
                    s_PlatformBlockHeader.fontSize = 11;
                }

                return s_PlatformBlockHeader;
            }
        }
    }
}
