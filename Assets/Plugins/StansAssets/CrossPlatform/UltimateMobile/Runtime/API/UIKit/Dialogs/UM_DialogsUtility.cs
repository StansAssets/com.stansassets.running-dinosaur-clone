using SA.Foundation.Templates;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Static class with the collection of dialogs helper methods.
    /// </summary>
    public static class UM_DialogsUtility
    {
        /// <summary>
        /// Creates new simple alert and immediately shows it.
        /// </summary>
        /// <param name="title">Alert title.</param>
        /// <param name="message">Alert message.</param>
        public static void ShowMessage(string title, string message)
        {
            var builder = new UM_NativeDialogBuilder(title, message);
            builder.SetPositiveButton("Okay", () => { });

            var dialog = builder.Build();
            dialog.Show();
        }

        /// <summary>
        /// Creates new simple alert and immediately shows it.
        /// The alter has predefined title set as `Note`.
        /// </summary>
        /// <param name="message">Notification message.</param>
        public static void ShowNotification(string message)
        {
            ShowMessage("Note", message);
        }

        public static void DisplayResultMessage(SA_Result result)
        {
            if (result.IsSucceeded)
                ShowMessage("Success", "Operation completed successfully!");
            else
                ShowMessage("Error", result.Error.FullMessage);
        }
    }
}
