using SA.iOS.UIKit;

namespace SA.CrossPlatform.UI
{
    class UM_IOSNativeDialogImpl : UM_iUIDialog
    {
        readonly ISN_UIAlertController m_Dialog;

        public UM_IOSNativeDialogImpl(UM_NativeDialogBuilder builder)
        {
            m_Dialog = new ISN_UIAlertController(builder.Title, builder.Message, ISN_UIAlertControllerStyle.Alert);

            if (builder.PositiveButton != null)
                m_Dialog.AddAction(new ISN_UIAlertAction(builder.PositiveButton.Title, ISN_UIAlertActionStyle.Default, builder.PositiveButton.ButtonAction));

            if (builder.NeutralButton != null)
                m_Dialog.AddAction(new ISN_UIAlertAction(builder.NeutralButton.Title, ISN_UIAlertActionStyle.Default, builder.NeutralButton.ButtonAction));

            if (builder.NegativeButton != null)
                m_Dialog.AddAction(new ISN_UIAlertAction(builder.NegativeButton.Title, ISN_UIAlertActionStyle.Cancel, builder.NegativeButton.ButtonAction));

            if (builder.DestructiveButton != null)
                m_Dialog.AddAction(new ISN_UIAlertAction(builder.DestructiveButton.Title, ISN_UIAlertActionStyle.Destructive, builder.DestructiveButton.ButtonAction));
        }

        public void Show()
        {
            m_Dialog.Present();
        }

        public void Hide()
        {
            m_Dialog.Dismiss();
        }
    }
}
