using UnityEngine;
using UnityEngine.UI;

public class UM_SavaedGameMetaView : MonoBehaviour
{
    [SerializeField]
    Text m_Title = null;
    [SerializeField]
    Button m_DeleteButton = null;
    [SerializeField]
    Button m_GetDataButton = null;

    public Button DeleteButton => m_DeleteButton;

    public Button GetDataButton => m_GetDataButton;

    public void SetTitle(string title)
    {
        m_Title.text = title;
    }
}
