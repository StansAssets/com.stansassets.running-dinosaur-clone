using System;
using UnityEngine;
using StansAssets.ProjectSample.Controls;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class DinoCharacter : ScreenSizeDependent
    {
        public event Action OnHit;

        [SerializeField] AudioSource m_JumpAudioSource;
        [SerializeField] Animator m_Animator;
        [SerializeField] Rigidbody2D m_Rigidbody2D;
        [SerializeField] Vector2 m_InitialJumpImpulse;
        [SerializeField] float m_JumpButtonHeldForce;
        [SerializeField] ConstantForce2D m_Force2D;

        // initial position of Dino is used as a respawn position
        Vector2 m_SpawnPosition;
        // increases a height of jump if user holds the Jump button
        DinoState m_State;
        
        void Start ()
        {
            State = DinoState.Grounded;
            m_SpawnPosition = m_Rigidbody2D.position;

            m_Force2D.force = Vector2.up * m_JumpButtonHeldForce;
            m_Force2D.enabled = false;

            var inputs = FindObjectOfType<InputControl>();
            inputs.Subscribe("Jump",
                             jumpPressed => {
                                 if (jumpPressed) {
                                     if (State != DinoState.Jumping)
                                         State = DinoState.Jumping;
                                 }
                                 else {
                                     m_Force2D.enabled = false;
                                 }
                             });
            inputs.Subscribe("Duck",
                             duckingPressed => {
                                 if (duckingPressed) {
                                     if (State == DinoState.Grounded)
                                         State = DinoState.Ducking;
                                 }
                                 else {
                                     if (State == DinoState.Ducking)
                                         State = DinoState.Grounded;
                                 }
                             });
        }


        public DinoState State {
            get => m_State;
            set {
                if (m_State == value) return;

                switch (value) {
                    case DinoState.Jumping:
                        m_JumpAudioSource.Play ();
                        m_Force2D.enabled = true;
                        m_Rigidbody2D.AddForce (m_InitialJumpImpulse, ForceMode2D.Impulse);
                        break;
                    case DinoState.Ducking:
                    case DinoState.Grounded:
                        m_Force2D.enabled = false;
                        break;
                    case DinoState.Dead: 
                        SetFrozen (true);
                        break;
                    case DinoState.WaitingForStart:
                        SetFrozen (true);
                        m_Rigidbody2D.MovePosition (m_SpawnPosition);
                        break;
                    default: throw new ArgumentOutOfRangeException (nameof(value), value, null);
                }

                m_State = value;
                m_Animator.Play (GetAnimationName ());
            }
        }

        public void SetFrozen (bool frozen)
        {
            m_Rigidbody2D.bodyType = frozen ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
            m_Animator.speed = frozen ? 0 : 1;
        }

        void OnCollisionEnter2D (Collision2D collision)
        {
            if (collision.gameObject.CompareTag ("Player")) {
                if (State == DinoState.Jumping)
                    State = DinoState.Grounded;
            }
            else { OnHit?.Invoke (); }
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

        public override void UpdateScreenWidth(int screenWidthDelta)
        {
            m_SpawnPosition.x -= screenWidthDelta;
        }
    }
}
