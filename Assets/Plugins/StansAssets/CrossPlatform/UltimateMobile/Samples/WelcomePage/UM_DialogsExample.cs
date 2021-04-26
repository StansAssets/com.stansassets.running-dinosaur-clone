using System;
using UnityEngine;
using UnityEngine.UI;
using SA.CrossPlatform.UI;
using System.Collections.Generic;
using SA.iOS.UIKit;
using StansAssets.Foundation.Async;

public class UM_DialogsExample : MonoBehaviour
{
    [Header("Cross-platform")]
    [SerializeField]
    Button m_Message = null;
    [SerializeField]
    Button m_Dialog = null;
    [SerializeField]
    Button m_DestructiveDialog = null;
    [SerializeField]
    Button m_ComplexDialog = null;
    [SerializeField]
    Button m_Preloader = null;
    [SerializeField]
    Button m_RateUs = null;
    [SerializeField]
    Button m_Calendar = null;
    [SerializeField]
    Button m_WheelPicker = null;

    [Header("iOS Only")]
    [SerializeField]
    Button m_DateTimePicker = null;
    [SerializeField]
    Button m_DatePicker = null;
    [SerializeField]
    Button m_TimePicker = null;
    [SerializeField]
    Button m_CountdownTimer = null;
    [SerializeField]
    Button m_UIMenuController = null;

    void Start()
    {
        m_Message.onClick.AddListener(Message);
        m_Dialog.onClick.AddListener(Dialog);
        m_DestructiveDialog.onClick.AddListener(DestructiveDialog);
        m_ComplexDialog.onClick.AddListener(ComplexDialog);
        m_Preloader.onClick.AddListener(Preloader);
        m_RateUs.onClick.AddListener(RateUsDialog);
        m_Calendar.onClick.AddListener(PickDate);
        m_WheelPicker.onClick.AddListener(WheelPicker);

        InitIOSDialogs();
    }

