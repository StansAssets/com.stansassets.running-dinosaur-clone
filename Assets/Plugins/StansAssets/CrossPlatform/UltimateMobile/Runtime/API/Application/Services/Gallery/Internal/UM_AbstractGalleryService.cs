using System;
using UnityEngine;
using SA.Foundation.Templates;
using SA.Foundation.Utility;

namespace SA.CrossPlatform.App
{
    abstract class UM_AbstractGalleryService
    {
        public void SaveScreenshot(string fileName, Action<SA_Result> callback)
        {
            SA_ScreenUtil.TakeScreenshot(1024, image =>
            {
                SaveImage(image, fileName, callback);
            });
        }

        public abstract void SaveImage(Texture2D image, string fileName, Action<SA_Result> callback);
    }
}
