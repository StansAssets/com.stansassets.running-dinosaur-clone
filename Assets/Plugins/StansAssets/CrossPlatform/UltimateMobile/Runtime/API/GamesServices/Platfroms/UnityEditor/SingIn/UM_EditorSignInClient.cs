using System;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.GameServices
{
    class UM_EditorSignInClient : UM_AbstractSignInClient, UM_iSignInClient
    {
        protected override void StartSingInFlow(Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                var playerInfo = new UM_PlayerInfo(UM_PlayerState.SignedIn, UM_Settings.Instance.GSEditorPlayer);
                UpdateSignedPlayer(playerInfo);
                callback.Invoke(new SA_Result());
            });
        }

        public void SignOut(Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                var playerInfo = new UM_PlayerInfo(UM_PlayerState.SignedOut, null);
                UpdateSignedPlayer(playerInfo);
                callback.Invoke(new SA_Result());
            });
        }
    }
}
