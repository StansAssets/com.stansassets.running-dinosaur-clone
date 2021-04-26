using System;

namespace SA.CrossPlatform.UI
{
    /// <summary>
    /// Use to construct the native dialog UI.
    /// </summary>
    public class UM_NativeDialogBuilder
    {
        /// <summary>
        /// Native dialog button model.
        /// </summary>
        public class Button
        {
            /// <summary>
            /// Button Title.
            /// </summary>
            public readonly string Title;

            /// <summary>
            /// Button action callback.
            /// </summary>
            public readonly Action ButtonAction;

            /// <summary>
            /// Creates a button for the dialog.
            /// </summary>
            /// <param name="title"></param>
            /// <param name="buttonAction"></param>
            public Button(string title, Action buttonAction)
            {
                Title = title;
                ButtonAction = buttonAction;
            }
        }

        string m_Title;
        string m_Message;

        Button m_NeutralButton;
        Button m_PositiveButton;
        Button m_NegativeButton;
        Button m_DestructiveButton;

        /// <summary>
        /// Create new native dialog builder instance.
        /// </summary>
        /// <param name="title">Alert Title.</param>
        /// <param name="message">Alert Message</param>
        public UM_NativeDialogBuilder(string title, string message)
        {
            m_Title = title;
            m_Message = message;
        }

        /// <summary>
        /// Set alert Title.
        /// </summary>
        /// <param name="title">New alert title.</param>
        public void SetTitle(string title)
        {
            m_Title = title;
        }

        /// <summary>
        /// Set alert Message.
        /// </summary>
        /// <param name="message">New alert message.</param>
        public void SetMessage(string message)
        {
            m_Message = message;
        }

        /// <summary>
        /// Alert Title.
        /// </summary>
        public string Title => m_Title;

        /// <summary>
        /// Alert Message.
        /// </summary>
        public string Message => m_Message;

        /// <summary>
        /// Gets the neutral button.
        /// </summary>
        public Button NeutralButton => m_NeutralButton;

        /// <summary>
        /// Gets the positive button.
        /// </summary>
        public Button PositiveButton => m_PositiveButton;

        /// <summary>
        /// Gets the negative button.
        /// </summary>
        public Button NegativeButton => m_NegativeButton;

        /// <summary>
        /// Gets the destructive button.
        /// </summary>
        public Button DestructiveButton => m_DestructiveButton;

        /// <summary>
        /// Set button with default style.
        /// </summary>
        /// <param name="text">button text</param>
        /// <param name="callback">click listener</param>
        public void SetNeutralButton(string text, Action callback)
        {
            m_NeutralButton = new Button(text, callback);
        }

        /// <summary>
        /// Set button with positive style.
        /// </summary>
        /// <param name="text">button text</param>
        /// <param name="callback">click listener</param>
        public void SetPositiveButton(string text, Action callback)
        {
            m_PositiveButton = new Button(text, callback);
        }

        /// <summary>
        /// Set button with negative style,
        /// that indicates the action cancels the operation and leaves things unchanged.
        /// </summary>
        /// <param name="text">button text</param>
        /// <param name="callback">click listener</param>
        public void SetNegativeButton(string text, Action callback)
        {
            m_NegativeButton = new Button(text, callback);
        }

        /// <summary>
        /// Set button with destructive style,
        /// that indicates the action might change or delete data.
        /// </summary>
        /// <param name="text">button text</param>
        /// <param name="callback">click listener</param>
        public void SetDestructiveButton(string text, Action callback)
        {
            m_DestructiveButton = new Button(text, callback);
        }

        /// <summary>
        /// Build the dialog based on a builder properties
        /// </summary>
        /// <returns></returns>
        public UM_iUIDialog Build()
        {
            return UM_DialogsFactory.CreateDialog(this);
        }
    }
}
