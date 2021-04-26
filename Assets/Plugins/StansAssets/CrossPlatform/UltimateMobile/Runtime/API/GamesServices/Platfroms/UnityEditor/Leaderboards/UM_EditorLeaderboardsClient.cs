using UnityEngine;
using System;
using System.Collections.Generic;
using SA.Foundation.Templates;
using StansAssets.Foundation;

namespace SA.CrossPlatform.GameServices
{
    class UM_EditorLeaderboardsClient : UM_AbstractLeaderboardsClient, UM_iLeaderboardsClient
    {
        public void ShowUI(Action<SA_Result> callback)
        {
            callback.Invoke(new SA_Result());
        }

        public void ShowUI(string leaderboardId, Action<SA_Result> callback)
        {
            callback.Invoke(new SA_Result());
        }

        public void ShowUI(string leaderboardId, UM_LeaderboardTimeSpan timeSpan, Action<SA_Result> callback)
        {
            callback.Invoke(new SA_Result());
        }

        public void SubmitScore(string leaderboardId, long score, ulong context, Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                var umScore = new UM_Score(score, 10, context, TimeUtility.ToUnixTime(DateTime.Now));
                UM_EditorAPIEmulator.SetString(leaderboardId, JsonUtility.ToJson(umScore));
                callback.Invoke(new SA_Result());
            });
        }

        public void LoadLeaderboardsMetadata(Action<UM_LoadLeaderboardsMetaResult> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                var umLeaderboards = new List<UM_iLeaderboard>();
                foreach (var umLeaderboard in UM_Settings.Instance.GSLeaderboards) umLeaderboards.Add(umLeaderboard);
                var umResult = new UM_LoadLeaderboardsMetaResult(umLeaderboards);
                callback.Invoke(umResult);
            });
        }

        public void LoadCurrentPlayerScore(string leaderboardId, UM_LeaderboardTimeSpan span, UM_LeaderboardCollection collection, Action<UM_ScoreLoadResult> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                if (UM_EditorAPIEmulator.HasKey(leaderboardId))
                {
                    var json = UM_EditorAPIEmulator.GetString(leaderboardId);
                    var umScore = JsonUtility.FromJson<UM_Score>(json);
                    callback.Invoke(new UM_ScoreLoadResult(umScore));
                }
                else
                {
                    var error = new SA_Error(100, "Leaderboard with id: " + leaderboardId + " does not have any scores yet.");
                    callback.Invoke(new UM_ScoreLoadResult(error));
                }
            });
        }
    }
}
