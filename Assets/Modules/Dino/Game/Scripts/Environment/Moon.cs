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

        protected override void AddScore (float score)
        {
            base.AddScore (score);
            if (CycleProgress > 0) {
                m_Image.color = new Color (
                                           0.75f,
                                           0.75f,
                                           0.75f,
                                           GetMoonTransparency (CycleProgress));
            }
        }

        // a = 1 in the middle of a night time, a = 0 at the beginning and at the end of the night time
        float GetMoonTransparency (float nightTimePassed) => 1f - Mathf.Pow ((nightTimePassed - 0.5f) * 2, 2);
    }
}
