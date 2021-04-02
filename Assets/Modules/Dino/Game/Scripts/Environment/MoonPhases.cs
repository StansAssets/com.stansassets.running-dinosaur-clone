using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class MoonPhases : MonoBehaviour
    {
        [SerializeField] Image m_MoonImage;
        [SerializeField] Sprite[] m_MoonSprites;

        int m_Phase;
        bool m_Visible;

        public bool Visible {
            get => m_Visible;
            set {
                if (m_Visible != value) {
                    m_Visible = value;
                    m_MoonImage.gameObject.SetActive (value);
                }
            }
        }

        public int Phase {
            get => m_Phase;
            set {
                m_Phase = value;
                m_MoonImage.sprite = m_MoonSprites[m_Phase];
            }
        }

        public void Reset ()
        {
            m_Phase = 0;
            Visible = false;
        }

        public void DayFinished ()
        {
            Visible = false;
            Phase = Phase++ % m_MoonSprites.Length;
        }

        public void UpdateNightTimeProgress (float nightTimePassed)
        {
            if (nightTimePassed > 0) {
                m_MoonImage.color = new Color (
                                               0.7333333333f,
                                               0.7333333333f,
                                               0.7333333333f,
                                               GetMoonTransparency (nightTimePassed));
            }
        }

        // a = 1 in the middle of a night time, a = 0 at the beginning and at the end of the night time
        float GetMoonTransparency (float nightTimePassed) => 1f - Mathf.Pow ((nightTimePassed - 0.5f) * 2, 2);
    }
}
