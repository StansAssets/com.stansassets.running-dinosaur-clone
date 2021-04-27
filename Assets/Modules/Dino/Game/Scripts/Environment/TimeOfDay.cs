using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class TimeOfDay : MonoBehaviour
    {
        public event Action<bool> OnDayTimeChange;
        
        [SerializeField] int dayTimeLength, nightTimeLength;
        
        float m_TimeOfDayLength, m_TimeOfDayRemaining;

        void ScoreGained (float score)
        {
            m_TimeOfDayRemaining -= score;
            if (m_TimeOfDayRemaining <= 0) {
                BeginTimeOfDay (!IsDay);
            }
        }

        private bool IsDay { get; set; }

        void Start ()
        {
            var level = FindObjectOfType<DinoLevel> ();
            level.OnScoreGained += ScoreGained;
            level.OnReset += () => {
                m_TimeOfDayRemaining = 0;
                BeginTimeOfDay (true);
            };
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
