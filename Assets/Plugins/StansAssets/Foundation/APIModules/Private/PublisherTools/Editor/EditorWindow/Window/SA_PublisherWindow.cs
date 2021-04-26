using SA.Foundation.Editor;

namespace SA.Foundation.Publisher
{
    public class SA_PublisherWindow : SA_PluginSettingsWindow<SA_PublisherWindow>
    {
        protected override void OnAwake()
        {
            SetHeaderTitle("Publisher Tools");
            SetHeaderDescription("Internals tools set for an asset publishing.");
            SetHeaderVersion("1.0");

            AddMenuItem("EXPORT", CreateInstance<SA_PackagesExportTab>());
        }
    }
}
