using System;

namespace StansAssets.Dino.Localization
{
    public interface ILocalizationService
    {
        string Language { get; }

        void Subscribe(ILocalizationListener listener);
        void Unsubscribe(ILocalizationListener listener);
        void SetLocalization(string language);

        string Get(string token);
    }

    public interface ILocalizationListener
    {
        string Name { get; }
        void LanguageChanged(string language);
    }
}