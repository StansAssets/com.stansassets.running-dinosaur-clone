using SA.CrossPlatform.App;
using SA.CrossPlatform.UI;
using UnityEngine;
using UnityEngine.UI;

public class UM_BuildInfoControllerSample : MonoBehaviour
{
    [SerializeField]
    Button m_IsDarkModeButton = default;

    // Start is called before the first frame update
    void Start()
    {
        m_IsDarkModeButton.onClick.AddListener(() =>
        {
            UM_DialogsUtility.ShowNotification(UM_Application.IsDarkMode 
                ? "Night mode is active, we're using dark theme" 
                : "Night mode is not active, we're using the light theme");
        });
    }
}
