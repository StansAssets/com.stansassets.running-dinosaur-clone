using System;
using SA.iOS.UserNotifications;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.Notifications
{
    class UM_IOSNotificationsClient : UM_AbstractNotificationsClient, UM_iNotificationsClient
    {
        public override void RequestAuthorization(Action<SA_Result> callback)
        {
            var options = ISN_UNAuthorizationOptions.Alert | ISN_UNAuthorizationOptions.Sound | ISN_UNAuthorizationOptions.Badge;
            ISN_UNUserNotificationCenter.RequestAuthorization(options, callback);
        }

        public UM_IOSNotificationsClient()
        {
            ISN_UNUserNotificationCenterDelegate.WillPresentNotification.AddListener(notification =>
            {
                var request = ToUMRequest(notification.Request);
                m_OnNotificationReceived.Invoke(request);
            });

            ISN_UNUserNotificationCenterDelegate.DidReceiveNotificationResponse.AddListener((ISN_UNNotificationResponse responce) =>
            {
                if (responce.ActionIdentifier.Equals(ISN_UNNotificationAction.DefaultActionIdentifier))
                {
                    var request = ToUMRequest(responce.Notification.Request);
                    m_OnNotificationClick.Invoke(request);
                }
            });
        }

        public UM_NotificationRequest LastOpenedNotification
        {
            get
            {
                var responce = ISN_UNUserNotificationCenterDelegate.LastReceivedResponse;
                if (responce == null) return null;

                return ToUMRequest(responce.Notification.Request);
            }
        }

        public void RemoveAllPendingNotifications()
        {
            ISN_UNUserNotificationCenter.RemoveAllPendingNotificationRequests();
        }

        public void RemovePendingNotification(int identifier)
        {
            ISN_UNUserNotificationCenter.RemovePendingNotificationRequests(identifier.ToString());
        }

        public void RemoveAllDeliveredNotifications()
        {
            ISN_UNUserNotificationCenter.RemoveAllDeliveredNotifications();
        }

        public void RemoveDeliveredNotification(int identifier)
        {
            ISN_UNUserNotificationCenter.RemoveDeliveredNotifications(identifier.ToString());
        }

        protected override void AddNotificationRequestInternal(UM_NotificationRequest request, Action<SA_Result> callback)
        {
            var content = new ISN_UNNotificationContent();
            content.Title = request.Content.Title;
            content.Body = request.Content.Body;
            if (request.Content.BadgeNumber != -1)
                content.Badge = request.Content.BadgeNumber;

            if (string.IsNullOrEmpty(request.Content.SoundName))
                content.Sound = ISN_UNNotificationSound.DefaultSound;
            else
                content.Sound = ISN_UNNotificationSound.SoundNamed(request.Content.SoundName);

            ISN_UNNotificationTrigger trigger = null;

            if (request.Trigger is UM_TimeIntervalNotificationTrigger)
            {
                var timeIntervalTrigger = (UM_TimeIntervalNotificationTrigger)request.Trigger;
                trigger = new ISN_UNTimeIntervalNotificationTrigger(timeIntervalTrigger.Interval, timeIntervalTrigger.Repeating);
            }

            var ios_request = new ISN_UNNotificationRequest(request.Identifier.ToString(), content, trigger);
            ISN_UNUserNotificationCenter.AddNotificationRequest(ios_request, callback);
        }

        UM_NotificationRequest ToUMRequest(ISN_UNNotificationRequest ios_request)
        {
            var content = new UM_Notification();
            content.SetTitle(ios_request.Content.Title);
            content.SetBody(ios_request.Content.Body);

            var timeIntervalTrigger = (ISN_UNTimeIntervalNotificationTrigger)ios_request.Trigger;

            var interval = timeIntervalTrigger.TimeInterval;
            var repeating = timeIntervalTrigger.Repeats;
            var trigger = new UM_TimeIntervalNotificationTrigger(interval);
            trigger.SerRepeating(repeating);

            var Identifier = Convert.ToInt32(ios_request.Identifier);
            var request = new UM_NotificationRequest(Identifier, content, trigger);

            return request;
        }
    }
}
