using System;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Boxes.EndGameUI
{
    public class BoxesEndGameUIController : MonoBehaviour, IBoxesEndGameUI
    {
        [SerializeField]
        Button m_RestartButton = null;

        [SerializeField]
        Button m_MainMenu = null;

        public event Action OnMainMenu;
        public event Action OnRestart;

        void Awake()
        {
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
