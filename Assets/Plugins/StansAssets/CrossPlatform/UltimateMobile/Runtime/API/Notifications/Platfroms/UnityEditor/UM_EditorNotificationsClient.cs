using SA.Foundation.Templates;
using System;
using StansAssets.Foundation.Async;

namespace SA.CrossPlatform.Notifications
{
    class UM_EditorNotificationsClient : UM_AbstractNotificationsClient, UM_iNotificationsClient
    {
        public UM_NotificationRequest LastOpenedNotification => null;

        public void RemoveAllDeliveredNotifications() { }

        public void RemoveAllPendingNotifications() { }

        public void RemoveDeliveredNotification(int identifier) { }

        public void RemovePendingNotification(int identifier) { }

        public override void RequestAuthorization(Action<SA_Result> callback)
        {
            callback.Invoke(new SA_Result());
        }

        protected override void AddNotificationRequestInternal(UM_NotificationRequest request, Action<SA_Result> callback)
        {
            CoroutineUtility.WaitForSeconds(1f, () =>
            {
                callback.Invoke(new SA_Result());
            });
        }
    }
}
