using System;
using SA.iOS.Contacts;

namespace SA.CrossPlatform.App
{
    [Serializable]
    class UM_IOSContact : UM_AbstractContact, UM_iContact
    {
        public UM_IOSContact(ISN_CNContact contact)
        {
            Name = contact.Nickname;
            if (contact.Emails.Count > 0)
                Email = contact.Emails[0];

            if (contact.Phones.Count > 0)
                Phone = contact.Phones[0].FullNumber;

            OrganizationName = contact.OrganizationName;
            JobTitle = contact.JobTitle;
            Note = contact.Note;
        }
    }
}
