using SA.iOS.XCode;

namespace SA.iOS
{
    class ISN_EventKitResolver : ISN_APIResolver
    {
        protected override ISN_XcodeRequirements GenerateRequirements()
        {
            var requirements = new ISN_XcodeRequirements();
            requirements.AddFramework(new ISD_Framework(ISD_iOSFramework.EventKit));

            var NSCalendarUsageDescription = new ISD_PlistKey();
            NSCalendarUsageDescription.Name = "NSCalendarsUsageDescription";
            NSCalendarUsageDescription.StringValue = ISN_Settings.Instance.NsCalendarsUsageDescription;
            NSCalendarUsageDescription.Type = ISD_PlistKeyType.String;
            requirements.AddInfoPlistKey(NSCalendarUsageDescription);

            var NSReminderUsageDescription = new ISD_PlistKey();
            NSReminderUsageDescription.Name = "NSRemindersUsageDescription";
            NSReminderUsageDescription.StringValue = ISN_Settings.Instance.NsRemindersUsageDescription;
            NSReminderUsageDescription.Type = ISD_PlistKeyType.String;
            requirements.AddInfoPlistKey(NSReminderUsageDescription);

            return requirements;
        }

        protected override string LibFolder => "EventKit/";

        public override string DefineName => "EVENT_KIT_ENABLED";

        public override bool IsSettingsEnabled
        {
            get => ISN_Settings.Instance.EventKit;
            set => ISN_Settings.Instance.EventKit = value;
        }
    }
}
