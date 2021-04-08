using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] GameObject m_Visuals;
        [SerializeField] int m_ScoreForFullCycle = 250;
        [SerializeField] bool m_DisabledAtDay, m_DisabledAtNight;

        float m_FullCycleLength;
        float m_MovementPerScorePoint;
        float m_Counter;
        bool m_Active;
        Vector3 m_InitialPosition;
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

        protected float CycleProgress => m_Counter / m_ScoreForFullCycle;
        
        void Start ()
        {
            var rectWidth = m_Visuals.GetComponent<RectTransform> ().rect.width;
            m_FullCycleLength = Screen.width + 2 * rectWidth;
            m_MovementPerScorePoint = m_FullCycleLength / m_ScoreForFullCycle;
            m_Image = m_Visuals.GetComponent<Image> ();
            m_InitialPosition = transform.localPosition;
            
            var level = FindObjectOfType<DinoLevel> ();
            level.OnReset += Reset;
            level.OnScoreGained += AddScore;

            FindObjectOfType<TimeOfDay> ().OnDayTimeChange += HandleDayTimeChange;
            Active = !m_DisabledAtDay;
        }

        protected virtual void HandleDayTimeChange (bool value)
        {
            Active = value 
                         ? !m_DisabledAtDay 
                         : !m_DisabledAtNight;
        }

        protected virtual void Reset ()
        {
            transform.localPosition = m_InitialPosition;
        }

        protected virtual void AddScore (float score)
        {
            if (!Active) return;
            
            m_Counter += score;
            if (m_Counter > m_ScoreForFullCycle) {
                m_Counter -= m_ScoreForFullCycle;
                transform.Translate (new Vector3 (m_FullCycleLength - score * m_MovementPerScorePoint, 0));
            }
            else {
                transform.Translate (new Vector3 (-score * m_MovementPerScorePoint, 0));
            }
        }
    }
}
