using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public abstract class PoolableGameObject : MonoBehaviour
    {
        Action m_OnRelease;
        public abstract void Init();
        protected abstract void OnRelease();

        public void Release()
        {
            OnRelease();
            m_OnRelease?.Invoke();
        }

        internal void SetOnReleaseCallback(Action onRelease)
        {
            m_OnRelease = onRelease;
        }
    }
}
