using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    [RequireComponent (typeof(RectTransform)), RequireComponent (typeof(Image))]
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] int m_ScoreForFullCycle = 250;
        [SerializeField] bool m_DisabledAtDay, m_DisabledAtNight;

        float m_FullCycleLength;
        float m_MovementPerScorePoint;
        float m_Counter;
        bool m_Active;
        protected Image m_Image;
        Vector3 m_InitialPosition;
        
        bool Active { 
            get => m_Active;
            set {
                if (m_Active != value) {
                    m_Active = value;
                    m_Image.enabled = value;
                }
            } 
        }

        protected float CycleProgress => m_Counter / m_ScoreForFullCycle;
        
        void Start ()
        {
            var rectWidth = GetComponent<RectTransform> ().rect.width;
            m_FullCycleLength = Screen.width + 2 * rectWidth;
            m_MovementPerScorePoint = m_FullCycleLength / m_ScoreForFullCycle;
            m_Image = GetComponent<Image> ();
            m_InitialPosition = transform.localPosition;
            
            var level = FindObjectOfType<DinoLevel> ();
            level.OnReset += Reset;
            level.OnScoreGained += AddScore;

            FindObjectOfType<TimeOfDay> ().OnDayTimeChange += HandleDayTimeChange;
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
