using System;
using System.Collections;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class MobilePreloader : MonoBehaviour, IScenePreloader, ISceneManager
    {
        [SerializeField]
        Image m_FadeImage = null;

        [SerializeField]
        GameObject m_PreloaderRoot = null;

        public void FadeIn(Action onComplete)
        {
            m_PreloaderRoot.SetActive(true);
            StartCoroutine(FadeImage(false, onComplete));
        }

        public void FadeOut(Action onComplete)
        {
            StartCoroutine(FadeImage(true, () =>
            {
                onComplete.Invoke();
                m_PreloaderRoot.SetActive(false);
            }));
        }

        public void OnProgress(float progress)
        {
            //throw new NotImplementedException();
        }

        IEnumerator FadeImage(bool fadeAway, Action onComplete)
        {
            if (fadeAway)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    var color = m_FadeImage.color;
                    color = new Color( color.r,  color.g,  color.b, i);
                    m_FadeImage.color = color;
                    yield return null;
                }
            }
            else
            {
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    var color = m_FadeImage.color;
                    color = new Color(color.r, color.g, color.b, i);
                    m_FadeImage.color = color;
                    yield return null;
                }
            }

            onComplete.Invoke();
        }
    }
}
