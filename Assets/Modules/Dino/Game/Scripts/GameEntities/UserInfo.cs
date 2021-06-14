using StansAssets.Dino.GameServices;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.Dino.Game
{
    public class UserInfo : MonoBehaviour
    {
        private IGameServices m_gameServices;
        [SerializeField] private RawImage m_avatarImage;
        [SerializeField] private Text m_playerName;
        [SerializeField] private Button m_leaderboardsButton ;
        void Start() {
            m_gameServices = App.Services.Get<IGameServices>();
            var player = m_gameServices.CurrentPlayer;
            if (player != null) {
                m_playerName.text = player.DisplayName;
                player.GetAvatar((texture) => {
                    m_avatarImage.texture = texture;`
                });
            }

            m_leaderboardsButton.onClick.AddListener(() => {
                m_gameServices.ShowLeaderboards();
            });
        }


    }
}
