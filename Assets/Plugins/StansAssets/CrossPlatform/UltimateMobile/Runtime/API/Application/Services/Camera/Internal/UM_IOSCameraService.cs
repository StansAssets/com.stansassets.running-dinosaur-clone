using System;
using System.Collections.Generic;
using SA.iOS.AVFoundation;
using SA.iOS.UIKit;

namespace SA.CrossPlatform.App
{
    class UM_IOSCameraService : UM_iCameraService
    {
        public void TakePicture(int thumbnailSize, Action<UM_MediaResult> callback)
        {
            CaptureMedia(thumbnailSize, UM_MediaType.Image, ISN_UIImagePickerControllerSourceType.Camera, callback);
        }

        public void TakeVideo(int thumbnailSize, Action<UM_MediaResult> callback)
        {
            CaptureMedia(thumbnailSize, UM_MediaType.Video, ISN_UIImagePickerControllerSourceType.Camera, callback);
        }

        internal static void CaptureMedia(int thumbnailSize, UM_MediaType type, ISN_UIImagePickerControllerSourceType source, Action<UM_MediaResult> callback)
        {
            var picker = new ISN_UIImagePickerController();
            picker.SourceType = source;
            switch (type)
            {
                case UM_MediaType.Image:
                    picker.MediaTypes = new List<string> { ISN_UIMediaType.Image };
                    break;
                case UM_MediaType.Video:
                    picker.MediaTypes = new List<string> { ISN_UIMediaType.Movie };
                    break;
            }

            picker.MaxImageSize = thumbnailSize;
            picker.ImageCompressionFormat = ISN_UIImageCompressionFormat.JPEG;
            picker.ImageCompressionRate = 0.8f;

            UM_MediaResult pickResult;
            picker.Present(result =>
            {
                if (result.IsSucceeded)
                {
                    UM_Media media = null;
                    switch (result.MediaType)
                    {
                        case ISN_UIMediaType.Image:
                            media = new UM_Media(result.Image, result.RawBytes, result.ImageURL, UM_MediaType.Image);
                            break;
                        case ISN_UIMediaType.Movie:
                            var img = ISN_AVAssetImageGenerator.CopyCgImageAtTime(result.OriginalMediaURL, 0);
                            media = new UM_Media(img, result.RawBytes, result.MediaURL, UM_MediaType.Video);
                            break;
                    }

                    pickResult = new UM_MediaResult(media);
                }
                else
                {
                    pickResult = new UM_MediaResult(result.Error);
                }

                callback.Invoke(pickResult);
            });
        }
    }
}
