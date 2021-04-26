using UnityEngine;
using System;
using SA.Android.App;
using SA.iOS.Foundation;

namespace SA.CrossPlatform.App
{
    /// <summary>
    /// Information about linguistic, cultural, and technological conventions
    /// for use in formatting data for presentation.
    /// </summary>
    [Serializable]
    public class UM_Locale : AN_Locale
    {
        /// <summary>
        /// Returns The locale is formed from the settings for the current user’s chosen system locale
        /// overlaid with any custom settings the user has specified.
        /// </summary>
        public static UM_Locale GetCurrentLocale()
        {
            var unLocale = new UM_Locale();
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    var locale = GetDefault();
                    unLocale.m_CountryCode = locale.CountryCode;
                    unLocale.m_LanguageCode = locale.LanguageCode;
                    unLocale.m_CurrencySymbol = locale.CurrencySymbol;
                    unLocale.m_CurrencyCode = locale.CurrencyCode;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    var isnLocale = ISN_NSLocale.CurrentLocale;
                    unLocale.m_CountryCode = isnLocale.CountryCode;
                    unLocale.m_LanguageCode = isnLocale.LanguageCode;
                    unLocale.m_CurrencySymbol = isnLocale.CurrencySymbol;
                    unLocale.m_CurrencyCode = isnLocale.CurrencyCode;
                    break;
                default:
                    unLocale.m_CountryCode = "US";
                    unLocale.m_LanguageCode = "ENG";
                    unLocale.m_CurrencySymbol = "$";
                    unLocale.m_CurrencyCode = "USD";
                    break;
            }

            return unLocale;
        }
    }
}
