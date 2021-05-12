using SA.Android.App;
using SA.Android.Content.Res;
using SA.Android.OS;
using SA.iOS.UIKit;
using UnityEngine;

namespace SA.CrossPlatform.App
{
    /// <summary>
    /// Main entry point for the Application APIs.
    /// This class provides APIs and interfaces to access the Application services functionality.
    /// </summary>
    public static class UM_Application
    {
        /// <summary>
        /// Returns a shared instance of <see cref="UM_iGalleryService"/>
        /// </summary>
        static UM_iGalleryService s_GalleryService;

        public static UM_iGalleryService GalleryService
        {
            get
            {
                if (s_GalleryService == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_GalleryService = new UM_AndroidGalleryService();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_GalleryService = new UM_IOSGalleryService();
                            break;
                        default:
                            s_GalleryService = new UM_EditorGalleryService();
                            break;
                    }

                return s_GalleryService;
            }
        }

        /// <summary>
        /// Returns a shared instance of <see cref="UM_iCameraService"/>
        /// </summary>
        static UM_iCameraService s_CameraService;

        public static UM_iCameraService CameraService
        {
            get
            {
                if (s_CameraService == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_CameraService = new UM_AndroidCameraService();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_CameraService = new UM_IOSCameraService();
                            break;
                        default:
                            s_CameraService = new UM_EditorCameraService();
                            break;
                    }

                return s_CameraService;
            }
        }

        /// <summary>
        /// Returns a shared instance of <see cref="UM_iContactsService"/>
        /// </summary>
        static UM_iContactsService s_ContactsService;

        public static UM_iContactsService ContactsService
        {
            get
            {
                if (s_ContactsService == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_ContactsService = new UM_AndroidContactsService();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_ContactsService = new UM_IOSContactsService();
                            break;
                        default:
                            s_ContactsService = new UM_EditorContactsService();
                            break;
                    }

                return s_ContactsService;
            }
        }

        /// <summary>
        /// Will send an application to background.
        /// The method can be used to emulate pressing a Home button.
        /// </summary>
        public static void SendToBackground()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    AN_MainActivity.Instance.MoveTaskToBack(true);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    ISN_UIApplication.Suspend();
                    break;
            }
        }

        /// <summary>
        /// Returns `true` if user chooses dark mode UI in the device system preferences,
        /// returns `false` otherwise.
        /// </summary>
        public static bool IsDarkMode
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        // Only supported from android 10.0 (api 29)
                        if (AN_Build.VERSION.SDK_INT < AN_Build.VERSION_CODES.Q)
                        {
                            return false;
                        }

                        var configuration = AN_MainActivity.Instance.GetResources().GetConfiguration();
                        var currentNightMode = configuration.UIMode & AN_Configuration.UI_MODE_NIGHT_MASK;
                        switch (currentNightMode)
                        {
                            case AN_Configuration.UI_MODE_NIGHT_YES:
                                return true;
                            default:
                                return false;
                        }

                    case RuntimePlatform.IPhonePlayer:
                        var userInterfaceStyle = ISN_UIScreen.MainScreen.TraitCollection.UserInterfaceStyle;
                        return userInterfaceStyle == ISN_UIUserInterfaceStyle.Dark;

                    // Would you like is to add an editor settings for the default repose? 
                    default:
                        return false;
                }
            }
        }
    }
}
