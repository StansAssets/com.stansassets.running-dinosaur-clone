using StansAssets.ProjectSample.Ads;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StansAssets.Dino.EndGame
{
    public class EndGameUIController : MonoBehaviour, IDinoEndGameUI
    {
        public event UnityAction OnMainMenu {
            add => m_MainMenu.onClick.AddListener (value);
            remove => m_MainMenu.onClick.RemoveListener (value);
        }
        
        public event UnityAction OnRestart {
            add => m_RestartButton.onClick.AddListener (value);
            remove => m_RestartButton.onClick.RemoveListener (value);
        }
        
        [SerializeField] Button m_RestartButton;
        [SerializeField] Button m_MainMenu;


        void Start()
        {
            App.Services.Get<AdsManager>().ShowRewardedAds(HandleRewardedAdsResult);
        }

        void HandleRewardedAdsResult(bool result)
        {
            m_RestartButton.enabled = true;
            m_MainMenu.enabled = true;
        }
    }
}
