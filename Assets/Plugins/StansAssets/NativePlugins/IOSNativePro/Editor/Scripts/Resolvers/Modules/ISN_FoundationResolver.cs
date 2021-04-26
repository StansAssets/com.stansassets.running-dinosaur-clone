using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_FoundationResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();

            var property = new ISD_BuildProperty("GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
            requirements.AddBuildProperty(property);

            if (ISD_API.Capability.iCloud.Enabled && ISD_API.Capability.iCloud.keyValueStorage) requirements.Capabilities.Add("iCloud");

            return requirements;
        }

        public override bool IsSettingsEnabled
        {
            get => true;
            set { }
        }

        protected override string LibFolder => string.Empty;
        public override string DefineName => string.Empty;
    }
}
