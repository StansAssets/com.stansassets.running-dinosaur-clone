using System;
using System.Collections.Generic;
using UnityEngine;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.GameServices
{
    /// <summary>
    /// Achievements Load Result object,
    /// </summary>
    [Serializable]
    public class UM_AchievementsLoadResult : SA_Result
    {
        [SerializeField]
        List<UM_iAchievement> m_achievements = new List<UM_iAchievement>();

        internal UM_AchievementsLoadResult(SA_Error error)
            : base(error) { }

        internal UM_AchievementsLoadResult(List<UM_iAchievement> achievements)
        {
            m_achievements = achievements;
        }

        /// <summary>
        /// Achievements list.
        /// </summary>
        public List<UM_iAchievement> Achievements => m_achievements;
    }
}
