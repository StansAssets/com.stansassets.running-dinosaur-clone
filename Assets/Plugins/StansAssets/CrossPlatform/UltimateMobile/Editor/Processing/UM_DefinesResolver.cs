using UnityEditor;
using SA.Foundation.Editor;
using SA.Foundation.Utility;
using SA.Foundation.UtilitiesEditor;
using UnityEngine;

namespace SA.CrossPlatform.Editor
{
    [InitializeOnLoad]
    class UM_DefinesResolver : SA_PluginInstallationProcessor<UM_Settings>
    {
        const string k_AdmobLibFolderName = "GoogleMobileAds";
        const string k_AdmobInstalledDefine = "SA_ADMOB_INSTALLED";

        public const string UnityAdsPackageName = "com.unity.ads";

        static UM_DefinesResolver()
        {
            var installation = new UM_DefinesResolver();
            installation.Init();
        }

        //--------------------------------------
        //  SA_PluginInstallationProcessor
        //--------------------------------------

        protected override void OnInstall()
        {
            // Let's check if we have FB SKD in the project.
            ProcessAssets();
        }

        //--------------------------------------
        //  Public Methods
        //--------------------------------------

        public static void ProcessAssets()
        {
            // We are looking for folder.
            var projectFolders = SA_AssetDatabase.FindAssetsWithExtentions("Assets", "");
            foreach (var lib in projectFolders) ProcessAssetImport(lib);

            // We are looking for dll libs.
            var projectLibs = SA_AssetDatabase.FindAssetsWithExtentions("Assets", ".dll");
            foreach (var lib in projectLibs) ProcessAssetImport(lib);
        }

        public static void ProcessAssetImport(string assetPath)
        {
            CheckForAdMobSdk(assetPath, true);
        }

        public static void ProcessAssetDelete(string assetPath)
        {
            CheckForAdMobSdk(assetPath, false);
        }

        //--------------------------------------
        //  Get / Set
        //--------------------------------------

        public static bool IsAdMobInstalled
        {
            get
            {
#if SA_ADMOB_INSTALLED
                return true;
#else
                return false;
#endif
            }
        }

        public static bool IsUnityAdsInstalled => IsPackageInstalled(UnityAdsPackageName);


        public static bool IsPlayMakerInstalled
        {
            get
            {
#if PLAYMAKER
                return true;
#else
                return false;
#endif
            }
        }


        public static bool IsPackageInstalled(string packageName)
        {
            return AssetDatabase.LoadAssetAtPath<Object>(GetPackageRootPath(packageName)) != null;
        }

        /// <summary>
        /// Returns package root path based on package name.
        /// </summary>
        /// <param name="packageName">Package name.</param>
        /// <returns>Package root path.</returns>
        public static string GetPackageRootPath(string packageName) => "Packages/" + packageName;

        //--------------------------------------
        //  Private Methods
        //--------------------------------------

        static void CheckForAdMobSdk(string assetPath, bool enable)
        {
            var fileName = SA_PathUtil.GetFileName(assetPath);
            if (fileName.Equals(k_AdmobLibFolderName)) UpdateSdkDefine(enable, k_AdmobInstalledDefine);
        }

        static void UpdateSdkDefine(bool enabled, string define)
        {
            if (enabled)
            {
                if (!SA_EditorDefines.HasCompileDefine(define)) SA_EditorDefines.AddCompileDefine(define);
            }
            else
            {
                if (SA_EditorDefines.HasCompileDefine(define)) SA_EditorDefines.RemoveCompileDefine(define);
            }
        }
    }
}
