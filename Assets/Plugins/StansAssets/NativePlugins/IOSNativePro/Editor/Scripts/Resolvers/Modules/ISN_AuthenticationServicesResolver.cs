using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_AuthenticationServicesResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.AuthenticationServices, true));
            return requirements;
        }

        protected override string LibFolder => "AuthenticationServices/";

        public override bool IsSettingsEnabled
        {
            get => ISD_API.Capability.SignInWithApple.Enabled;
            set
            {
                ISD_API.Capability.SignInWithApple.Enabled = value;
                ISD_Settings.Save();
            }
        }

        public override string DefineName => "AUTHENTICATION_SERVICES_API_ENABLED";
    }
}
