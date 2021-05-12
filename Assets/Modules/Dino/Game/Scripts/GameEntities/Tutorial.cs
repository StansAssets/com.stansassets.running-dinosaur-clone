using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Dino.Game
{
    class Tutorial : MonoBehaviour
    {
        [SerializeField] float m_DelayBeforeDisabling;
        [SerializeField] List<Image> m_Images;
        [SerializeField] List<Text> m_Texts;
        
        public void Show()
        {
            StartCoroutine(SetActiveCoroutine(true));
        }

        public void Hide()
        {
            StartCoroutine(SetActiveCoroutine(false));
        }

        IEnumerator SetActiveCoroutine(bool active)
        {
            float autoDisableTime = Time.time + m_DelayBeforeDisabling;
            foreach (var img in m_Images) 
                img.color = GetColor(img.color, active);
            foreach (var txt in m_Texts)
                txt.color = GetColor(txt.color, active);
            
            if (active) {
                while (Time.time < autoDisableTime)
                    yield return null;
                Hide();
            }
        }

        static Color GetColor(Color baseColor, bool active)
        {
            return new Color(
                             baseColor.r,
                             baseColor.g,
                             baseColor.b,
                             active ? 1 : 0);
        }
    }
}
