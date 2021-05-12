﻿using System;
using System.Collections.Generic;
using StansAssets.Foundation.Extensions;
using StansAssets.ProjectSample.Dino.Game;
using UnityEngine.SceneManagement;
using StansAssets.ProjectSample.Core;
using SA.CrossPlatform.Analytics;

namespace StansAssets.ProjectSample.Dino
{
	public class DinoGame
    {
        const string k_InGameUISceneName = "DinoInGameUI";
        
        public event Action OnGameOver, OnStart;
        
        readonly DinoLevel m_DinoLevel;
        readonly DinoCharacter m_DinoCharacter;

        public DinoGame(Scene targetScene)
        {
            m_DinoCharacter = targetScene.GetComponentInChildren<DinoCharacter>();
            m_DinoCharacter.OnHit += () => OnGameOver?.Invoke();
            OnGameOver += () => {
                m_DinoCharacter.State = DinoState.Dead;
                var details = new Dictionary<string, object>();
                details.Add("Score", m_DinoLevel.Score);
                UM_AnalyticsService.Client.Event("GameOver", details);
            };
            
            m_DinoLevel = targetScene.GetComponentInChildren<DinoLevel>();
            App.Services.Get<ISceneService>()
               .Load<IDinoInGameUI>(
                                    k_InGameUISceneName,
                                    (scene, ui) => {
                                        OnStart += ui.Reset;
                                        m_DinoLevel.OnScoreGained += ui.AddPoints;
                                    });
            
            OnStart += () => {
                m_DinoCharacter.State = DinoState.WaitingForStart;
                m_DinoLevel.Reset();
                m_DinoLevel.SetLevelActive(true);
                m_DinoCharacter.State = DinoState.Grounded;
                m_DinoCharacter.SetFrozen(false);
            };
            
        }

        internal void Restart()
        {
            OnStart?.Invoke();
        }

        internal void Pause(bool isPaused)
        {
            m_DinoLevel.SetLevelActive(!isPaused);
            m_DinoCharacter.SetFrozen(isPaused);
        }

        internal void Destroy()
        {
            App.Services.Get<ISceneService>().Unload(k_InGameUISceneName, () => { });
        }

        internal void Start()
        {
            OnStart?.Invoke();
        }
    }
}

