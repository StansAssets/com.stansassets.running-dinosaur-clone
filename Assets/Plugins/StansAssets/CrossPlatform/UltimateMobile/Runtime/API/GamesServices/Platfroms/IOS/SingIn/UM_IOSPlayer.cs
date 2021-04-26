using System;
using UnityEngine;
using SA.iOS.GameKit;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    class UM_IOSPlayer : UM_AbstractPlayer, UM_iPlayer
    {
        [SerializeField]
        ISN_GKPlayer m_Player;

        public UM_IOSPlayer(ISN_GKPlayer player)
        {
            m_id = player.PlayerId;
            m_alias = player.Alias;
            m_displayName = player.DisplayName;
            m_Player = player;
        }

        public void GetAvatar(Action<Texture2D> callback)
        {
            m_Player.LoadPhoto(GKPhotoSize.Normal, result =>
            {
                callback.Invoke(result.IsSucceeded ? result.Image : null);
            });
        }
    }
}
