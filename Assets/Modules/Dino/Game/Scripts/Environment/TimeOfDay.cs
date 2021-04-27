using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    class TimeOfDay : MonoBehaviour
    {
        public event Action<bool> OnDayTimeChange;
        
        [SerializeField] int dayTimeLength, nightTimeLength;
        
        float m_TimeOfDayLength, m_TimeOfDayRemaining;
        bool m_Day;

        public void ScoreGained (float score)
        {
            m_TimeOfDayRemaining -= score;
            if (m_TimeOfDayRemaining <= 0) {
                m_Day = !m_Day;
                m_TimeOfDayLength = m_Day ? dayTimeLength : nightTimeLength;
                m_TimeOfDayRemaining += m_TimeOfDayLength;
                OnDayTimeChange?.Invoke (m_Day);
            }
        }

        public void Reset()
        {
            m_TimeOfDayRemaining = dayTimeLength;
            m_TimeOfDayRemaining = dayTimeLength;
            m_Day = true;
            OnDayTimeChange?.Invoke (m_Day);
        }
    }
}
