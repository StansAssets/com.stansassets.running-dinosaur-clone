using System;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterController2D))]
    public class EnemyBox : PoolableGameObject, IEnemyCharacter
    {
        const float k_DefaultSpeed = -28f;

        public event Action OnDeath;

        bool m_IsFrozen;

        CharacterController2D m_Controller2D;

        public override void Init()
        {
            m_Controller2D = GetComponent<CharacterController2D>();
        }

        protected override void OnRelease()
        {
            OnDeath?.Invoke();
            OnDeath = null;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            var pushable = other.gameObject.GetComponent<IPushable>();
            if (pushable != null)
            {
                var forceDirX = other.transform.position.x.CompareTo(transform.position.x);
                pushable.Push(new Vector2(forceDirX, 0));
            }
        }

        void FixedUpdate()
        {
            if (!m_IsFrozen)
            {
                m_Controller2D.Move(k_DefaultSpeed * Time.fixedDeltaTime, false);
            }
        }

        public void Die()
        {
            Release();
        }

        public void SetFreeze(bool frozen)
        {
            m_IsFrozen = frozen;
            m_Controller2D.enabled = !frozen;
        }
    }
}
