using System.Collections.Generic;
using SA.CrossPlatform;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_UCL_Fileset : SA_PluginFileset
    {
        public const string ID = "UCL Plugin";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();

            // paths.Add(UCL_PlatfromsLogSettings.PLUGIN_FOLDER);
            return paths;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return null;
        }
    }
}
