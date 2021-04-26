using System.Collections.Generic;
using UnityEngine;
using SA.Android;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_MediaUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_AVKitUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_AVKitResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_AppFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_CoreResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Getting-Started-(Media-Player)");
            AddFeatureUrl("Play Remove Video", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Play-Remove-Video");
        }

        public override string Title => "Media Player";

        protected override string Description => "MediaPlayer class can be used to control playback of audio/video files and streams.";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_media_icon.png");

        protected override void OnServiceUI() { }
    }
}
