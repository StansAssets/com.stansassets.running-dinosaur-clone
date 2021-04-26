using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    public abstract class UM_PluginSettingsUI : SA_ServiceLayout
    {
        protected override IEnumerable<string> SupportedPlatforms => new List<string> { "iOS", "Android", "Unity Editor" };

        protected override int IconSize => 25;
        protected override int TitleVerticalSpace => 2;
        protected override void DrawServiceRequirements() { }
    }
}
