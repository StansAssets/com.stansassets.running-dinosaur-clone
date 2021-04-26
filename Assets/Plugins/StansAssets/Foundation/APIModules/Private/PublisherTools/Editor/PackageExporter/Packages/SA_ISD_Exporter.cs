using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.iOS.XCode;
using SA.iOS;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_ISD_Exporter : SA_PluginExporter
    {
        public SA_ISD_Exporter()
            : base(ISD_Settings.PLUGIN_NAME)
        {
            SetFileSet(SA_FilesetManager.GetFileset(SA_ISD_Fileset.ID));
        }

        protected override string GetPackageSubfolder()
        {
            return "Productivity" + Path.DirectorySeparatorChar + "NativeUtils" + Path.DirectorySeparatorChar + "ISD" + Path.DirectorySeparatorChar;
        }

        public override string Version => ISD_Settings.FormattedVersion;
    }
}
