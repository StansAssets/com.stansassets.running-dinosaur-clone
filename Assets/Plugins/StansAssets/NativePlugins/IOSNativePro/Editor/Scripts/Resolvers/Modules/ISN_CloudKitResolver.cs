using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_CloudKitResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.CloudKit));
            return requirements;
        }

        protected override string LibFolder => "CloudKit/";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.CloudKit;
            set => ISN_Settings.Instance.CloudKit = value;
        }

        public override string DefineName => "CLOUDKIT_API_ENABLED";
    }
}
