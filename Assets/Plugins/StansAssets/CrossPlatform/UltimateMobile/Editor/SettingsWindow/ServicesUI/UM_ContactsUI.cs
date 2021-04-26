using UnityEngine;
using UnityEditor;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;
using SA.CrossPlatform.App;
using StansAssets.Plugins.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_ContactsUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_ContactsUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_ContactsResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_ContactsFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_ContactsResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Contacts)");
            AddFeatureUrl("Phone Contacts", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Retrieving-Phone-Contacts");
        }

        public override string Title => "Contacts";

        protected override string Description => "Access the user's contacts and format and localize contact information.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_contact_icon.png");

        protected override void OnServiceUI()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Editor Testing")))
            {
                EditorGUILayout.HelpBox("Spesifiy contacts book entries that will be used " +
                    "while emulating API inside the editor.", MessageType.Info);
                
                SA_EditorGUILayout.ReorderablList(UM_Settings.Instance.EditorTestingContacts, GetContactDisplayName, DrawProductContent, () =>
                {
                    var name = "John Smith";
                    var phone = "1–800–854–3680";
                    var email = "johnsmith@gmail.com";

                    var contact = new UM_EditorContact(name, phone, email);
                    UM_Settings.Instance.EditorTestingContacts.Add(contact);
                });
            }
        }

        string GetContactDisplayName(UM_EditorContact contact)
        {
            return contact.Name + " (" + contact.Email + ")";
        }

        void DrawProductContent(UM_EditorContact contact)
        {
            contact.Name = IMGUILayout.TextField("Name", contact.Name);
            contact.Email = IMGUILayout.TextField("Email", contact.Email);
            contact.Phone = IMGUILayout.TextField("Phone", contact.Phone);
        }
    }
}
