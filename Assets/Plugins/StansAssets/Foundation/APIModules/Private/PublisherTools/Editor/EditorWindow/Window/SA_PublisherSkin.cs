using UnityEngine;
using UnityEditor;
using SA.Foundation.Config;
using StansAssets.Plugins.Editor;

namespace SA.Foundation.Publisher
{
    public static class SA_PublisherSkin
    {
        const string k_IconsPath = SA_Config.StansAssetsFoundationApiModulesPathPrivate + "PublisherTools/Editor/Art/Icons/";

        public static Texture2D WindowIcon => EditorGUIUtility.isProSkin 
            ? EditorAssetDatabase.GetTextureAtPath(k_IconsPath + "publish_icon_pro.png") 
            : EditorAssetDatabase.GetTextureAtPath(k_IconsPath + "publish_icon.png");
    }
}
