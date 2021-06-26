using System;
using System.Collections.Generic;
using StansAssets.GoogleDoc.Localization;

namespace StansAssets.Dino.Localization
{
    public class GoogleDocLocalizationService : ILocalizationService
    {
        private LocalizationClient m_Client;
        private readonly HashSet<ILocalizationListener> m_listeners = new HashSet<ILocalizationListener>();

        public string Language => m_Client.CurrentLanguage;

        public GoogleDocLocalizationService(string language)
        {
            m_Client = LocalizationClient.Default;
            m_Client.SetLanguage(language);
        }

        public void Subscribe(ILocalizationListener listener)
        {
            m_listeners.Add(listener);
        }

        public void Unsubscribe(ILocalizationListener listener)
        {
            m_listeners.Remove(listener);
        }

        public void SetLocalization(string language)
        {
            if (Language != language)
            {
                m_Client.SetLanguage(language);
                OnLocalizationChanged();
            }
        }

        public string Get(string token)
        {
            return m_Client.GetLocalizedString(token);
        }

        private void OnLocalizationChanged()
        {
            foreach (var listener in m_listeners)
            {
                listener.LanguageChanged(Language);
            }
        }
    }
}