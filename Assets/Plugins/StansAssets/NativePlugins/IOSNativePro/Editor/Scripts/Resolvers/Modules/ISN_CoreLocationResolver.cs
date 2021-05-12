using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_CoreLocationResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.CoreLocation));

            var nsLocationWhenInUseUsageDescription = new ISD_PlistKey();
            nsLocationWhenInUseUsageDescription.Name = "NSLocationWhenInUseUsageDescription";
            nsLocationWhenInUseUsageDescription.StringValue = ISN_Settings.Instance.LocationWhenInUseUsageDescription;
            nsLocationWhenInUseUsageDescription.Type = ISD_PlistKeyType.String;
            requirements.AddInfoPlistKey(nsLocationWhenInUseUsageDescription);

            var nsLocationAlwaysAndWhenInUseUsageDescription = new ISD_PlistKey();
            nsLocationAlwaysAndWhenInUseUsageDescription.Name = "NSLocationAlwaysAndWhenInUseUsageDescription";
            nsLocationAlwaysAndWhenInUseUsageDescription.StringValue = ISN_Settings.Instance.LocationAlwaysAndWhenInUseUsageDescription;
            nsLocationAlwaysAndWhenInUseUsageDescription.Type = ISD_PlistKeyType.String;
            requirements.AddInfoPlistKey(nsLocationAlwaysAndWhenInUseUsageDescription);

            return requirements;
        }

        protected override string LibFolder => "CoreLocation/";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.CoreLocation;
            set => ISN_Settings.Instance.CoreLocation = value;
        }

        public override string DefineName => "CORE_LOCATION_API_ENABLED";
    }
}
