using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class TimeOfDay : MonoBehaviour
    {
        public event Action<bool> OnDayTimeChange;
        
        [SerializeField] int dayTimeLength, nightTimeLength;

        bool m_IsDay;
        float m_TimeOfDayLength, m_TimeOfDayRemaining;

        void ScoreGained (float score)
        {
            m_TimeOfDayRemaining -= score;
            if (m_TimeOfDayRemaining <= 0) {
                BeginTimeOfDay (!m_IsDay);
            }
        }

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
            m_IsDay = isDay;
            m_TimeOfDayLength = isDay ? dayTimeLength : nightTimeLength;
            m_TimeOfDayRemaining += m_TimeOfDayLength;
            OnDayTimeChange?.Invoke (m_IsDay);
        }

        /*void SetColors ()
        {
            m_ColorCurves.redChannel.MoveKey(0, new Keyframe(0, currentPhase));
            m_ColorCurves.redChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
            m_ColorCurves.greenChannel.MoveKey(0, new Keyframe(0, currentPhase));
            m_ColorCurves.greenChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
            m_ColorCurves.blueChannel.MoveKey(0, new Keyframe(0, currentPhase));
            m_ColorCurves.blueChannel.MoveKey(1, new Keyframe(1, 1 - currentPhase));
        }*/
    }
}
