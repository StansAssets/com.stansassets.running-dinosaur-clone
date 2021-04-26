using SA.CrossPlatform.GameServices;
using SA.CrossPlatform.UI;
using UnityEngine;
using UnityEngine.UI;
//com.crescentmoongames.starfetched 
namespace SA.CrossPlatform.Samples
{
    public class UM_AchievementMetaView : MonoBehaviour
    {
        [SerializeField]
        Text m_Title = default;

        [SerializeField]
        Button m_IncrementButton = default;

        UM_iAchievement m_Achievement;

        void Awake()
        {
            m_IncrementButton.onClick.AddListener(() =>
            {
                var client = UM_GameService.AchievementsClient;
                client.Increment(m_Achievement.Identifier, 1, UM_DialogsUtility.DisplayResultMessage);
            });
        }

        public void SetAchievement(UM_iAchievement achievement)
        {
            m_Achievement = achievement;
            m_Title.text = $"{achievement.Name} / {achievement.State}";
        }
    }
}
