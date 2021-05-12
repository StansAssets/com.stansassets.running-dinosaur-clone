using System.Collections.Generic;
using SA.Foundation.Config;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEngine;

namespace SA.CrossPlatform.Editor
{
    static class UM_Packages
    {
        const string k_FacebookPackageName = "com.stansassets.facebook";
        const string k_FacebookPackageVersion = "1.0.1";
        const string k_DefaultFacebookSDKPath = "Assets/FacebookSDK";
        public const string FacebookSDKWebPage = "https://developers.facebook.com/docs/unity/";

        static ScopeRegistry StansAssetsScopeRegistry =>
            new ScopeRegistry("Stan's Assets npmjs Package Registry",
                "https://registry.npmjs.org/",
                new HashSet<string>
                {
                    "com.stansassets"
                });

        public static void InstallFacebookAddon()
        {
            if (!HasFacebookSDKFolder)
            {
                var ok = EditorUtility.DisplayDialog("Warning",
                    "Looks like you do not have Facebook SDK installed. Make sure you have Facebook SDK installed in your project" +
                    "before downloading facebook addon package. You may get a compilation errors otherwise.",
                    "Open Download Page",
                    "Continue. I know what I am doing.");

                if (ok)
                {
                    Application.OpenURL(FacebookSDKWebPage);
                    return;
                }
            }

            AddStansAssetsPackage(k_FacebookPackageName, k_FacebookPackageVersion);
        }

        public static bool HasFacebookSDKFolder => AssetDatabase.IsValidFolder(k_DefaultFacebookSDKPath);

        public static bool IsFacebookAddonInstalled
        {
            get
            {
#if SA_FACEBOOK
                return true;
#else
                return false;
#endif
            }
        }

        static void AddStansAssetsPackage(string packageName, string packageVersion)
        {
            var devKit = SA_Config.StansAssetsDevKitPackagePath.TrimEnd('/');
            var foundation = SA_Config.StansAssetsFoundationPackagePath.TrimEnd('/');

            if (AssetDatabase.IsValidFolder(devKit))
            {
                AssetDatabase.DeleteAsset(devKit);
                AssetDatabase.DeleteAsset(foundation);
            }

            AddPackage(StansAssetsScopeRegistry, packageName, packageVersion);
        }

        static void AddPackage(ScopeRegistry scopeRegistry, string packageName, string packageVersion)
        {
            var manifest = new Manifest();
            manifest.Fetch();

            var manifestUpdated = false;
            if (!manifest.TryGetScopeRegistry(scopeRegistry.Url, out _))
            {
                manifest.SetScopeRegistry(scopeRegistry.Url, scopeRegistry);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(packageName))
            {
                manifest.SetDependency(packageName, packageVersion);
                manifestUpdated = true;
            }

            if (manifestUpdated)
                manifest.ApplyChanges();
        }
    }
}
