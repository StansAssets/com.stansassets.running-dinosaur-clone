using System;
using UnityEngine;
using SA.Android.GMS.Games;
using SA.Android.GMS.Common.Images;
using SA.Android.Utilities;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    class UM_AndroidPlayer : UM_AbstractPlayer, UM_iPlayer
    {
        [SerializeField]
        AN_Player m_anPlayer;
        Texture2D m_Avatar = null;

        public UM_AndroidPlayer(AN_Player player)
        {
            m_id = player.PlayerId;
            m_alias = player.Title;
            m_displayName = player.DisplayName;

            m_anPlayer = player;
        }

        public void GetAvatar(Action<Texture2D> callback)
        {
            if (m_Avatar != null)
            {
                callback.Invoke(m_Avatar);
                return;
            }

            if (!m_anPlayer.HasHiResImage)
            {
                callback.Invoke(null);
                return;
            }

            var url = m_anPlayer.HiResImageUri;
            var manager = new AN_ImageManager();
            AN_Logger.Log("TrYING TO LOAD AN IMAGE");
            manager.LoadImage(url, (result) =>
            {
                if (result.IsSucceeded)
                    callback.Invoke(result.Image);
                else
                    callback.Invoke(null);
            });
        }
    }
}
