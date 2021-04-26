namespace SA.CrossPlatform.UI
{
    class UM_EditorNativeDialogImpl : UM_iUIDialog
    {
#if UNITY_EDITOR
        readonly UM_NativeDialogBuilder m_Builder;
#endif

        public UM_EditorNativeDialogImpl(UM_NativeDialogBuilder builder)
        {
#if UNITY_EDITOR
            m_Builder = builder;
#endif
        }

        public void Show()
        {
#if UNITY_EDITOR

            UM_NativeDialogBuilder.Button noButton;
            if (m_Builder.NegativeButton != null)
                noButton = m_Builder.NegativeButton;
            else
                noButton = m_Builder.DestructiveButton;

            if (m_Builder.NeutralButton == null)
            {
                if (m_Builder.NegativeButton == null && m_Builder.DestructiveButton == null)
                {
                    UnityEditor.EditorUtility.DisplayDialog(m_Builder.Title, m_Builder.Message, m_Builder.PositiveButton.Title);
                    m_Builder.PositiveButton.ButtonAction.Invoke();
                }
                else
                {
                    var result = UnityEditor.EditorUtility.DisplayDialog(m_Builder.Title, m_Builder.Message, m_Builder.PositiveButton.Title, noButton.Title);
                    if (result)
                        m_Builder.PositiveButton.ButtonAction.Invoke();
                    else
                        noButton.ButtonAction.Invoke();
                }
            }
            else
            {
                var option = UnityEditor.EditorUtility.DisplayDialogComplex(m_Builder.Title, m_Builder.Message, m_Builder.PositiveButton.Title, noButton.Title, m_Builder.NeutralButton.Title);
                switch (option)
                {
                    case 0:
                        m_Builder.PositiveButton.ButtonAction.Invoke();
                        break;
                    case 1:
                        noButton.ButtonAction.Invoke();
                        break;
                    case 2:
                        m_Builder.NeutralButton.ButtonAction.Invoke();
                        break;
                }
            }
#endif
        }

        public void Hide()
        {
            //Don't supported by editor popups
        }
    }
}
