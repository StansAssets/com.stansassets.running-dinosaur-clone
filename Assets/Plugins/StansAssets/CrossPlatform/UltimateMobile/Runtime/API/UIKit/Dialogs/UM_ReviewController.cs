using UnityEngine;
using SA.iOS.StoreKit;
using SA.Android.App;
using SA.Android.Content;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Controls the process of requesting App Store ratings and reviews from users.
    /// </summary>
    public static class UM_ReviewController
    {
        /// <summary>
        /// Use the <see cref="RequestReview"/>  method to indicate when it makes sense 
        /// within the logic of your app to ask the user for ratings and reviews within your app.
        /// </summary>
        public static void RequestReview()
        {
            var appName = Application.productName;
            var appIdentifier = Application.identifier;

            var title = string.Format("Rate {0}!", appName);
            var message = string.Format("If you enjoy using {0}, please take a moment to rate it.Thanks for your support!", appName);
            var noTitle = "No, thanks";
            var rateTitle = "Rate";

            if (Application.isEditor)
            {
                var builder = new UM_NativeDialogBuilder(title, message);
                builder.SetPositiveButton(rateTitle, () => { });
                builder.SetNegativeButton(noTitle, () => { });

                var dialog = builder.Build();
                dialog.Show();
            }

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    ISN_SKStoreReviewController.RequestReview();
                    break;
                case RuntimePlatform.Android:

                    var dialog = new AN_AlertDialog(AN_DialogTheme.Default);
                    dialog.Title = title;
                    dialog.Message = message;

                    dialog.SetNegativeButton(noTitle, () => { });

                    dialog.SetPositiveButton(rateTitle, () =>
                    {
                        //This code will take user to your app Play Market page
                        var uri = new System.Uri("market://details?id=" + appIdentifier);
                        var viewIntent = new AN_Intent(AN_Intent.ACTION_VIEW, uri);
                        AN_MainActivity.Instance.StartActivity(viewIntent);
                    });

                    dialog.Show();
                    break;
            }
        }
    }
}
