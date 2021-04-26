using System;
using SA.Foundation.Templates;
using SA.iOS.GameKit;

namespace SA.CrossPlatform.GameServices
{
    class UM_IOSSignInClient : UM_AbstractSignInClient, UM_iSignInClient
    {
        bool m_Subscribed;
        Action<SA_Result> m_SingInCallback;

        protected override void StartSingInFlow(Action<SA_Result> callback)
        {
            if (m_Subscribed)
                return;

            m_Subscribed = true;
            m_SingInCallback = callback;
            ISN_GKLocalPlayer.SetAuthenticateHandler(HandleAuthentication);
        }

        void HandleAuthentication(SA_Result result)
        {
            if (m_SingInCallback != null)
            {
                m_SingInCallback.Invoke(result);
                m_SingInCallback = null;
            }

            if (result.IsSucceeded)
                UpdatePlayerInfo(ISN_GKLocalPlayer.LocalPlayer);
            else
                UpdatePlayerInfo(null);
        }

        public void SignOut(Action<SA_Result> callback)
        {
            //We will jus do nothing for iOS
        }

        void UpdatePlayerInfo(ISN_GKLocalPlayer player)
        {
            UM_PlayerInfo playerInfo;
            if (player != null)
                playerInfo = new UM_PlayerInfo(UM_PlayerState.SignedIn, new UM_IOSPlayer(player));
            else
                playerInfo = new UM_PlayerInfo(UM_PlayerState.SignedOut, null);

            UpdateSignedPlayer(playerInfo);
        }
    }
}
