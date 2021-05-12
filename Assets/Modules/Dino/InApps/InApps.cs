using System.Collections.Generic;
using SA.CrossPlatform.InApp;
using SA.CrossPlatform.UI;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.InApps
{
    class InApps : MonoBehaviour
    {
        public const string ConsumableProductId = "consumable.product.id";
        public const string PremiumProductId = "premium.product.id";

        [SerializeField] Button m_PremiumPurchaseButton, m_ConsumablePurchaseButton, m_ResetPurchasesButton;
        [SerializeField] List<GameObject> m_PremiumVisuals, m_NormalVisuals;
        [SerializeField] Text m_ConsumablesCounterText;
        
        TransactionObserverExample m_TransactionObserver;
        bool m_Connected;

        void Awake()
        {
            m_TransactionObserver = new TransactionObserverExample(RewardManager.AddConsumable, RewardManager.ActivatePremium);
            m_TransactionObserver.OnTransactionResult += UpdatePurchases;

            UM_InAppService.Client.SetTransactionObserver(m_TransactionObserver);
            UM_InAppService.Client.Connect(
                                           result => {
                                               m_Connected = result.IsSucceeded;
                                               UpdatePurchases();

                                               if (!result.IsSucceeded)
                                                   UM_DialogsUtility.ShowMessage("Error", result.Error.FullMessage);
                                           });

            m_PremiumPurchaseButton.onClick.AddListener(
                                                        () => {
                                                            UM_InAppService.Client.AddPayment(PremiumProductId);
                                                            SetButtonsActive(false);
                                                        });
            m_ConsumablePurchaseButton.onClick.AddListener(
                                                           () => {
                                                               UM_InAppService.Client.AddPayment(ConsumableProductId);
                                                               SetButtonsActive(false);
                                                           });
            m_ResetPurchasesButton.onClick.AddListener(
                                                       () => {
                                                           RewardManager.Reset();
                                                           UpdatePurchases();
                                                       });
        }

        void SetButtonsActive(bool interactable)
        {
            m_ConsumablePurchaseButton.interactable = interactable;
            m_PremiumPurchaseButton.interactable = interactable;
        }
        
        void UpdatePurchases()
        {
            SetButtonsActive(true);
            m_ConsumablePurchaseButton.gameObject.SetActive(m_Connected);
            m_PremiumPurchaseButton.gameObject.SetActive(m_Connected && !RewardManager.HasPremium);
            
            foreach (var go in m_PremiumVisuals) {
                go.SetActive(RewardManager.HasPremium);
            }
            foreach (var go in m_NormalVisuals) {
                go.SetActive(!RewardManager.HasPremium);
            }

            m_ConsumablesCounterText.text = $"Purchases count: {RewardManager.Consumables}";
        }
    }
}

