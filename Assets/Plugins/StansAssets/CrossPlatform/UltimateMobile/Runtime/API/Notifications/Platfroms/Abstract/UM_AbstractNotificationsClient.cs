using SA.Foundation.Events;
using SA.Foundation.Templates;
using System;

namespace SA.CrossPlatform.Notifications
{
    abstract class UM_AbstractNotificationsClient
    {
        protected readonly SA_Event<UM_NotificationRequest> m_OnNotificationClick = new SA_Event<UM_NotificationRequest>();
        protected readonly SA_Event<UM_NotificationRequest> m_OnNotificationReceived = new SA_Event<UM_NotificationRequest>();

        public abstract void RequestAuthorization(Action<SA_Result> callback);
        protected abstract void AddNotificationRequestInternal(UM_NotificationRequest request, Action<SA_Result> callback);

        public void AddNotificationRequest(UM_NotificationRequest request, Action<SA_Result> callback)
        {
            AddNotificationRequestInternal(request, callback);
        }

        public SA_iEvent<UM_NotificationRequest> OnNotificationClick => m_OnNotificationClick;

        public SA_iEvent<UM_NotificationRequest> OnNotificationReceived => m_OnNotificationReceived;
    }
}
