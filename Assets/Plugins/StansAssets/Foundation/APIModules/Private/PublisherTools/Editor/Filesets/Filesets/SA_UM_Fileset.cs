using System.Collections.Generic;
using SA.CrossPlatform;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_UM_Fileset : SA_PluginFileset
    {
        public const string ID = "Ultimate Mobile Plugin";
        public const string GIF_PLUGIN_FOLDER = SA_Config.StansAssetsCrossPlatformPluginsPath + "SocialGif/";
        public override string Id => ID;

        public override List<string> GetExcludedPaths()
        {
            var excludes = base.GetExcludedPaths();

            excludes.AddRange(new SA_AN_Fileset().GetExcludedPaths());
            excludes.AddRange(new SA_ISN_Fileset().GetExcludedPaths());

            return excludes;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return UM_Settings.PluginVersion;
        }

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();
            paths.AddRange(new SA_AN_Fileset().GetDirsIncludedPaths());
            paths.AddRange(new SA_ISN_Fileset().GetDirsIncludedPaths());

            paths.Add(UM_Settings.PLUGIN_FOLDER);
            paths.Add(GIF_PLUGIN_FOLDER);

            return paths;
        }
    }
}