    void WheelPicker()
    {
        var values = new List<string> { "Test 1", "Test 2", "Test 3" };
        var picker = new UM_WheelPickerDialog(values, 1);
        picker.Show(result =>
        {
            if (result.HasError)
            {
                UM_DialogsUtility.DisplayResultMessage(result);
            }
            else
            {
                switch (result.State)
                {
                    case UM_WheelPickerState.InProgress:
                        Debug.Log($"User in progress, current picked value: {result.Value}");
                        break;
                    case UM_WheelPickerState.Done:
                        UM_DialogsUtility.ShowNotification($"User picked item: {result.Value}");
                        break;
                    case UM_WheelPickerState.Canceled:
                        UM_DialogsUtility.ShowNotification($"User canceled picker dialog.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        });
    }

    void PickDate()
    {
        var year = DateTime.Now.Year;
        var picker = new UM_DatePickerDialog(year);
        picker.Show(result =>
        {
            if (result.IsSucceeded)
            {
                Debug.Log("Date picked result.Year: " + result.Date.Year);
                Debug.Log("Date picked result.Month: " + result.Date.Month);
                Debug.Log("Date picked result.Day: " + result.Date.Day);
            }
            else
            {
                Debug.Log("Failed to pick a date: " + result.Error.FullMessage);
            }
        });
    }

    void RateUsDialog()
    {
        UM_ReviewController.RequestReview();
    }

    void Preloader()
    {
        UM_Preloader.LockScreen();
        CoroutineUtility.WaitForSeconds(2f, UM_Preloader.UnlockScreen);
    }

    /*
    string ScreenshotPath;

    void Update()
    {
        if (string.IsNullOrEmpty(ScreenshotPath))
            return;

        if (File.Exists(ScreenshotPath))
            Debug.Log("We have the file");
        else
            Debug.Log("No file");
    }
    
    ScreenCapture.CaptureScreenshot("image.png");
    ScreenshotPath = Path.Combine(Application.persistentDataPath, "image.png");
    */

    void Message()
    {
      

        var title = "Congrats";
        var message = "Your account has been verified";
        var builder = new UM_NativeDialogBuilder(title, message);
        builder.SetPositiveButton("Okay", () =>
        {
            Debug.Log("Okay button pressed");
        });

        var dialog = builder.Build();
        dialog.Show();
        CoroutineUtility.WaitForSeconds(2f, dialog.Hide);
    }

    void Dialog()
    {
        var title = "Save";
        var message = "Do you want to save your progress?";

        var builder = new UM_NativeDialogBuilder(title, message);
        builder.SetPositiveButton("Yes", () =>
        {
            Debug.Log("Yes button pressed");
        });

        builder.SetNegativeButton("No", () =>
        {
            Debug.Log("No button pressed");
        });
        var dialog = builder.Build();
        dialog.Show();
    }

    void DestructiveDialog()
    {
        var title = "Confirmation ";
        var message = "Do you want to delete this item?";
        var builder = new UM_NativeDialogBuilder(title, message);
        builder.SetNegativeButton("Cancel", () =>
        {
            Debug.Log("Cancel button pressed");
        });

        builder.SetDestructiveButton("Delete", () =>
        {
            Debug.Log("Delete button pressed");
        });

        var dialog = builder.Build();
        dialog.Show();
    }

    void ComplexDialog()
    {
        var title = "Save";
        var message = "Do you want to save your progress?";
        var builder = new UM_NativeDialogBuilder(title, message);
        builder.SetPositiveButton("Yes", () =>
        {
            Debug.Log("Yes button pressed");
        });
        builder.SetNegativeButton("No", () =>
        {
            Debug.Log("No button pressed");
        });
        builder.SetNeutralButton("Later", () =>
        {
            Debug.Log("Later button pressed");
        });
        var dialog = builder.Build();
        dialog.Show();
    }

    void InitIOSDialogs()
    {
        m_DateTimePicker.onClick.AddListener(() =>
        {
            var starDate = DateTime.Now;
            starDate = starDate.AddDays(-20);

            var picker = new ISN_UIDateTimePicker();
            picker.SetDate(starDate);

            picker.Show(date =>
            {
                UM_DialogsUtility.ShowMessage("Completed", "User picked date: " + date.ToLongDateString());
            });
        });

        m_DatePicker.onClick.AddListener(() =>
        {
            //20 days ago
            var starDate = DateTime.Now;
            starDate = starDate.AddDays(-20);

            var picker = new ISN_UIDateTimePicker();
            picker.SetDate(starDate);
            picker.DatePickerMode = ISN_UIDateTimePickerMode.Date;

            picker.Show(date =>
            {
                UM_DialogsUtility.ShowMessage("Completed", "User picked date: " + date.ToLongDateString());
            });
        });

        m_TimePicker.onClick.AddListener(() =>
        {
            //20 hours ago
            var starDate = DateTime.Now;
            starDate = starDate.AddHours(-20);

            var picker = new ISN_UIDateTimePicker();
            picker.SetDate(starDate);
            picker.DatePickerMode = ISN_UIDateTimePickerMode.Time;
            picker.MinuteInterval = 30;

            picker.Show(date =>
            {
                UM_DialogsUtility.ShowMessage("Completed", "User picked date: " + date.ToLongDateString());
            });
        });

        m_CountdownTimer.onClick.AddListener(() =>
        {
            var picker = new ISN_UIDateTimePicker();

            picker.DatePickerMode = ISN_UIDateTimePickerMode.CountdownTimer;
            picker.MinuteInterval = 10;

            //Set countdown start duration
            var hours = 5;
            var minutes = 20;
            var seconds = 0;
            var span = new TimeSpan(hours, minutes, seconds);
            picker.SetCountDownDuration(span);

            picker.Show(date =>
            {
                UM_DialogsUtility.ShowMessage("Completed", "User picked date: " + date.ToLongDateString());
            });
        });

        m_UIMenuController.onClick.AddListener(() =>
        {
            var rect = RectTransformToScreenSpace(m_UIMenuController.GetComponent<RectTransform>());
            var xPos = rect.center.x;
            var yPos = rect.center.y;
            var menuItems = new List<string> { "Button 1", "Button 2", "Button 3" };
            ISN_UIMenuController.SharedMenuController.MenuItems = menuItems;

            ISN_UIMenuController.SharedMenuController.ShowMenuFromPosition(xPos, yPos, result =>
            {
                if (menuItems.Count > result.ChosenIndex)
                {
                    var chosenItem = menuItems[result.ChosenIndex];
                    Debug.Log("User picked item " + chosenItem + " at position " + result.ChosenIndex);
                }
            });
        });
    }

    private Rect RectTransformToScreenSpace(RectTransform rectTransform)
    {
        Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);
        Rect rect = new Rect(rectTransform.position.x, Screen.height - rectTransform.position.y, size.x, size.y);
        rect.x -= (rectTransform.pivot.x * size.x);
        rect.y -= ((1.0f - rectTransform.pivot.y) * size.y);
        return rect;
    }
}
