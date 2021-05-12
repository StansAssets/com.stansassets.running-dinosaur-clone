using System;
using UnityEngine;
using UnityEngine.UI;
using SA.Android.App;
using SA.Android.Content;
using SA.Android.Provider;
using SA.CrossPlatform.App;
using SA.CrossPlatform.UI;
using SA.CrossPlatform.Notifications;
using SA.Foundation.Utility;
using SA.iOS.UIKit;
using SA.iOS.UserNotifications;
using StansAssets.Foundation;

public class UM_LocalNotificationsExample : MonoBehaviour
{
    const string k_NotificationIconSampleFile = "notifications_test_icon.png";
    const string k_NotificationSoundSampleFile = "notifications_test_sound.wav";

    [Header("Unified API Buttons")]
    [SerializeField]
    Button m_RequestAuthorization = null;
    [SerializeField]
    Button m_Create5SecNotification = null;
    [SerializeField]
    Button m_Create20SecNotification = null;
    [SerializeField]
    Button m_RemoveDelivered = null;
    [SerializeField]
    Button m_RemovePending = null;

    [Header("Debug Actions")]
    [SerializeField]
    Button m_CloseApp = null;
    [SerializeField]
    Button m_ToBackground = null;

    [Header("Android Only")]
    [SerializeField]
    Button m_LargeImageStyle = null;
    [SerializeField]
    Button m_LargeTextStyle = null;

    [SerializeField]
    Button m_OpenNotificationSettingsButton = default;
    
    [Header("iOS Only")]
    [SerializeField]
    InputField m_iOSNotificationTitle = null;
    [SerializeField]
    InputField m_iOSNotificationBody = null;
    [SerializeField]
    InputField m_iOSNotificationSubtitle = null;
    [SerializeField]
    InputField m_iOSNotificationBadge = null;

    [SerializeField]
    Button m_RegisterForRemoteNotifications = null;
    [SerializeField]
    Button m_iOSNotificationSchegule = null;

    [Header("iOS Debug Actions")]
    [SerializeField]
    Button m_iOSClearAppBadges = null;

    void Start()
    {
        var client = UM_NotificationCenter.Client;

        m_RequestAuthorization.onClick.AddListener(() =>
        {
            client.RequestAuthorization(UM_DialogsUtility.DisplayResultMessage);
        });

        m_Create5SecNotification.onClick.AddListener(() => { CreateNotificationWithInterval(5); });
        m_Create20SecNotification.onClick.AddListener(() => { CreateNotificationWithInterval(20); });

        m_RemoveDelivered.onClick.AddListener(() =>
        {
            client.RemoveAllDeliveredNotifications();
        });

        m_RemovePending.onClick.AddListener(() =>
        {
            client.RemoveAllPendingNotifications();
        });

        m_CloseApp.onClick.AddListener(Application.Quit);

        m_ToBackground.onClick.AddListener(() => { UM_Application.SendToBackground(); });

        iOSOnlySetup();
        AndroidOnlySetup();
    }

    /// Uncomment if you wan to test Notification scheduling on application pause
    /*
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            CreateNotificationWithInterval(5);
        }
    }*/
    void AndroidOnlySetup()
    {
        m_LargeImageStyle.onClick.AddListener(AndroidBigPictureStyle);
        m_LargeTextStyle.onClick.AddListener(AndroidBigTextStyle);
        
        m_OpenNotificationSettingsButton.onClick.AddListener(() =>
        {
            var intent = new AN_Intent(AN_Settings.ACTION_NOTIFICATION_POLICY_ACCESS_SETTINGS);
            AN_MainActivity.Instance.StartActivity(intent);
        });
    }

    void iOSOnlySetup()
    {
        m_RegisterForRemoteNotifications.onClick.AddListener(ISN_UIApplication.RegisterForRemoteNotifications);
        ISN_UIApplication.ApplicationDelegate.DidRegisterForRemoteNotifications.AddListener((result) =>
        {
            if (result.IsSucceeded)
            {
                var token = result.DeviceTokenUTF8;
                UM_DialogsUtility.ShowMessage("Register For Remote Notifications", "ANS token string:" + token);
                Debug.Log("ANS token string:" + token);
            }
            else
            {
                UM_DialogsUtility.ShowMessage("Register For Remote Notifications", "Error: " + result.Error.Message);
                Debug.Log("Error: " + result.Error.Message);
            }
        });

        m_iOSNotificationTitle.text = "Wake up!";
        m_iOSNotificationBody.text = "Rise and shine! It's morning time!";
        m_iOSNotificationBadge.text = "1";

        m_iOSNotificationSchegule.onClick.AddListener(ScheduleIOSNotification);
        m_iOSClearAppBadges.onClick.AddListener(() =>
        {
            ISN_UIApplication.ApplicationIconBadgeNumber = 0;
        });
    }

