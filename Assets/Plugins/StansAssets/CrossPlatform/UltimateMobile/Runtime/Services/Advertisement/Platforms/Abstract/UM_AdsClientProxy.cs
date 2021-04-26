namespace SA.CrossPlatform.Advertisement
{
    /// <summary>
    /// This class has to be Internal.
    /// Sorry for the extra noise but we can't make it internal, since addons must be able to use it.
    /// And since addons can not used `asmdef`, because Google Ads or Unity Ads aren't using it, we can not mark
    /// this class as internal and only open visibility to the appropriate addons.
    /// </summary>
    public static class UM_AdsClientProxy
    {
        internal static UM_iAdsClient GoogleAdsClient { get; set; }
        internal static UM_iAdsClient UnityAdsClient { get; set; }

        public static void RegisterGoogleAdsClient(UM_iAdsClient client)
        {
            GoogleAdsClient = client;
        }

        public static void RegisterUnityAdsClient(UM_iAdsClient client)
        {
            UnityAdsClient = client;
        }
    }
}
