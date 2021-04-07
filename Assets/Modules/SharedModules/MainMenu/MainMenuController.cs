using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class MainMenuController : MonoBehaviour, IMainMenuController
    {
        public event UnityAction OnGameRequest {
            add => m_PlayButton.onClick.AddListener (value);
            remove => m_PlayButton.onClick.RemoveListener (value);
        }
        
        [SerializeField] Button m_PlayButton;
        [SerializeField] GameObject m_MainMenu;

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

