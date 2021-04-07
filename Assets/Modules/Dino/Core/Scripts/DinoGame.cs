using System;
using StansAssets.Foundation.Extensions;
using StansAssets.ProjectSample.Dino.Game;
using UnityEngine.SceneManagement;
using StansAssets.ProjectSample.Core;

namespace StansAssets.ProjectSample.Dino
{
    public class DinoGame
    {
        const string k_InGameUISceneName = "DinoInGameUI";
        
        public event Action OnGameOver, OnStart;
        
        readonly DinoLevel m_DinoLevel;
        readonly DinoCharacter m_DinoCharacter;

        public DinoGame (Scene targetScene)
        {
            m_DinoCharacter = targetScene.GetComponentInChildren<DinoCharacter> ();
            m_DinoCharacter.OnHit += () => OnGameOver?.Invoke ();
            OnGameOver += () => m_DinoCharacter.State = DinoState.Dead;
            
            m_DinoLevel = targetScene.GetComponentInChildren<DinoLevel> ();
            // in would be nice to load/unload related scenes implicitly
            // with attribute like [BindScene(k_InGameUISceneName)]
            App.Services.Get<ISceneService> ().Load<IDinoInGameUI> (k_InGameUISceneName,
                                                                    (scene, ui) => {
                                                                        OnStart += ui.Reset;
                                                                        m_DinoLevel.OnScoreGained += ui.AddPoints;
                                                                    });
            
            OnStart += () => {
                m_DinoCharacter.State = DinoState.WaitingForStart;
                m_DinoLevel.Reset ();
                m_DinoLevel.SetLevelActive (true);
                m_DinoCharacter.State = DinoState.Grounded;
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
            App.Services.Get<ISceneService> ().Unload (k_InGameUISceneName, () => { });
        }

        internal void Start ()
        {
            OnStart?.Invoke ();
        }
    }
}

