using System;
using UnityEngine;
using System.Collections;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    class UM_EditorPlayer : UM_AbstractPlayer, UM_iPlayer
    {
        public Texture2D Avatar = null;

        public new string Id
        {
            get => m_id;

            set => m_id = value;
        }

        public new string Alias
        {
            get => m_alias;

            set => m_alias = value;
        }

        public new string DisplayName
        {
            get => m_displayName;

            set => m_displayName = value;
        }

        public void GetAvatar(Action<Texture2D> callback)
        {
            UM_EditorAPIEmulator.WaitForNetwork(() =>
            {
                callback.Invoke(Avatar);
            });
        }
    }
}
