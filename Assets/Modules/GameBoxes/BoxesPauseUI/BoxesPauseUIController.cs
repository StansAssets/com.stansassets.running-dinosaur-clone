using System;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Boxes.PauseUI
{
    public class BoxesPauseUIController : MonoBehaviour, IBoxesPauseUI
    {
        [SerializeField]
        Button m_BackButton = null;

        [SerializeField]
        Button m_RestartButton = null;

        [SerializeField]
        Button m_MainMenu = null;


        public event Action OnBack;
        public event Action OnMainMenu;
        public event Action OnRestart;

        void Awake()
        {
            m_BackButton.onClick.AddListener(() =>
            {
                OnBack?.Invoke();
            });

            m_RestartButton.onClick.AddListener(() =>
            {
                OnRestart?.Invoke();
            });

            m_MainMenu.onClick.AddListener(() =>
            {
                OnMainMenu?.Invoke();
            });
        }
    }
}
