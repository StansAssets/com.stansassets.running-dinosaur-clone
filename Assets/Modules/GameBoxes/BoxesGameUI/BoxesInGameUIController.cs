using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Boxes.GameUI
{
    class BoxesInGameUIController : MonoBehaviour, IBoxesInGameUI
    {
        [SerializeField]
        Button m_PauseButton = null;

        public event Action OnPauseRequest;

        void Awake()
        {
            m_PauseButton.onClick.AddListener(RequestPause);
        }

        void RequestPause () {
            OnPauseRequest?.Invoke();
        }

        public void SetActive (bool active) {
            m_PauseButton.gameObject.SetActive (active);
        }
    }
}
