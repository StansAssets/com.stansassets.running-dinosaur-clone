using SA.Android.GMS.CodeGen;
using SA.CrossPlatform.GameServices;
using UnityEngine;

namespace StansAssets.Dino.GameServices
{
    class GameServices : IGameServices
    {
        private UM_iSignInClient m_signInClient;
        private UM_iLeaderboardsClient m_leaderboardsClient;

        public UM_iPlayer CurrentPlayer { get; private set; }
        public void Init() {
            m_signInClient = UM_GameService.SignInClient;
            m_leaderboardsClient = UM_GameService.LeaderboardsClient;


            m_signInClient.OnPlayerUpdated.AddListener(PrintPlayerInfo);
            m_signInClient.SignIn(result => {
                if (result.IsSucceeded) {
                    Debug.Log("Player is signed");
                }
                else {
                    Debug.LogError($"Failed to sign in: {result.Error.FullMessage}");
                }
            });
        }

        public void SubmitScore(long score) {
            var leaderboardId = AN_GamesIds.Leaderboards.MaxPoints;
            m_leaderboardsClient.SubmitScore(leaderboardId, score, 0, (result) => {
                if(result.IsSucceeded) {
                    Debug.Log("Score submitted successfully");
                } else {
                    Debug.LogError($"Failed to submit score: {result.Error.FullMessage}");
                }
            });
        }

        public void ShowLeaderboards() {
            m_leaderboardsClient.ShowUI(result => {
                if (result.IsSucceeded) {
                    Debug.Log("Operation completed successfully!");
                }
                else {
                    Debug.LogError($"Failed to show leaderboards UI {result.Error.FullMessage}");
                }
            });
        }



        private void PrintPlayerInfo() {
            var playerInfo = UM_GameService.SignInClient.PlayerInfo;
            Debug.Log($"playerInfo state: {playerInfo.State}");
            if (playerInfo.State == UM_PlayerState.SignedIn) {
                var player = playerInfo.Player;
                Debug.Log($"player id: {player.Id}");
                Debug.Log($"player alias: {player.Alias}");
                Debug.Log($"player displayName: {player.DisplayName}");
                CurrentPlayer = player;
            }
        }
    }
}
