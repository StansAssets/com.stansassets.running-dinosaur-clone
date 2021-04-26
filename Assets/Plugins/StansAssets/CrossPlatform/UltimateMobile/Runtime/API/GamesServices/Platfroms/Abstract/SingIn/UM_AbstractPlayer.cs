using System;
using System.Collections.Generic;
using UnityEngine;

namespace SA.CrossPlatform.GameServices
{
    [Serializable]
    abstract class UM_AbstractPlayer
    {
        [SerializeField]
        protected string m_id;
        [SerializeField]
        protected string m_alias;
        [SerializeField]
        protected string m_displayName;

        public string Id => m_id;

        public string Alias => m_alias;

        public string DisplayName => m_displayName;
    }
}
