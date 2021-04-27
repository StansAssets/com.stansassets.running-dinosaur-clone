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
                IsDay = false;
            }
        }

        bool IsDay
        {
            get => m_Day;
            set
            {
                if (m_Day == value) return;
                
                m_Day = value;
                m_TimeOfDayLength = m_Day ? dayTimeLength : nightTimeLength;
                m_TimeOfDayRemaining += m_TimeOfDayLength;
                OnDayTimeChange?.Invoke (m_Day);
            }
        }

        public void Reset()
        {
            m_TimeOfDayRemaining = 0;
            IsDay = true;
        }

        void BeginTimeOfDay (bool isDay)
        {
            IsDay = isDay;
            m_TimeOfDayLength = isDay ? dayTimeLength : nightTimeLength;
            m_TimeOfDayRemaining += m_TimeOfDayLength;
            OnDayTimeChange?.Invoke (IsDay);
        }
    }
}
