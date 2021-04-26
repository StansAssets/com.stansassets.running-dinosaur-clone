using System.Collections.Generic;
using SA.CrossPlatform;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_GD_Fileset : SA_PluginFileset
    {
        public const string PLUGIN_FOLDER = SA_Config.StansAssetsProductivityPluginsPath + "GoogleSheets/";
        public const string ID = "GD Plugin";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();
            paths.Add(PLUGIN_FOLDER);
            return paths;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return null;
        }
    }
}
