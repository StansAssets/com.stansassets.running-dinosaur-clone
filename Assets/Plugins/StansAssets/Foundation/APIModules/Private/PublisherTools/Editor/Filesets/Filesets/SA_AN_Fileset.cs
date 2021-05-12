using System.Collections.Generic;
using SA.Android;
using SA.Android.Manifest;
using SA.Foundation.Config;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_AN_Fileset : SA_PluginFileset
    {
        public const string ID = "Android Native Plugin";
        public override string Id => ID;

        public override List<string> GetExcludedPaths()
        {
            var excludes = base.GetExcludedPaths();
            excludes.Add(".*AndroidManifest.xml");
            excludes.Add(".*games-ids.xml");
            excludes.Add(".*AMM_EditorMenu.cs");
            excludes.Add(".*notifications_test_icon.png");
            excludes.Add(".*notifications_test_sound.wav");
            excludes.Add(AN_Settings.DependenciesFolder);

            return excludes;
        }

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();
            paths.Add(AN_Settings.AndroidNativeFolder);
            paths.Add(AMM_Settings.MANIFEST_MANAGER_FOLDER);
            return paths;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return AN_Settings.PluginVersion;
        }
    }
}
