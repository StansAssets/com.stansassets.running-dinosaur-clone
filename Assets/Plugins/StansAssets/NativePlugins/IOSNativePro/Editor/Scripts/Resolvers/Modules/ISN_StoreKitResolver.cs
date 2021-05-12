using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_StoreKitResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.StoreKit));
            requirements.Capabilities.Add("In-App Purchase");
            return requirements;
        }

        protected override string LibFolder => "StoreKit/";

        public override bool IsSettingsEnabled
        {
            get => ISD_API.Capability.InAppPurchase.Enabled;
            set
            {
                ISD_API.Capability.InAppPurchase.Enabled = value;
                ISD_Settings.Save();
            }
        }

        public override string DefineName => "STORE_KIT_API_ENABLED";
    }
}
