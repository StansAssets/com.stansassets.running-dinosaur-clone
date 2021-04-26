using System;
using System.Collections.Generic;
using SA.iOS.GameKit;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.GameServices
{
    class UM_EditorAchievementsClient : UM_AbstractAchievementsClient, UM_iAchievementsClient
    {
        public void ShowUI(Action<SA_Result> callback)
        {
            callback.Invoke(new SA_Result());
        }

        public void Load(Action<UM_AchievementsLoadResult> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                UM_AchievementsLoadResult loadResult;
                var achievements = new List<UM_iAchievement>();
                foreach (var achievement in ISN_GKAchievement.GetGameAchievements())
                {
                    if (UM_EditorAPIEmulator.HasKey(achievement.Identifier)) achievement.PercentComplete = UM_EditorAPIEmulator.GetFloat(achievement.Identifier);
                    var iosAchievement = new UM_IOSAchievement(achievement);
                    SetAchievementCahce(iosAchievement);
                    achievements.Add(iosAchievement);
                }

                loadResult = new UM_AchievementsLoadResult(achievements);
                callback.Invoke(loadResult);
            });
        }

        public void Reveal(string achievementId, Action<SA_Result> callback)
        {
            //Always revealed
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                callback.Invoke(new SA_Result());
            });
        }

        public void Unlock(string achievementId, Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                UM_EditorAPIEmulator.SetFloat(achievementId, 100f);
                callback.Invoke(new SA_Result());
            });
        }

        public void Increment(string achievementId, int numSteps, Action<SA_Result> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                var currentSteps = 0;
                if (UM_EditorAPIEmulator.HasKey(achievementId)) currentSteps = (int)UM_EditorAPIEmulator.GetFloat(achievementId);

                UM_EditorAPIEmulator.SetFloat(achievementId, currentSteps + numSteps);
                callback.Invoke(new SA_Result());
            });
        }
    }
}
