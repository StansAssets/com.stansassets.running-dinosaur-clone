using SA.Foundation.Templates;

namespace SA.CrossPlatform.InApp
{
    /// <summary>
    /// A set of methods that process transactions, unlock purchased functionality, and continue promoted in-app purchases.
    /// </summary>
    public interface UM_iTransactionObserver
    {
        /// <summary>
        /// Tells an observer that transaction have been updated.
        /// </summary>
        /// <param name="transaction">Updated transaction.</param>
        void OnTransactionUpdated(UM_iTransaction transaction);

        /// <summary>
        /// Tells an observer that transactions restore flow has been updated.
        /// </summary>
        /// <param name="result">Restore flow result.</param>
        void OnRestoreTransactionsComplete(SA_Result result);
    }
}
