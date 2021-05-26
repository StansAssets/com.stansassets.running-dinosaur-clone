using StansAssets.ProjectSample.Core;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.Dino.Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizationTextComponent : MonoBehaviour, ILocalizationListener
    {
        [SerializeField] private string m_token = default;
        [SerializeField] private Text m_text = default;

        private ILocalizationService Service => App.Services.Get<ILocalizationService>();
        public string Name => m_token;

        private void Awake()
        {
            if (m_text == null)
            {
                m_text = GetComponent<Text>();
            }
        }

        private void OnEnable()
        {
            Service.Subscribe(this);
            UpdateText();
        }

        private void OnDisable()
        {
            Service.Unsubscribe(this);
        }

        public void LanguageChanged(string _)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            m_text.text = Service.Get(m_token);
        }
    }
}

