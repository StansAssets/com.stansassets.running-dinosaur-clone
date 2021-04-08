using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] GameObject m_Visuals;
        [SerializeField] int m_ScoreForFullCycle = 250;
        [SerializeField] bool m_DisabledAtDay, m_DisabledAtNight;
        [SerializeField] Rect m_Bounds;

        float m_MovementPerScorePoint;
        bool m_Active;
        protected Image m_Image;

        bool Active {
            get => m_Active;
            set {
                if (m_Active != value) {
                    m_Active = value;
                    m_Visuals.SetActive (value);
                }
            }
        }

        public Rect Bounds {
            get => m_Bounds;
            set => m_Bounds = value;
        }

        protected float CycleProgress => (m_Bounds.xMax - m_Visuals.transform.position.x) / m_Bounds.width;

        void Start ()
        {
            m_MovementPerScorePoint = -m_Bounds.width / m_ScoreForFullCycle;
            m_Image = m_Visuals.GetComponent<Image> ();

            var level = FindObjectOfType<DinoLevel> ();
            level.OnReset += Reset;
            level.OnScoreGained += AddScore;

            FindObjectOfType<TimeOfDay> ().OnDayTimeChange += HandleDayTimeChange;
        }

        protected virtual void HandleDayTimeChange (bool isDay)
        {
            Active = isDay ? !m_DisabledAtDay : !m_DisabledAtNight;
        }

        protected virtual void Reset ()
        {
            m_Visuals.transform.localPosition = Vector3.zero;
            Active = !m_DisabledAtDay;
        }

        protected virtual void AddScore (float score)
        {
            if (!Active)
                return;

            var position = m_Visuals.transform.position;
            var translation = new Vector3 (m_MovementPerScorePoint * score, 0);
            if (position.x + translation.x < m_Bounds.xMin)
                translation += GetRespawnTranslation (position);
            m_Visuals.transform.Translate (translation);
        }

        Vector3 GetRespawnTranslation (Vector3 position)
        {
            // get random Y for a respawn position
            var newY = Random.Range (m_Bounds.yMin, m_Bounds.yMax);
            return new Vector3 (m_Bounds.width, newY - position.y);
        }
    }
}

