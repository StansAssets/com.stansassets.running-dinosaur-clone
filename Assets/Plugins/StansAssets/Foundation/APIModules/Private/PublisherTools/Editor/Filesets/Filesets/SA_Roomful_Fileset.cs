using System.Collections.Generic;
using SA.CrossPlatform;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_Roomful_Fileset : SA_PluginFileset
    {
        public const string ID = "Roomful Plugins bundle";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();
            return paths;
        }

        public override List<string> GetExcludedPaths()
        {
            var excludes = base.GetExcludedPaths();
            return excludes;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return UM_Settings.Instance.GetPluginVersion();
        }
    }
}
