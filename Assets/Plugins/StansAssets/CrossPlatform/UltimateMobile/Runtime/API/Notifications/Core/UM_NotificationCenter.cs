using UnityEngine;

namespace SA.CrossPlatform.Notifications
{
    /// <summary>
    /// The central object for managing notification-related activities for your app.
    /// </summary>
    public static class UM_NotificationCenter
    {
        static UM_iNotificationsClient s_Client = null;

        /// <summary>
        /// Returns a new instance of <see cref="UM_iNotificationsClient"/>
        /// </summary>
        public static UM_iNotificationsClient Client
        {
            get
            {
                if (s_Client == null)
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            s_Client = new UM_AndroidNotificationsClient();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            s_Client = new UM_IOSNotificationsClient();
                            break;
                        default:
                            s_Client = new UM_EditorNotificationsClient();
                            break;
                    }

                return s_Client;
            }
        }
    }
}
