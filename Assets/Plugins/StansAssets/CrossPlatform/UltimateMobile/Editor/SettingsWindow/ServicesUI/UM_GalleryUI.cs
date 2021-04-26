using System.Collections.Generic;
using UnityEngine;
using SA.Android;
using SA.Android.Editor;
using SA.iOS;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_GalleryUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_UIKitUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_UIKitResolver>().IsSettingsEnabled;
        }

        public class ANSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<AN_CameraAndGalleryFeaturesUI>();

            public override bool IsEnabled => AN_Preprocessor.GetResolver<AN_CameraAndGalleryResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new ANSettings());

            AddFeatureUrl("Save to Gallery", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Save-to-Gallery");
            AddFeatureUrl("Save Screenshot", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Save-to-Gallery#save-screenshot");
            AddFeatureUrl("Pick an Image", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Pick-from-Gallery#pick-an-image");
            AddFeatureUrl("Pick a Video", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Pick-from-Gallery#pick-a-video");
        }

        public override string Title => "Gallery";

        protected override string Description => "Pick image or video from the device local storage";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_gallery_icon.png");

        protected override void OnServiceUI() { }
    }
}
