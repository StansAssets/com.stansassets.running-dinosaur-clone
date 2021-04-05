using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Boxes.PauseUI
{
    public class DinoPauseUIController : MonoBehaviour, IDinoPauseUI
    {
        public event UnityAction OnBack {
            add => m_BackButton.onClick.AddListener (value);
            remove => m_BackButton.onClick.RemoveListener (value);
        }
        
        public event UnityAction OnMainMenu {
            add => m_MainMenu.onClick.AddListener (value);
            remove => m_MainMenu.onClick.RemoveListener (value);
        }
        
        public event UnityAction OnRestart {
            add => m_RestartButton.onClick.AddListener (value);
            remove => m_RestartButton.onClick.RemoveListener (value);
        }
        
        [SerializeField] Button m_BackButton;
        [SerializeField] Button m_RestartButton;
        [SerializeField] Button m_MainMenu;

    }
}
