using System;
using System.Collections.Generic;
using SA.iOS.Contacts;

namespace SA.CrossPlatform.App
{
    class UM_IOSContactsService : UM_iContactsService
    {
        public int GetContactsCount()
        {
            throw new NotImplementedException();
        }

        public void Retrieve(Action<UM_ContactsResult> callback, bool includeNotes = false)
        {
            ISN_CNContactStore.FetchPhoneContacts(result =>
            {
                UM_ContactsResult loadResult;
                if (result.IsSucceeded)
                {
                    var contacts = new List<UM_iContact>();
                    foreach (var contact in result.Contacts)
                    {
                        UM_iContact umContact = new UM_IOSContact(contact);
                        contacts.Add(umContact);
                    }

                    loadResult = new UM_ContactsResult(contacts);
                }
                else
                {
                    loadResult = new UM_ContactsResult(result.Error);
                }

                callback.Invoke(loadResult);
            }, includeNotes);
        }

        public void RetrieveContacts(int index, int count, Action<UM_ContactsResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}
