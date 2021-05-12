using UnityEngine;
using UnityEngine.UI;
using SA.CrossPlatform.GameServices;
using SA.CrossPlatform.UI;

namespace SA.CrossPlatform.Samples
{
    public class UM_GameServiceLeaderboardsExample : MonoBehaviour
    {
        [SerializeField]
        Button m_NativeUIButton = null;
        [SerializeField]
        Button m_LoadButton = null;
        [SerializeField]
        UM_LeaderboardMetaView m_LeaderboardMetaView = null;

        void Start()
        {
            m_LeaderboardMetaView.gameObject.SetActive(false);
            m_LoadButton.onClick.AddListener(LoadMeta);
            m_NativeUIButton.onClick.AddListener(() =>
            {
                var client = UM_GameService.LeaderboardsClient;
                client.ShowUI(result =>
                {
                    if (result.IsSucceeded)
                        Debug.Log("Operation completed successfully!");
                    else
                        Debug.LogError($"Failed to show leaderboards UI {result.Error.FullMessage}");
                });
            });
        }

        void LoadMeta()
        {
            var client = UM_GameService.LeaderboardsClient;
            client.LoadLeaderboardsMetadata(result =>
            {
                if (result.IsSucceeded)
                    foreach (var leaderboard in result.Leaderboards)
                    {
                        var view = Instantiate(m_LeaderboardMetaView.gameObject, m_LeaderboardMetaView.transform.parent);
                        view.SetActive(true);
                        view.transform.localScale = Vector3.one;
                        var meta = view.GetComponent<UM_LeaderboardMetaView>();
                        meta.SetTitle(leaderboard.Title);
                        Debug.Log($"leaderboard.Identifier: {leaderboard.Identifier}");
                        Debug.Log($"leaderboard.Title: {leaderboard.Title}");
                    }
                else
                    Debug.LogError($"Failed to load leaderboards meta {result.Error.FullMessage}");
            });
        }

        void LoadPlayerScore()
        {
            var client = UM_GameService.LeaderboardsClient;

            //The identifier for the leaderboard.
            var leaderboardId = "YOUR_LEADERBOARD_ID_HERE";

            //The period of time to which a player’s best score is restricted.
            var span = UM_LeaderboardTimeSpan.AllTime;

            //The scope of players to be searched for scores.
            var collection = UM_LeaderboardCollection.Public;
            client.LoadCurrentPlayerScore(leaderboardId, span, collection, (result) =>
            {
                if (result.IsSucceeded)
                {
                    var score = result.Score;
                    Debug.Log($"score.Value: {score.Value}");
                    Debug.Log($"score.Rank: {score.Rank}");
                    Debug.Log($"score.Context: {score.Context}");
                    Debug.Log($"score.Date: {score.Date}");
                }
                else
                {
                    Debug.Log($"Failed to load player score {result.Error.FullMessage}");
                }
            });
        }
    }
}
