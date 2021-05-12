using System.IO;
using SA.CrossPlatform;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_UM_Exporter : SA_PluginExporter
    {
        public SA_UM_Exporter()
            : base(UM_Settings.PLUGIN_NAME)
        {
            SetFileSet(SA_FilesetManager.GetFileset(SA_UM_Fileset.ID));
        }

        protected override string GetPackageSubfolder()
        {
            return "CrossPlatform" + Path.DirectorySeparatorChar + "UM" + Path.DirectorySeparatorChar;
        }

        public override string Version => UM_Settings.FormattedVersion;
    }
}
