using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class DinoCharacter : MonoBehaviour
    {
        public event Action OnHit;

        [SerializeField] AudioSource m_JumpAudioSource;
        [SerializeField] Animator m_Animator;
        [SerializeField] Rigidbody2D m_Rigidbody2D;
        [SerializeField] Vector2 m_InitialJumpImpulse;
        [SerializeField] float m_JumpButtonHeldForce;

        // initial position of Dino is used as a respawn position
        Vector3 m_SpawnPosition;
        // increases a height of jump if user holds the Jump button
        ConstantForce2D m_Force2D;
        DinoState m_State;
        
        void Start ()
        {
            State = DinoState.Grounded;
            m_SpawnPosition = transform.localPosition;
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            m_Force2D = m_Rigidbody2D.gameObject.AddComponent<ConstantForce2D> ();
            m_Force2D.force = Vector2.up * m_JumpButtonHeldForce;
            m_Force2D.enabled = false;
        }


        public DinoState State {
            get => m_State;
            set {
                if (m_State == value) return;

                switch (value) {
                    case DinoState.Jumping:
                        m_JumpAudioSource.Play ();
                        Jump ();
                        break;
                    case DinoState.Ducking:
                    case DinoState.Grounded: 
                        break;
                    case DinoState.Dead: 
                        SetFrozen (true);
                        break;
                    case DinoState.WaitingForStart:
                        transform.localPosition = m_SpawnPosition;
                        SetFrozen (true);
                        break;
                    default: throw new ArgumentOutOfRangeException (nameof(value), value, null);
                }

                m_State = value;
                m_Animator.Play (GetAnimationName ());
            }
        }

        public void SetFrozen (bool frozen)
        {
            //todo
        }

        void FixedUpdate ()
        {
            bool jumpInputPressed = Input.GetButton ("Jump") || Input.GetAxis ("Vertical") > 0.05f;
            bool duckInputPressed = Input.GetAxis ("Vertical") < -0.05f;

            var setState = State;
            switch (State) {
                case DinoState.Grounded:
                    if (jumpInputPressed) 
                        setState = DinoState.Jumping; 
                    else if (duckInputPressed)  
                        setState = DinoState.Ducking; 
                    break;
                case DinoState.Ducking:
                    if (!duckInputPressed)
                        setState = DinoState.Grounded;
                    break;
                case DinoState.Jumping:
                    if (!jumpInputPressed
                     && !m_Force2D.enabled)
                        m_Force2D.enabled = false;
                    break;
            }

            State = setState;
        }

        void Jump ()
        {
            m_Rigidbody2D.AddForce (m_InitialJumpImpulse, ForceMode2D.Impulse);
            m_Force2D.enabled = true;
        }

        void OnCollisionEnter2D (Collision2D collision)
        {
            if (collision.gameObject.CompareTag ("Player"))
                State = DinoState.Grounded;
            else
                OnHit?.Invoke ();
        }

        string GetAnimationName ()
        {
            switch (State) {
                case DinoState.Dead: return "dino-dead";
                case DinoState.WaitingForStart: return "dino-waiting";
                case DinoState.Grounded: return "dino-running";
                case DinoState.Jumping: return "dino-jumping";
                case DinoState.Ducking: return "dino-ducking";
                default: throw new ArgumentOutOfRangeException ();
            }
        }
    }
}
