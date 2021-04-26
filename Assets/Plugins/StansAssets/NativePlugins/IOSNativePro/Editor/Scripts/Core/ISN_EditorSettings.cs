using UnityEngine;
using System.Collections.Generic;
using SA.Foundation.Config;
using SA.Foundation.Patterns;

namespace SA.iOS
{
    class ISN_EditorSettings : SA_ScriptableSingletonEditor<ISN_EditorSettings>
    {
        public List<AudioClip> NotificationAlertSounds = new List<AudioClip>();

        //--------------------------------------
        // SA_ScriptableSettings
        //--------------------------------------

        protected override string BasePath => ISN_Settings.IOSNativeFolder;

        public override string PluginName => ISN_Settings.Instance.PluginName + " Editor";

        public override string DocumentationURL => ISN_Settings.Instance.DocumentationURL;

        public override string SettingsUIMenuItem => ISN_Settings.Instance.SettingsUIMenuItem;
    }
}
