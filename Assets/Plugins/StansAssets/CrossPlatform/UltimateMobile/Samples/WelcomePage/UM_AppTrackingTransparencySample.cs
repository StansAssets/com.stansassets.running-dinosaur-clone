using SA.CrossPlatform.UI;
using SA.iOS.AppTrackingTransparency;
using UnityEngine;
using UnityEngine.UI;

public class UM_AppTrackingTransparencySample : MonoBehaviour
{
    [SerializeField]
    Button m_RequestTrackingAuthorizationButton  = default;
    
    [SerializeField]
    Button m_TrackingAuthorizationStatusButton  = default;

    void Start()
    {
        m_RequestTrackingAuthorizationButton.onClick.AddListener(() =>
        {
            ISN_ATTrackingManager.RequestTrackingAuthorization(status =>
            {
                Debug.Log($"New authorization status: {status}");
                UM_DialogsUtility.ShowNotification(status.ToString());
            });
        });
        
        m_TrackingAuthorizationStatusButton.onClick.AddListener(() =>
        {
            Debug.Log($"Current authorization status: {ISN_ATTrackingManager.TrackingAuthorizationStatus}");
            UM_DialogsUtility.ShowNotification(ISN_ATTrackingManager.TrackingAuthorizationStatus.ToString());
        });
        
    }
}
