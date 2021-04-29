using System;
using System.Collections;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    class TimeOfDay : MonoBehaviour
    {
        private const string k_ShaderBlendPropertyName = "_Blend";
        
        public event Action<bool> OnDayTimeChange;

        [SerializeField] int dayTimeLength, nightTimeLength;
        [SerializeField] Material m_Material;
        [SerializeField] float m_BlendDuration;

        float m_TimeOfDayRemaining;
        bool m_Day;
        float m_CurrentShaderBlend;

        private float Blend
        {
            get => m_CurrentShaderBlend;
            set {
                m_Material.SetFloat(k_ShaderBlendPropertyName, value);
                m_CurrentShaderBlend = value;
            }
        }

        public void ScoreGained(float score)
        {
            m_TimeOfDayRemaining -= score;
            if (m_TimeOfDayRemaining <= 0)
            {
                m_Day = !m_Day;
                m_TimeOfDayRemaining += m_Day ? dayTimeLength : nightTimeLength;
                OnDayTimeChange?.Invoke(m_Day);
                StartCoroutine(BlendCoroutine());
            }
        }

        public void Reset()
        {
            m_TimeOfDayRemaining = dayTimeLength;
            m_TimeOfDayRemaining = dayTimeLength;
            m_Day = true;
            OnDayTimeChange?.Invoke(m_Day);
            StartCoroutine(BlendCoroutine());
        }

        IEnumerator BlendCoroutine()
        {
            float targetBlend = m_Day ? 0f : 1f;
            if (Math.Abs(m_CurrentShaderBlend - targetBlend) < 0.01f) yield break;

            float elapsedTime = 0f;
            float blendingSpeed = (targetBlend - Blend) / m_BlendDuration;
            while (elapsedTime < m_BlendDuration)
            {
                elapsedTime += Time.deltaTime;
                Blend += blendingSpeed * Time.deltaTime;
                yield return null;
            }  
            
            Blend = targetBlend;
            yield return null;
        }

        private void OnDestroy()
        {
            Blend = 0;
        }
    }
}
