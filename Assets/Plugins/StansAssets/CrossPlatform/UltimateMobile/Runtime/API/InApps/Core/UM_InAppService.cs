using UnityEngine;

namespace SA.CrossPlatform.InApp
{
    /// <summary>
    /// Main entry point for the In-App Purchases API.
    /// This class provides API and interfaces to access the in-game billing functionality.
    /// </summary>
    public static class UM_InAppService
    {
        public const string TEST_ITEM_PURCHASED = "android.test.purchased";
        public const string TEST_ITEM_UNAVAILABLE = "android.test.item_unavailable";

        static UM_iInAppClient s_Client;
        static UM_AndroidInAppUtilities s_AndroidInAppUtilities;

        /// <summary>
        /// Returns a new instance of <see cref="UM_iInAppClient"/>
        /// </summary>
        public static UM_iInAppClient Client
        {
            get
            {
                if (s_Client == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_Client = new UM_AndroidInAppClient();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_Client = new UM_IOSInAppClient();
                            break;
                        default:
                            s_Client = new UM_EditorInAppClient();
                            break;
                    }

                return s_Client;
            }
        }

        /// <summary>
        /// Utilities collection for the Android platform.
        /// </summary>
        public static UM_AndroidInAppUtilities AndroidUtilities
        {
            get
            {
                if (s_AndroidInAppUtilities == null)
                    s_AndroidInAppUtilities = new UM_AndroidInAppUtilities();

                return s_AndroidInAppUtilities;
            }
        }
    }
}
