namespace SA.CrossPlatform.InApp
{
    /// <summary>
    /// Possible transaction state options.
    /// </summary>
    public enum UM_TransactionState
    {
        /// <summary>
        /// Unknown Transaction state.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Transaction finished shamefully.
        /// </summary>
        Purchased,

        /// <summary>
        /// Transaction was resorted.
        /// </summary>
        Restored,

        /// <summary>
        /// Transaction Failed.
        /// </summary>
        Failed,

        /// <summary>
        /// (Android) A Transaction is pending and the player will be informed when it's done.
        ///  For more information on how to handle pending transactions see
        ///  https://developer.android.com/google/play/billing/billing_library_overview.
        ///  https://developer.android.com/google/play/billing/billing_library_overview#pending
        ///
        /// (iOS) A transaction that is in the queue, but its final status is pending external action such as Ask to Buy.
        /// </summary>
        Pending,
    }
}
