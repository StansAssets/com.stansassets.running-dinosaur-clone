using System;

namespace SA.CrossPlatform.App
{
    /// <summary>
    /// Contacts related API service.
    /// Shared service instance is available via <see cref="UM_Application.ContactsService"/>
    /// </summary>
    public interface UM_iContactsService
    {
        /// <summary>
        /// Retrieves all contacts from a device contacts book.
        /// </summary>
        /// <param name="callback">Operation callback.</param>
        /// <param name="includeNotes">
        /// Set to `true` if you would also like to get a user notes for the contact.
        /// Please note that including the notes field requires an entitlement since IOS 13.
        /// The default value is `false`
        /// </param>
        /// <returns>The operation result callback.</returns>
        void Retrieve(Action<UM_ContactsResult> callback, bool includeNotes = false);

       /// <summary>
       /// Retrieves all contacts from a device contacts book.
       /// </summary>
       /// <param name="index">Start index.</param>
       /// <param name="count">Contacts count to retrieve.</param>
       /// <param name="callback">Operation callback.</param>
        void RetrieveContacts(int index, int count, Action<UM_ContactsResult> callback);

        /// <summary>
        /// Get contacts count from a device contacts book.
        /// </summary>
        /// <returns>Count of contacts.</returns>
        int GetContactsCount();
    }
}
