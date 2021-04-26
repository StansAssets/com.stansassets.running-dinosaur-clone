using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.Advertisement
{
    class UM_EditorBaseAds
    {
        protected bool m_IsReady = false;

        public void Load(Action<SA_Result> callback)
        {
            Load("editor_ads_id", callback);
        }

        public void Load(string id, Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                m_IsReady = true;
                callback.Invoke(new SA_Result());
            });
        }

        public virtual bool IsReady => m_IsReady;
    }
}
