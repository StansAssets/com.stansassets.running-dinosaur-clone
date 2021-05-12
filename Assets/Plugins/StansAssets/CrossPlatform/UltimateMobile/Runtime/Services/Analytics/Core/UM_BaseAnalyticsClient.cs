namespace SA.CrossPlatform.Analytics
{
    abstract class UM_BaseAnalyticsClient
    {
        public virtual void Init() { }
        public virtual bool IsInitialized { get; }
    }
}
