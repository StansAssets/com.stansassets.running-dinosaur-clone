using System;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterController2D))]
    public class BoxMainCharacter : MonoBehaviour, IMainCharacter, IPushable
    {
        [SerializeField]
        float m_RunSpeed = 40f;

        [SerializeField]
        float m_EnemyPushImpulse = 40f;

        float m_HorizontalMove = 0f;
        bool m_Jump = false;

        Vector3 m_SpawnPoint;

        IInputService m_InputService;
        CharacterController2D m_Controller;
        Rigidbody2D m_Rigidbody2D;

        void Awake()
        {
            enabled = false;
            m_Controller = GetComponent<CharacterController2D>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Init(IReadOnlyServiceLocator services, Action onComplete)
        {
            m_InputService = services.Get<IInputService>();
            m_InputService.OnJump += () =>
            {
                m_Jump = true;
            };

            m_SpawnPoint = transform.localPosition;
            enabled = true;
            onComplete.Invoke();
        }

        void Update()
        {
            m_HorizontalMove = m_InputService.Horizontal * m_RunSpeed;
            m_Controller.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Jump);
            m_Jump = false;
        }

        public void Pause(bool isPaused)
        {
            m_Controller.enabled = !isPaused;
            enabled = !isPaused;
        }

        public void Restart()
        {
            m_Jump = false;
            m_HorizontalMove = 0f;
            transform.localPosition = m_SpawnPoint;
            gameObject.SetActive(true);
        }

        public void Destroy() { }

        public void Die()
        {
            gameObject.SetActive(false);
        }

        public void Push(Vector2 direction)
        {
            m_Rigidbody2D.AddForce(new Vector2(direction.x * m_EnemyPushImpulse, m_EnemyPushImpulse * 0.5f), ForceMode2D.Impulse);
        }
    }
}
