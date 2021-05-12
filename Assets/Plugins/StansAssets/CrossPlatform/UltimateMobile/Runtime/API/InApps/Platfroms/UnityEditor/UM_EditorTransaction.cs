using System;
using StansAssets.Foundation;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    class UM_EditorTransaction : UM_AbstractTransaction<UM_EditorTransaction>, UM_iTransaction
    {
        public UM_EditorTransaction(string productId, UM_TransactionState state)
        {
            m_id = IdFactory.RandomString;
            m_productId = productId;
            m_unixTimestamp = TimeUtility.ToUnixTime(DateTime.Now);
            m_state = state;
        }
    }
}
