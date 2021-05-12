using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_AssetsLibraryResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.AssetsLibrary));
            return requirements;
        }

        protected override string LibFolder => "AssetsLibrary/";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.AssetsLibrary;
            set => ISN_Settings.Instance.AssetsLibrary = value;
        }

        public override string DefineName => "CORE_LOCATION_API_ENABLED";
    }
}
