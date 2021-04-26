using System;
using UnityEngine;
using SA.Android.Editor;
using SA.iOS.Editor;

namespace SA.CrossPlatform.Editor
{
    [Serializable]
    class UM_ExportedSettings
    {
        public string Settings => m_Settings;

        public AN_ExportedSettings AndroidSettings => m_AndroidSettings;

        public ISN_ExportedSettings ISNSettings => m_ISNSettings;

        [SerializeField]
        string m_Settings;

        [SerializeField]
        AN_ExportedSettings m_AndroidSettings;

        [SerializeField]
        ISN_ExportedSettings m_ISNSettings;

        public UM_ExportedSettings()
        {
            m_Settings = JsonUtility.ToJson(UM_Settings.Instance);
            m_AndroidSettings = AN_SettingsManager.GetExportedSettings();
            m_ISNSettings = ISN_SettingsManager.GetExportedSettings();
        }
    }
}
