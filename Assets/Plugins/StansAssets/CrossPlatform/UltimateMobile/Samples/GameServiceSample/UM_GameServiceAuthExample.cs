using UnityEngine;
using UnityEngine.UI;
using SA.CrossPlatform.GameServices;

public class UM_GameServiceAuthExample : MonoBehaviour
{
#pragma warning disable 649
    [Header("User Info")]
    [SerializeField]
    Text m_userId;
    [SerializeField]
    Text m_userName;
    [SerializeField]
    Text m_userAlias;
    [SerializeField]
    RawImage m_userAvatar;
    [SerializeField]
    GameObject m_userAvatarLoader;
    [Header("Buttons")]
    [SerializeField]
    Button m_connect;

#pragma warning restore 649

    void Awake()
    {
        UpdatePlayerUI();
        var client = UM_GameService.SignInClient;
        client.OnPlayerUpdated.AddListener(UpdatePlayerUI);
        m_connect.onClick.AddListener(() =>
        {
            if (client.PlayerInfo.State == UM_PlayerState.SignedIn)
                client.SignOut(result =>
                {
                    if (result.IsSucceeded)
                        Debug.Log("Signed Out");
                    else
                        Debug.LogError($"Failed to sign out: {result.Error.FullMessage}");
                });
            else
                client.SignIn(result =>
                {
                    if (result.IsSucceeded)
                        Debug.Log("Player is signed");
                    else
                        Debug.LogError($"Failed to sign in: {result.Error.FullMessage}");
                });
        });
    }

    void UpdatePlayerUI()
    {
        var client = UM_GameService.SignInClient;
        if (client.PlayerInfo.State == UM_PlayerState.SignedIn)
        {
            var player = client.PlayerInfo.Player;
            m_userId.text = player.Id;
            m_userAlias.text = player.Alias;
            m_userName.text = player.DisplayName;
            m_userAvatarLoader.gameObject.SetActive(true);
            player.GetAvatar(texure =>
            {
                //We need to make sure here that player is still singed 
                //by the time we are getting image load callback
                if (client.PlayerInfo.State == UM_PlayerState.SignedIn)
                {
                    if (texure == null)
                    {
                        //You mau want to assing some default texture here
                    }

                    //Assign image to RawImage object
                    m_userAvatar.texture = texure;
                }

                m_userAvatarLoader.gameObject.SetActive(false);
            });
            m_connect.GetComponentInChildren<Text>().text = "Sign Out";
        }
        else
        {
            m_connect.GetComponentInChildren<Text>().text = "Sign In";

            //Display User info
            m_userName.text = "Signed out";
            m_userAlias.text = string.Empty;
            m_userId.text = string.Empty;
            m_userAvatar.texture = null;
            m_userAvatarLoader.gameObject.SetActive(false);
        }
    }
}
