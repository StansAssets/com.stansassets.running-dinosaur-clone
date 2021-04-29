using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class Moon : MovingObject
    {
        [SerializeField] Sprite[] m_MoonSprites;

        int m_Phase;

        public int Phase {
            get => m_Phase;
            set {
                m_Phase = value;
                m_Image.sprite = m_MoonSprites[m_Phase];
            }
        }

        protected override void Reset ()
        {
            base.Reset ();
            Phase = 0;
        }

        protected override void HandleDayTimeChange (bool isDay)
        {
            base.HandleDayTimeChange (isDay);
            if (!isDay) {
                Phase = Phase++ % m_MoonSprites.Length;
            }
        }
    }
}
