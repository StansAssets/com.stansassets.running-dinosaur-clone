using System;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class BoxesMainMenuController : MonoBehaviour, IMainMenuController
    {
        [SerializeField]
        Button m_PlayButton = null;

        [SerializeField]
        Button m_SettingsButton = null;

        [SerializeField]
        GameObject m_MainMenu = null;

        public event Action OnGameRequest;
        public event Action OnSettingsRequest;

        void Awake()
        {
            m_PlayButton.onClick.AddListener(() =>
            {
                OnGameRequest?.Invoke();
            });

            m_SettingsButton.onClick.AddListener(() =>
            {
                OnSettingsRequest?.Invoke();
            });
        }

        public void Active()
        {
            m_MainMenu.SetActive(true);
        }

        public void Deactivate()
        {
            m_MainMenu.SetActive(false);
        }
    }
}
