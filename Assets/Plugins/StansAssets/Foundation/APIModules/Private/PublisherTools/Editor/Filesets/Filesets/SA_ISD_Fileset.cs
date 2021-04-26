using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.CrossPlatform;
using SA.Foundation.Config;
using SA.iOS.XCode;
using SA.iOS;

namespace SA.Foundation.Publisher.Exporter
{
    public class SA_ISD_Fileset : SA_PluginFileset
    {
        public const string ID = "IOS Deploy";
        public override string Id => ID;

        public override List<string> GetDirsIncludedPaths()
        {
            var paths = base.GetDirsIncludedPaths();
            paths.Add(ISD_Settings.IOS_DEPLOY_FOLDER);
            return paths;
        }

        protected override PluginVersionHandler GetPluginVersion()
        {
            return ISD_Settings.PluginVersion;
        }
    }
}
