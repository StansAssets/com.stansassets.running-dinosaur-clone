using UnityEngine;

namespace SA.CrossPlatform.App
{
    /// <summary>
    /// This class provides APIs for interacting with current build properties.
    /// </summary>
    public static class UM_Build
    {
        /// <summary>
        /// Returns a shared instance of <see cref="UM_iGalleryService"/>
        /// </summary>
        static UM_iBuildInfo s_Info;

        public static UM_iBuildInfo Info
        {
            get
            {
                if (s_Info == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_Info = new UM_AndroidBuildInfo();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_Info = new UM_IOSBuildInfo();
                            break;
                        default:
                            s_Info = new UM_EditorBuildInfo();
                            break;
                    }

                return s_Info;
            }
        }
    }
}
