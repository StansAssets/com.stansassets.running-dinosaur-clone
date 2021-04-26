using System.IO;
using SA.Android.Manifest;
using System.Collections.Generic;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_AMM_Exporter : SA_PluginExporter
    {
        string m_version;

        public SA_AMM_Exporter()
            : base(AMM_Settings.PLUGIN_NAME)
        {
            SetFileSet(SA_FilesetManager.GetFileset(SA_AMM_Fileset.ID));
        }

        protected override string GetPackageSubfolder()
        {
            return "Productivity" + Path.DirectorySeparatorChar + "NativeUtils" + Path.DirectorySeparatorChar + "AMM" + Path.DirectorySeparatorChar;
        }

        public override string Version => AMM_Settings.FormattedVersion;
    }
}
