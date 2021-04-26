using System.Collections.Generic;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_FoundationFileset : SA_PluginFileset
    {
        public const string ID = "Foundation";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            return new List<string>()
            {
                SA_Config.StansAssetsFoundationPath,
                SA_Config.StansAssetsFoundationPackagePath,
                SA_Config.StansAssetsDevKitPackagePath
            };
        }

        public override List<string> GetFilesIncludedPaths()
        {
            return new List<string>()
            {
                SA_Config.StansAssetsThirdPartyNotices
            };
        }

        public override List<string> GetExcludedPaths()
        {
            return new List<string>()
            {
                SA_Config.StansAssetsFoundationApiModulesPathPublic + "EditorStylesCollection/" + ".*",
                SA_Config.StansAssetsFoundationApiModulesPathPrivate + ".*"
            };
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return SA_Config.FoundationVersion;
        }
    }
}
