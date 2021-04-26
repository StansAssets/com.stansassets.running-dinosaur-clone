using SA.iOS;
using System.IO;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_ISN_Exporter : SA_PluginExporter
    {
        public SA_ISN_Exporter()
            : base(ISN_Settings.PluginTittle)
        {
            SetFileSet(SA_FilesetManager.GetFileset(SA_ISN_Fileset.ID));
        }

        protected override string GetPackageSubfolder()
        {
            return "NativePlugins" + Path.DirectorySeparatorChar + "ISN" + Path.DirectorySeparatorChar;
        }

        public override string Version => ISN_Settings.FormattedVersion;
    }
}
