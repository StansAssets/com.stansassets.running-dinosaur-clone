using System;
using UnityEngine;
using System.Collections;

public class DinoCharacter : MonoBehaviour
{
    public event Action OnHit;
    
    [SerializeField] GameObject m_RunningDino, m_DuckingDino;
    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D m_Rigidbody2D;
    [SerializeField] float m_InitialJumpImpulse;
    [SerializeField] float m_JumpButtonHeldForce;

    Vector2 m_JumpImpulse;
    
    // initial position of Dino is used as a respawn position
    Vector3 m_SpawnPosition;
    // increases a height of jump if user holds the Jump button
    ConstantForce2D m_Force2D;
    
	void Start () {
        State = DinoState.Grounded;
        m_SpawnPosition = transform.position;
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        m_JumpImpulse = Vector2.up * m_InitialJumpImpulse;
        
        m_Force2D = m_Rigidbody2D.gameObject.AddComponent<ConstantForce2D> ();
        m_Force2D.force = Vector2.up * m_JumpButtonHeldForce;
        m_Force2D.enabled = false;
    }

    DinoState m_State;
    
    static readonly int s_DuckingHash = Animator.StringToHash ("ducking");
    static readonly int s_JumpingHash = Animator.StringToHash ("jumping");

    public DinoState State {
        get => m_State;
        set {
            if (m_State == value) return;

            switch (value) {
                case DinoState.Ducking:
                    UpdateVisuals ();
                    
                    break;
                case DinoState.Grounded:
                    break;
                    
                case DinoState.Jumping:
                    jumpAudioSource.Play ();
                    Jump ();
                    break;
                case DinoState.Dead:
                    return;
                default: throw new ArgumentOutOfRangeException (nameof(value), value, null);
            }
            m_State = value;
            UpdateVisuals ();
        }
    }

    void Reset ()
    {
        transform.position = m_SpawnPosition;
        State = DinoState.Grounded;
    }

    void UpdateVisuals ()
    {
        m_RunningDino.SetActive (State != DinoState.Ducking);
        m_DuckingDino.SetActive (State == DinoState.Ducking);
        animator.SetBool (s_DuckingHash, State == DinoState.Ducking);
        animator.SetBool (s_JumpingHash, State == DinoState.Jumping);
    }

	void FixedUpdate () 
    {
        bool jumpInputPressed = Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0.05f;
        bool duckInputPressed = Input.GetAxis("Vertical") < -0.05f;

        var setState = State;
        switch (State) {
            case DinoState.Grounded:
                if (jumpInputPressed) {
                    setState = DinoState.Jumping;
                } else if (duckInputPressed) {
                    setState = DinoState.Ducking;
                }
                break;
            case DinoState.Ducking:
                if (!duckInputPressed)
                    setState = DinoState.Grounded;
                break;
            case DinoState.Jumping:
                if (!jumpInputPressed && !m_Force2D.enabled) 
                    m_Force2D.enabled = false;
                break;
        }
        State = setState;
    }

    void Jump ()
    {
        m_Rigidbody2D.AddForce (m_JumpImpulse, ForceMode2D.Impulse);
        m_Force2D.enabled = true;
    }

	void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player")) 
            State = DinoState.Grounded;
        else 
            OnHit?.Invoke ();
    }
}
