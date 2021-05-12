using System;

namespace SA.CrossPlatform.App
{
    /// <summary>
    /// Camera related API service.
    /// Shared service instance is available via <see cref="UM_Application.CameraService"/>
    /// </summary>
    public interface UM_iCameraService
    {
        /// <summary>
        /// Take picture using the device camera.
        /// </summary>
        /// <param name="maxThumbnailSize">
        /// Max image size. If picture size is bigger then imageSize value,
        /// picture will be scaled to meet the requirements
        /// before transferring from native to unity side.
        /// </param>
        /// <param name="callback">Operation callback.</param>
        void TakePicture(int maxThumbnailSize, Action<UM_MediaResult> callback);

        /// <summary>
        /// Take video using the device camera.
        /// </summary>
        /// <param name="maxThumbnailSize">
        /// Max image size. If picture size is bigger then imageSize value,
        /// picture will be scaled to meet the requirements
        /// before transferring from native to unity side.
        /// </param>
        /// <param name="callback">Operation callback.</param>
        void TakeVideo(int maxThumbnailSize, Action<UM_MediaResult> callback);
    }
}
