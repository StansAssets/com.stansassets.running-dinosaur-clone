using SA.Android.Utilities;
using SA.CrossPlatform.App;
using UnityEngine;
using UnityEngine.UI;

namespace SA.CrossPlatform.Samples
{
    public class UM_ContactsSample : MonoBehaviour
    {
        [SerializeField]
        Button m_LoadAllContactsAsyncButton = null;
        [SerializeField]
        Button m_LoadContactsAsyncButton = null;
        [SerializeField]
        Button m_LoadAllContactsButton = null;
        [SerializeField]
        Button m_LoadContactsButton = null;
        [SerializeField]
        Button m_LoadContactsCountButton = null;
        UM_iContactsService m_Client;

        void Start()
        {
            m_Client = UM_Application.ContactsService;
            m_LoadAllContactsAsyncButton.onClick.AddListener(() =>
            {
                LoadAllContactsAsync();
            });
            m_LoadContactsAsyncButton.onClick.AddListener(() =>
            {
                LoadContactAsync(0, 5);
            });
            m_LoadAllContactsButton.onClick.AddListener(() =>
            {
                LoadAllContacts();
            });
            m_LoadContactsButton.onClick.AddListener(() =>
            {
                LoadContact(0, 5);
            });
            m_LoadContactsCountButton.onClick.AddListener(() =>
            {
                GetContactsCount();
            });
        }

        void LoadAllContactsAsync()
        {
            m_Client.Retrieve(result =>
            {
                LogContacts(result);
            });
        }

        void LoadContactAsync(int index, int count)
        {
            m_Client.RetrieveContacts(index, count, result =>
            {
                LogContacts(result);
            });
        }

        void LoadAllContacts()
        {
            var result = Android.Contacts.AN_ContactsContract.RetrieveAll();
            LogContacts(result);
        }

        void LoadContact(int index, int count)
        {
            var result = Android.Contacts.AN_ContactsContract.Retrieve(index, count);
            LogContacts(result);
        }

        void GetContactsCount()
        {
            Debug.Log("---------->");
            Debug.Log("Contacts count " + m_Client.GetContactsCount());
        }

        void LogContacts(Android.Contacts.AN_ContactsResult result)
        {
            if (result.IsSucceeded)
                foreach (var contact in result.Contacts)
                {
                    Debug.Log("---------->");
                    Debug.Log("contact.Name:" + contact.Name);
                    Debug.Log("contact.Phone:" + contact.Phone);
                    Debug.Log("contact.Email:" + contact.Email);
                }
            else
                Debug.Log("Failed to load contacts: " + result.Error.FullMessage);
        }

        void LogContacts(UM_ContactsResult result)
        {
            if (result.IsSucceeded)
                foreach (var contact in result.Contacts)
                {
                    Debug.Log("---------->");
                    Debug.Log("contact.Name:" + contact.Name);
                    Debug.Log("contact.Phone:" + contact.Phone);
                    Debug.Log("contact.Email:" + contact.Email);
                }
            else
                Debug.Log("Failed to load contacts: " + result.Error.FullMessage);
        }
    }
}
