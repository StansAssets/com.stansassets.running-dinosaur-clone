using System;
using System.Collections.Generic;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.App
{
    class UM_EditorContactsService : UM_iContactsService
    {
        public int GetContactsCount()
        {
            return UM_Settings.Instance.EditorTestingContacts.Count;
        }

        public void Retrieve(Action<UM_ContactsResult> callback, bool includeNotes = false)
        {
            CoroutineUtility.WaitForSeconds(2f, () =>
            {
                var contacts = new List<UM_iContact>();
                foreach (var contact in UM_Settings.Instance.EditorTestingContacts) contacts.Add(contact.Clone());

                var loadResult = new UM_ContactsResult(contacts);
                callback.Invoke(loadResult);
            });
        }

        public void RetrieveContacts(int index, int count, Action<UM_ContactsResult> callback)
        {
            CoroutineUtility.WaitForSeconds(2f, () =>
            {
                var contacts = new List<UM_iContact>();
                for (var i = index; i < UM_Settings.Instance.EditorTestingContacts.Count; i++) contacts.Add(UM_Settings.Instance.EditorTestingContacts[i].Clone());

                var loadResult = new UM_ContactsResult(contacts);
                callback.Invoke(loadResult);
            });
        }
    }
}
