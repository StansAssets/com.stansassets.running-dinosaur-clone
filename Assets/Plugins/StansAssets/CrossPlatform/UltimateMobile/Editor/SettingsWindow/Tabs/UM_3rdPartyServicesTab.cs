using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    class UM_3rdPartyServicesTab : SA_ServicesTab
    {
        protected override void OnCreateServices()
        {
            RegisterService(CreateInstance<UM_Facebook>());
            RegisterService(CreateInstance<UM_AdvertisementUI>());
            RegisterService(CreateInstance<UM_AnalyticsUI>());
            RegisterService(CreateInstance<UM_PlaymakerUI>());
            RegisterService(CreateInstance<UM_GifUI>());
        }
    }
}
