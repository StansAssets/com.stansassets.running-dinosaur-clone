using System;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class MainMenuController : MonoBehaviour, IMainMenuController
    {
        [SerializeField]
        Button m_PlayButton = null;

        [SerializeField]
        GameObject m_MainMenu = null;

        public event Action OnGameRequest;

        void Awake()
        {
            m_PlayButton.onClick.AddListener(() =>
            {
                OnGameRequest?.Invoke();
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