    void ScheduleIOSNotification()
    {
        var content = new ISN_UNNotificationContent();
        content.Title = m_iOSNotificationTitle.text;
        content.Body = m_iOSNotificationBody.text;
        content.Subtitle = m_iOSNotificationSubtitle.text;
        content.Badge = Convert.ToInt32(m_iOSNotificationBadge.text);

        var seconds = 5;
        var repeats = false;
        var trigger = new ISN_UNTimeIntervalNotificationTrigger(seconds, repeats);

        var identifier = "TestAlarm";
        var request = new ISN_UNNotificationRequest(identifier, content, trigger);

        ISN_UNUserNotificationCenter.AddNotificationRequest(request, (result) =>
        {
            Debug.Log("iOS AddNotificationRequest: " + result.IsSucceeded);
        });
    }

    void CreateNotificationWithInterval(int delay)
    {
        var client = UM_NotificationCenter.Client;
        var content = new UM_Notification();

        content.SetTitle("Title X");
        content.SetBody("Body message X");
        content.SetSoundName(k_NotificationSoundSampleFile);
        content.SetSmallIconName(k_NotificationIconSampleFile);

        var requestId = IdFactory.NextId;

        //2 seconds
        var trigger = new UM_TimeIntervalNotificationTrigger(delay);
        var request = new UM_NotificationRequest(requestId, content, trigger);
        client.AddNotificationRequest(request, (result) =>
        {
            if (result.IsSucceeded)
                UM_DialogsUtility.ShowMessage("Succeeded", "Notification was successfully scheduled.");
            else
                UM_DialogsUtility.ShowMessage("Failed", result.Error.FullMessage);
        });
    }

    void AndroidBigPictureStyle()
    {
        SA_ScreenUtil.TakeScreenshot(256, (screenshot) =>
        {
            var builder = new AN_NotificationCompat.Builder();
            builder.SetContentText("Big Picture Style");
            builder.SetContentTitle("Big Picture Style title");
            var bigPictureStyle = new AN_NotificationCompat.BigPictureStyle();

            bigPictureStyle.BigPicture(screenshot);
            bigPictureStyle.BigLargeIcon(screenshot);
            builder.SetStyle(bigPictureStyle);
            builder.SetDefaults(AN_Notification.DEFAULT_ALL);

            var trigger = new AN_AlarmNotificationTrigger();
            trigger.SetDate(TimeSpan.FromSeconds(1));

            var id = IdFactory.NextId;
            var request = new AN_NotificationRequest(id, builder, trigger);
            AN_NotificationManager.Schedule(request);
        });
    }

    void AndroidBigTextStyle()
    {
        SA_ScreenUtil.TakeScreenshot(256, (screenshot) =>
        {
            var builder = new AN_NotificationCompat.Builder();
            builder.SetContentText("Big Text Style");
            builder.SetContentTitle("Big TextStyle Title");

            var bigTextStyle = new AN_NotificationCompat.BigTextStyle();
            bigTextStyle.BigText("This is test big text style");
            builder.SetStyle(bigTextStyle);
            builder.SetDefaults(AN_Notification.DEFAULT_ALL);

            var trigger = new AN_AlarmNotificationTrigger();
            trigger.SetDate(TimeSpan.FromSeconds(1));

            var id = IdFactory.NextId;
            var request = new AN_NotificationRequest(id, builder, trigger);
            AN_NotificationManager.Schedule(request);
        });
    }

    public static void SubscribeToTheNotificationEvents()
    {
        var client = UM_NotificationCenter.Client;
        var startRequest = client.LastOpenedNotification;
        if (startRequest != null) UM_DialogsUtility.ShowMessage("Launched via Notification", GetNotificationInfo(startRequest));

        //if this isn't null on your app launch, means user launched your app by clicking on notification icon

        client.OnNotificationReceived.AddListener(request =>
        {
            //Notification was received while app is running
            UM_DialogsUtility.ShowMessage("Notification Received", GetNotificationInfo(request));
        });

        client.OnNotificationClick.AddListener(request =>
        {
            //User clicked on notification while app is running
            UM_DialogsUtility.ShowMessage("Restored from background via Notification", GetNotificationInfo(request));
        });
    }

    static string GetNotificationInfo(UM_NotificationRequest request)
    {
        return string.Format("request.Identifier: {0} \n request.Content.Title: {1}",
            request.Identifier,
            request.Content.Title);
    }
}
