using System;
using UnityEngine;
using SA.iOS.UIKit;
using SA.Android.App;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Class allows to show preloader and lock application screen.
    /// </summary>
    public static class UM_Preloader
    {
        /// <summary>
        /// Locks the screen and displays a preloader spinner.
        /// </summary>
        /// <param name="progressBarText">The text will be used if system lock screen dialog has a label where text can be assigned.</param>
        public static void LockScreen(string progressBarText = "Please Wait..")
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                UnityEditor.EditorUtility.DisplayProgressBar(progressBarText, string.Empty, 0.4f);
                return;
            }
#endif
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    AN_Preloader.LockScreen(progressBarText);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    ISN_Preloader.LockScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The {Application.platform} is not supported.");
            }
        }

        /// <summary>
        /// Unlocks the screen and hide a preloader spinner
        /// In case there is no preloader displayed, method does nothing
        /// </summary>
        public static void UnlockScreen()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                UnityEditor.EditorUtility.ClearProgressBar();
                return;
            }
#endif
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    AN_Preloader.UnlockScreen();
                    break;
                case RuntimePlatform.IPhonePlayer:
                    ISN_Preloader.UnlockScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The {Application.platform} is not supported.");
            }
        }
    }
}
