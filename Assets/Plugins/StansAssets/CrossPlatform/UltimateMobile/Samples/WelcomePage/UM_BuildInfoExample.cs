using SA.CrossPlatform.App;
using SA.iOS.Foundation;
using SA.iOS.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class UM_BuildInfoExample : MonoBehaviour
{
    [SerializeField]
    Text m_LocaleInfoLabel = null;
    [SerializeField]
    Text m_BuildInfoLabel = null;

    void Start()
    {
        PrintBuildInfo();
        PrintLocaleInfo();
        PrintTimezoneInfo();

        var buildInfo = UM_Build.Info;

        Debug.Log("buildInfo.Identifier: " + buildInfo.Identifier);
        Debug.Log("buildInfo.Version: " + buildInfo.Version);
    }

    void PrintBuildInfo()
    {
        var buildInfo = UM_Build.Info;
        m_BuildInfoLabel.text = string.Format("<b>Build Info:</b> " +
            "\n Identifier: {0} " +
            "\n Version: {1}",
            buildInfo.Identifier,
            buildInfo.Version);
    }

    void PrintLocaleInfo()
    {
        var currentLocale = UM_Locale.GetCurrentLocale();
        m_LocaleInfoLabel.text = string.Format("<b>Locale Info:</b> " +
            "\n CountryCode: {0} " +
            "\n LanguageCode: {1}" +
            "\n CurrencyCode: {2}" +
            "\n CurrencySymbol: {3}",
            currentLocale.CountryCode,
            currentLocale.LanguageCode,
            currentLocale.CurrencyCode,
            currentLocale.CurrencySymbol);
    }

    void PrintTimezoneInfo()
    {
        var zone = ISN_NSTimeZone.LocalTimeZone;
        Debug.Log("LocalTimeZone.Name: " + zone.Name);
        Debug.Log("LocalTimeZone.Description: " + zone.Description);
        Debug.Log("LocalTimeZone.SecondsFromGMT: " + zone.SecondsFromGmt);

        zone = ISN_NSTimeZone.DefaultTimeZone;
        Debug.Log("DefaultTimeZone.Name: " + zone.Name);
        Debug.Log("DefaultTimeZone.Description: " + zone.Description);
        Debug.Log("DefaultTimeZone.SecondsFromGMT: " + zone.SecondsFromGmt);

        zone = ISN_NSTimeZone.SystemTimeZone;
        Debug.Log("SystemTimeZone.Name: " + zone.Name);
        Debug.Log("SystemTimeZone.Description: " + zone.Description);
        Debug.Log("SystemTimeZone.SecondsFromGMT: " + zone.SecondsFromGmt);
    }
}
