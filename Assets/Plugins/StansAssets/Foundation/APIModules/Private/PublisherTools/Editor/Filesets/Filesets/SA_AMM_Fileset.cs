using System.Collections.Generic;
using SA.Android.Manifest;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_AMM_Fileset : SA_PluginFileset
    {
        public const string ID = "AMM Plugin";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            return new List<string>()
            {
                AMM_Settings.MANIFEST_MANAGER_FOLDER
            };
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return AMM_Settings.PluginVersion;
        }
    }
}
