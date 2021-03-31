using System;
using System.Collections;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public class DestroyOverTimePoolable : PoolableGameObject
    {
        
        [SerializeField]
        float m_PlayTime = 1f;


        public override void Init()
        {
            StartCoroutine(WaitForPlay());
        }

        protected override void OnRelease()
        {
        }

        IEnumerator WaitForPlay()
        {
            yield return new WaitForSeconds(m_PlayTime);
            Release();
        }

    }
}
