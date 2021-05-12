using UnityEditor;
using SA.Foundation.Editor;
using SA.iOS.XCode;
using UnityEngine;

namespace SA.iOS
{
    abstract class ISN_APIResolver : SA_iAPIResolver
    {
        ISN_XcodeRequirements m_Requirements;

        //--------------------------------------
        // Virtual
        //--------------------------------------

        public virtual void RunAdditionalPreprocess() { }

        //--------------------------------------
        // Abstract
        //--------------------------------------

        protected abstract string LibFolder { get; }
        public abstract string DefineName { get; }

        //Method is executed when Resolver is created.
        //Resolvers can be refreshed from the editor, and GenerateRequirements will be triggered again
        //Those requirement will apply / remove only on build stage
        protected abstract ISN_XcodeRequirements GenerateRequirements();
        public abstract bool IsSettingsEnabled { get; set; }

        public void ResetRequirementsCache()
        {
            m_Requirements = null;
        }

        public ISN_XcodeRequirements XcodeRequirements
        {
            get
            {
                if (m_Requirements == null)
                    m_Requirements = GenerateRequirements();

                return m_Requirements;
            }
        }

        //--------------------------------------
        // Public Methods
        //--------------------------------------

        public void Run()
        {
            if (IsSettingsEnabled)
                Enable();
            else
                Disable();

            //Defines & custom postprocess should be executed every time
            ChangeDefines(IsSettingsEnabled);
        }

        //--------------------------------------
        // Get / Set
        //--------------------------------------

        public string LibFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(LibFolder))
                    return string.Empty;

                return ISN_Settings.IOSNativeXcode + LibFolder;
            }
        }

        //--------------------------------------
        // Private Methods
        //--------------------------------------

        void Enable()
        {
            AddXcodeRequirements();
            SetNativePluginsEnableState(LibFolderPath, true);
        }

        void Disable()
        {
            RemoveXcodeRequirements();
            SetNativePluginsEnableState(LibFolderPath, false);
        }

        void SetNativePluginsEnableState(string path, bool enabled)
        {
            if (string.IsNullOrEmpty(path))
                return;

            path = path.Trim('/');
            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.LogError("Can't find the source lib folder at path: " + path);
                return;
            }

            var assets = AssetDatabase.FindAssets(string.Empty, new[] { path });
            for (var i = 0; i < assets.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assets[i]);
                var importer = AssetImporter.GetAtPath(assetPath);
                if (importer is PluginImporter pluginImporter)
                {
                    pluginImporter.SetCompatibleWithAnyPlatform(false);
                    pluginImporter.SetCompatibleWithPlatform(BuildTarget.iOS, enabled);
                }
            }
        }

        protected virtual void RemoveXcodePlistKey(ISD_PlistKey key)
        {
            ISD_API.RemoveInfoPlistKey(key);
        }

        //Method is executed on post process build only
        protected virtual void RemoveXcodeRequirements()
        {
            foreach (var framework in XcodeRequirements.Frameworks)
                ISD_API.RemoveFramework(framework.Type);

            foreach (var lib in XcodeRequirements.Libraries)
                ISD_API.RemoveLibrary(lib.Type);

            foreach (var property in XcodeRequirements.Properties)
                ISD_API.RemoveBuildProperty(property);

            foreach (var key in XcodeRequirements.PlistKeys)
                RemoveXcodePlistKey(key);
        }

        //Method is executed on post process build only
        protected virtual void AddXcodeRequirements()
        {
            foreach (var framework in XcodeRequirements.Frameworks)
                ISD_API.AddFramework(framework);

            foreach (var lib in XcodeRequirements.Libraries)
                ISD_API.AddLibrary(lib);

            foreach (var property in XcodeRequirements.Properties)
                ISD_API.SetBuildProperty(property);

            foreach (var key in XcodeRequirements.PlistKeys)
                AddXcodePlistKey(key);
        }

        protected virtual void AddXcodePlistKey(ISD_PlistKey key)
        {
            ISD_API.SetInfoPlistKey(key);
        }

        void ChangeDefines(bool isEnabled)
        {
            if (isEnabled)
                SA_EditorDefines.AddCompileDefine(DefineName, BuildTarget.iOS, BuildTarget.tvOS, BuildTarget.StandaloneOSX);
            else
                SA_EditorDefines.RemoveCompileDefine(DefineName, BuildTarget.iOS, BuildTarget.tvOS, BuildTarget.StandaloneOSX);
        }
    }
}
