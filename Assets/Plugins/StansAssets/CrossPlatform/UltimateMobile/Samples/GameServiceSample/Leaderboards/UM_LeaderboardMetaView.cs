using UnityEngine;
using UnityEngine.UI;

namespace SA.CrossPlatform.Samples
{
    public class UM_LeaderboardMetaView : MonoBehaviour
    {
        [SerializeField]
        RawImage m_Icon;
        [SerializeField]
        Text m_Title = null;

        public void SetTitle(string title)
        {
            m_Title.text = title;
        }
    }
}
