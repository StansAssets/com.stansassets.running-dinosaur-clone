using StansAssets.ProjectSample.InApps;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class MovingObject : ScreenSizeDependent
    {
        [SerializeField] GameObject m_Visuals;
        [SerializeField] int m_ScoreForFullCycle = 250;
        [SerializeField] bool m_DisabledAtDay, m_DisabledAtNight;
        [SerializeField] Rect m_Bounds;
        [SerializeField] GameObject m_PremiumVisuals;

        float m_MovementPerScorePoint;
        protected Image m_Image;
        Vector3 m_InitialPosition;

        bool Active { get; set; } = true;

        protected float CycleProgress => (m_Bounds.xMax - m_Visuals.transform.position.x) / m_Bounds.width;

        void Start ()
        {
            m_MovementPerScorePoint = -m_Bounds.width / m_ScoreForFullCycle;
            m_Image = m_Visuals.GetComponent<Image> ();
            m_InitialPosition = transform.localPosition;

            var level = FindObjectOfType<DinoLevel> ();
            level.OnReset += Reset;
            level.OnScoreGained += AddScore;

            FindObjectOfType<TimeOfDay> ().OnDayTimeChange += HandleDayTimeChange;
            
            if (m_PremiumVisuals)
                m_PremiumVisuals.SetActive(RewardManager.HasPremium);
            
            HandleDayTimeChange(true);
        }

        protected virtual void HandleDayTimeChange (bool isDay)
        {
            Active = isDay ? !m_DisabledAtDay : !m_DisabledAtNight;
        }

        protected virtual void Reset ()
        {
            m_Visuals.transform.localPosition = m_InitialPosition;
            Active = !m_DisabledAtDay;
        }

        protected virtual void AddScore (float score)
        {
            if (!Active)
                return;

            var position = m_Visuals.transform.position;
            var translation = new Vector3 (m_MovementPerScorePoint * score, 0);
            if (position.x + translation.x < m_Bounds.xMin)
                translation += new Vector3(m_Bounds.width, Random.Range (m_Bounds.yMin, m_Bounds.yMax) - transform.localPosition.y);
            m_Visuals.transform.Translate (translation);
        }

        public override void UpdateScreenSize(Vector2 fromSize, Vector2 toSize)
        {
            m_Bounds.width += toSize.x - fromSize.x;
        }
    }
}

