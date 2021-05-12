using System;
using SA.Android.Contacts;

namespace SA.CrossPlatform.App
{
    [Serializable]
    class UM_AndroidContact : UM_AbstractContact, UM_iContact
    {
        public UM_AndroidContact(AN_ContactInfo contact)
        {
            Name = contact.Name;
            Email = contact.Email;
            Phone = contact.Phone;
            OrganizationName = contact.Organization.Name;
            JobTitle = contact.Organization.Title;
            Note = contact.Note;
        }
    }
}
