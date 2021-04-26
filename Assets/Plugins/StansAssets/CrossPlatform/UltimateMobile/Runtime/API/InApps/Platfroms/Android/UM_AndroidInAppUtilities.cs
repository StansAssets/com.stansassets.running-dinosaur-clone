using SA.Android.Vending.BillingClient;

namespace SA.CrossPlatform.InApp
{
    /// <summary>
    /// Utilities collection for the Android platform.
    /// </summary>
    public class UM_AndroidInAppUtilities
    {
        /// <summary>
        /// Active Android billing client.
        /// </summary>
        public AN_BillingClient ActiveBillingClient
        {
            get
            {
                var client = UM_InAppService.Client;
                if (client is UM_AndroidInAppClient) return (client as UM_AndroidInAppClient).BillingClient;

                return null;
            }
        }
    }
}
