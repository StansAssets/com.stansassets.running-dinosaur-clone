using System;
using UnityEngine;
using System.Collections.Generic;

namespace SA.CrossPlatform.Analytics
{
    class UM_MasterAnalyticsClient : UM_BaseAnalyticsClient, UM_IMasterAnalyticsClient
    {
        bool m_IsInitialized = false;
        List<UM_IAnalyticsClient> m_Clients = null;

        public override void Init()
        {
            if (m_IsInitialized)
            {
                Debug.LogError("Client was already Initialized. Make sure you init analytics only once on app start");
                return;
            }

            //All client implemented in a safe manner, so in case client service is missing
            //client will not do anything.
            m_Clients = new List<UM_IAnalyticsClient>();
            m_Clients.Add(new UM_FirebaseAnalyticsClient());
            m_Clients.Add(new UM_UnityAnalyticsClient());
#if SA_FACEBOOK
            m_Clients.Add(new UM_FacebookAnalyticsClient());
#endif

            m_IsInitialized = true;
        }

        public void Event(string eventName)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.Event(eventName);
        }

        public void Event(string eventName, IDictionary<string, object> data)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.Event(eventName, data);
        }

        public void SetUserBirthYear(int birthYear)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.SetUserBirthYear(birthYear);
        }

        public void SetUserGender(UM_Gender gender)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.SetUserGender(gender);
        }

        public void RegisterClient(UM_IAnalyticsClient client)
        {
            if (!m_IsInitialized)
            {
                throw new InvalidOperationException("Master analytics client has to be initialized before you can register more client.");
            }

            m_Clients.Add(client);
        }

        public void SetUserId(string userId)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.SetUserId(userId);
        }

        public void Transaction(string productId, float amount, string currency)
        {
            if (!m_IsInitialized)
            {
                Debug.LogError("Analytics client has to be initialized prior using any other methods.");
                return;
            }

            foreach (var client in m_Clients)
                client.Transaction(productId, amount, currency);
        }

        public override bool IsInitialized => m_IsInitialized;
    }
}
