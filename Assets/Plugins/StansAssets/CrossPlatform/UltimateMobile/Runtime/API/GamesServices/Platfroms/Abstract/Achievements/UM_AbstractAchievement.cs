using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    abstract class UM_AbstractAchievement : UM_iAchievement
    {
        [SerializeField]
        protected string m_identifier;
        [SerializeField]
        protected string m_name;
        [SerializeField]
        protected int m_currentSteps;
        [SerializeField]
        protected int m_totalSteps;

        [SerializeField]
        protected UM_AchievementType m_type;
        [SerializeField]
        protected UM_AchievementState m_state;

        public string Identifier => m_identifier;

        public string Name => m_name;

        public int CurrentSteps => m_currentSteps;

        public int TotalSteps => m_totalSteps;

        public UM_AchievementType Type => m_type;

        public UM_AchievementState State => m_state;

        public void SetCurrentSteps(int num)
        {
            m_currentSteps = num;
        }

        public void SetSate(UM_AchievementState state)
        {
            m_state = state;
        }
    }
}
