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

        public event Action OnGameOver = delegate { };
        public event Action OnStart = delegate { };

        public event Action<int> OnLivesAmountChanged = delegate { };

        readonly DinoLevel m_DinoLevel;
        readonly DinoCharacter m_DinoCharacter;

        internal bool IsGameOver { get; private set; }

        internal int m_LivesLeft;
        internal int LivesLeft
        {
            get => m_LivesLeft;
            private set
            {
                m_LivesLeft = value;
                OnLivesAmountChanged(m_LivesLeft);
            }
        }

        private IDinoInGameUI m_InGameUI;
        private IGameServices m_GameServices;

        private const int DinoLives = 3;

        public DinoGame(Scene targetScene) {
            LivesLeft = DinoLives;
            m_GameServices = App.Services.Get<IGameServices>();
            m_DinoCharacter = targetScene.GetComponentInChildren<DinoCharacter>();
            m_DinoCharacter.OnHit += OnHitCallbackHandler;

            m_DinoLevel = targetScene.GetComponentInChildren<DinoLevel>();
            App.Services.Get<ISceneService>()
                .Load<IDinoInGameUI>(
                    k_InGameUISceneName,
                    (scene, ui) =>
                    {
                        ui.SetLivesAmount(LivesLeft);
                        OnStart += ui.Reset;
                        OnLivesAmountChanged += ui.SetLivesAmount;
                        m_DinoLevel.OnScoreGained += ui.AddPoints;
                    });
        }

        private void OnHitCallbackHandler()
        {
            LivesLeft--;
            if (LivesLeft == 0)
            {
                IsGameOver = true;
                m_DinoCharacter.State = DinoState.Dead;
                var details = new Dictionary<string, object>();
                details.Add("Score", m_DinoLevel.Score);
                UM_AnalyticsService.Client.Event("GameOver", details);
                m_GameServices.SubmitScore(m_DinoLevel.Score);
                OnGameOver();
            }
        }

        internal void Restart()
        {
            StartGame();
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
            StartGame();
        }

        private void StartGame()
        {
            LivesLeft = DinoLives;
            m_DinoCharacter.State = DinoState.WaitingForStart;
            m_DinoLevel.Reset();
            m_DinoLevel.SetLevelActive(true);
            m_DinoCharacter.State = DinoState.Grounded;
            m_DinoCharacter.SetFrozen(false);

            OnStart.Invoke();
        }

        void SetGameState(bool state)
        {
            m_DinoLevel.SetLevelActive(state);
            m_DinoCharacter.SetFrozen(!state);
        }
    }
}

