namespace SA.CrossPlatform.Analytics
{
    /// <summary>
    /// An extended <see cref="UM_IAnalyticsClient"/> client.
    /// </summary>
    public interface UM_IMasterAnalyticsClient : UM_IAnalyticsClient
    {
        /// <summary>
        /// Allows to register more analytics clients inside the master client.
        /// </summary>
        /// <param name="client"></param>
        void RegisterClient(UM_IAnalyticsClient client);
    }
}
