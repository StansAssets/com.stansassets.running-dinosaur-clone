using System.IO;
using SA.Android;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_AN_Exporter : SA_PluginExporter
    {
        public SA_AN_Exporter()
            : base(AN_Settings.PLUGIN_NAME)
        {
            SetFileSet(SA_FilesetManager.GetFileset(SA_AN_Fileset.ID));
        }

        protected override string GetPackageSubfolder()
        {
            return "NativePlugins" + Path.DirectorySeparatorChar + "AN" + Path.DirectorySeparatorChar;
        }

        public override string Version => AN_Settings.FormattedVersion;
    }
}
