using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_AVKitResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.AVKit));
            return requirements;
        }

        protected override string LibFolder => "AVKit/";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.AVKit;
            set => ISN_Settings.Instance.AVKit = value;
        }

        public override string DefineName => "AVKIT_API_ENABLED";
    }
}
