using StansAssets.ProjectSample.InApps;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.Dino.Game
{
    enum Distance
    {
        Far = 1,
        Closer = 2,
        TheClosest = 3
    }
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] GameObject m_Visuals;
        [SerializeField] int m_ScoreForFullCycle = 250, m_TargetMistake = 0;
        [SerializeField] bool m_DisabledAtDay, m_DisabledAtNight;
        [SerializeField] GameObject m_EndLevel;
        [SerializeField] GameObject m_PremiumVisuals;
        [SerializeField] Distance distance = Distance.Far;


        float m_MovementPerScorePoint;
        protected Image m_Image;
        Vector3 m_InitialPosition;

        bool Active { get; set; } = true;

        // protected float CycleProgress => (m_Bounds.xMax - m_Visuals.transform.position.x) / m_Bounds.width;

        void Start()
        {
            m_MovementPerScorePoint = (m_EndLevel.transform.position.x - transform.position.x) / m_ScoreForFullCycle;
            m_Image = m_Visuals.GetComponent<Image>();
            m_InitialPosition = m_Visuals.transform.position;

            var level = FindObjectOfType<DinoLevel>();
            level.OnReset += Reset;
            level.OnScoreGained += AddScore;

            FindObjectOfType<TimeOfDay>().OnDayTimeChange += HandleDayTimeChange;

            if (m_PremiumVisuals)
                m_PremiumVisuals.SetActive(RewardManager.HasPremium);

            HandleDayTimeChange(true);
        }

        protected virtual void HandleDayTimeChange(bool isDay)
        {
            Active = isDay ? !m_DisabledAtDay : !m_DisabledAtNight;
        }

        protected virtual void Reset()
        {
            m_Visuals.transform.position = m_InitialPosition;
            Active = !m_DisabledAtDay;
        }

        protected virtual void AddScore(float score)
        {
            if (!Active)
                return;

            int Targeting = Random.Range(-m_TargetMistake, m_TargetMistake);
            var position = m_Visuals.transform.position;
            var translation = new Vector3(m_MovementPerScorePoint * score * ((int)distance) *0.5f, 0);
            if (position.x + translation.x < m_EndLevel.transform.position.x)
            {
                translation = new Vector3(transform.position.x, 0);
                if(position.y+Targeting< m_EndLevel.transform.position.y)
                    translation += new Vector3(0, Targeting);
                else
                    translation += new Vector3(0, -Targeting);
            }    
            m_Visuals.transform.Translate(translation);

        }
    }
}

 