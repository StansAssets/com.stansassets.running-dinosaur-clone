using System;
using StansAssets.Foundation.Extensions;
using StansAssets.ProjectSample.Dino.Game;
using UnityEngine.SceneManagement;

namespace StansAssets.ProjectSample.Dino
{
    public class DinoGame
    {
        public event Action OnGameOver, OnStart;
        
        readonly DinoLevel m_DinoLevel;
        readonly DinoCharacter m_DinoCharacter;

        public DinoGame (Scene targetScene, IDinoInGameUI ui)
        {
            OnStart += ui.Reset;
            
            m_DinoCharacter = targetScene.GetComponentInChildren<DinoCharacter> ();
            m_DinoCharacter.OnHit += () => OnGameOver?.Invoke ();
            OnGameOver += () => m_DinoCharacter.State = DinoState.Dead;
            OnStart += () => {
                m_DinoCharacter.Reset ();
                m_DinoCharacter.State = DinoState.Grounded;
            };
            
            m_DinoLevel = targetScene.GetComponentInChildren<DinoLevel> ();
            
            m_DinoLevel.OnScoreGained += ui.AddPoints;
            OnStart += () => {
                m_DinoLevel.Reset ();
                m_DinoLevel.SetLevelActive (true);
            };
            
        }

        internal void Restart ()
        {
            OnGameOver?.Invoke ();
            OnStart?.Invoke ();
        }

        internal void Pause (bool isPaused)
        {
            m_DinoLevel.SetLevelActive (!isPaused);
            m_DinoCharacter.SetFrozen (isPaused);
        }

        internal void Destroy ()
        {
            
        }

        internal void Start ()
        {
            OnStart?.Invoke ();
        }
    }
}

