using System;
using System.Collections.Generic;
using SA.Android.Contacts;

namespace SA.CrossPlatform.App
{
    class UM_AndroidContactsService : UM_iContactsService
    {
        public void Retrieve(Action<UM_ContactsResult> callback, bool includeNotes = false)
        {
            AN_ContactsContract.RetrieveAllAsync(result =>
            {
                UM_ContactsResult loadResult;
                if (result.IsSucceeded)
                {
                    var contacts = new List<UM_iContact>();
                    foreach (var contact in result.Contacts)
                    {
                        UM_iContact umContact = new UM_AndroidContact(contact);
                        contacts.Add(umContact);
                    }

                    loadResult = new UM_ContactsResult(contacts);
                }
                else
                {
                    loadResult = new UM_ContactsResult(result.Error);
                }

                callback.Invoke(loadResult);
            });
        }

        public void RetrieveContacts(int index, int count, Action<UM_ContactsResult> callback)
        {
            AN_ContactsContract.RetrieveAsync(index, count, result =>
            {
                UM_ContactsResult loadResult;
                if (result.IsSucceeded)
                {
                    var contacts = new List<UM_iContact>();
                    foreach (var contact in result.Contacts)
                    {
                        UM_iContact umContact = new UM_AndroidContact(contact);
                        contacts.Add(umContact);
                    }

                    loadResult = new UM_ContactsResult(contacts);
                }
                else
                {
                    loadResult = new UM_ContactsResult(result.Error);
                }

                callback.Invoke(loadResult);
            });
        }

        public int GetContactsCount()
        {
            return AN_ContactsContract.GetContactsCount();
        }
    }
}
