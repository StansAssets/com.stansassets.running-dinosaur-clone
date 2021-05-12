using System;
using SA.Foundation.Editor;

namespace SA.CrossPlatform.Editor
{
    abstract class UM_NativeServiceLayoutBasedSetting : UM_NativeServiceSettings
    {
        protected abstract SA_ServiceLayout Layout { get; }
        public override string ServiceName => Layout.Title;
        public override Type ServiceUIType => Layout.GetType();

        public override bool IsEnabled => false;
    }
}
