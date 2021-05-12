using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.iOS.XCode;
using SA.iOS.UIKit;

namespace SA.iOS
{
    class ISN_SocialResolver : ISN_LSApplicationQueriesSchemesResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();

            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.Accounts));
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.Social));
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.MessageUI));

            var LSApplicationQueriesSchemes = new ISD_PlistKey();
            LSApplicationQueriesSchemes.Name = "LSApplicationQueriesSchemes";
            LSApplicationQueriesSchemes.Type = ISD_PlistKeyType.Array;

            requirements.AddInfoPlistKey(LSApplicationQueriesSchemes);

            var instagram = new ISD_PlistKey();
            instagram.StringValue = "instagram";
            instagram.Type = ISD_PlistKeyType.String;
            LSApplicationQueriesSchemes.AddChild(instagram);

            var whatsapp = new ISD_PlistKey();
            whatsapp.StringValue = "whatsapp";
            whatsapp.Type = ISD_PlistKeyType.String;
            LSApplicationQueriesSchemes.AddChild(whatsapp);

            return requirements;
        }

        protected override string LibFolder => "Social/";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.Social;
            set => ISN_Settings.Instance.Social = value;
        }

        public override string DefineName => "SOCIAL_API_ENABLED";
    }
}
