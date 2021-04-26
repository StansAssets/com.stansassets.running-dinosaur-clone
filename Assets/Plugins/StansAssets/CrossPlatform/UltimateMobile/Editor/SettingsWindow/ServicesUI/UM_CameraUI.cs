using SA.Foundation.Editor;
using SA.iOS;
using UnityEngine;

namespace SA.CrossPlatform.Editor
{
    class UM_CameraUI : UM_ServiceSettingsUI
    {
        public class ISNSettings : UM_NativeServiceLayoutBasedSetting
        {
            protected override SA_ServiceLayout Layout => CreateInstance<ISN_AVKitUI>();

            public override bool IsEnabled => ISN_Preprocessor.GetResolver<ISN_AVKitResolver>().IsSettingsEnabled;
        }

        public override void OnLayoutEnable()
        {
            base.OnLayoutEnable();
            AddPlatform(UM_UIPlatform.IOS, new ISNSettings());
            AddPlatform(UM_UIPlatform.Android, new UM_GalleryUI.ANSettings());

            AddFeatureUrl("Capture an Image", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Camera-API#capture-image-from-camera");
            AddFeatureUrl("Capture a Video", "https://github.com/StansAssets/com.stansassets.ultimate-mobile/wiki/Camera-API#capture-video-from-camera");
        }

        public override string Title => "Camera";

        protected override string Description => "Capture image or video with device camera";

        protected override Texture2D Icon => UM_Skin.GetServiceIcon("um_camera_icon.png");

        protected override void OnServiceUI() { }
    }
}
