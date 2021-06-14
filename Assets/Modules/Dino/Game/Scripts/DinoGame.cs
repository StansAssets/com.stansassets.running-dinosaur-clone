using System;
using System.Collections.Generic;
using StansAssets.Foundation.Extensions;
using UnityEngine.SceneManagement;
using StansAssets.ProjectSample.Core;
using SA.CrossPlatform.Analytics;
using StansAssets.Dino.GameServices;

namespace StansAssets.Dino.Game
{
	public class DinoGame
    {
        const string k_InGameUISceneName = "DinoInGameUI";

        public event Action OnGameOver, OnStart;

        readonly DinoLevel m_DinoLevel;
        readonly DinoCharacter m_DinoCharacter;

        internal bool IsGameOver { get; private set; }
        private IGameServices m_gameServices;

        public DinoGame(Scene targetScene) {

            m_gameServices = App.Services.Get<IGameServices>();
            m_DinoCharacter = targetScene.GetComponentInChildren<DinoCharacter>();
            m_DinoCharacter.OnHit += () => OnGameOver?.Invoke();
            OnGameOver += () =>
            {
                IsGameOver = true;
                m_DinoCharacter.State = DinoState.Dead;
                var details = new Dictionary<string, object>();
                details.Add("Score", m_DinoLevel.Score);
                UM_AnalyticsService.Client.Event("GameOver", details);
                m_gameServices.SubmitScore(m_DinoLevel.Score);
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

        internal void Pause()
        {
            SetGameState(false);
        }

        internal void Resume()
        {
            SetGameState(true);
        }

        internal void Destroy()
        {
            App.Services.Get<ISceneService>().Unload(k_InGameUISceneName, () => { });
        }

        internal void Start()
        {
            OnStart?.Invoke();
        }

        void SetGameState(bool state)
        {
            m_DinoLevel.SetLevelActive(state);
            m_DinoCharacter.SetFrozen(!state);
        }
    }
}

