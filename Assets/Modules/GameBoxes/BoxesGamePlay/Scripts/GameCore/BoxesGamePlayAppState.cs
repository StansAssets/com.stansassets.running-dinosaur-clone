using System;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Boxes.GameUI;
using StansAssets.ProjectSample.Boxes.PauseUI;
using StansAssets.ProjectSample.Boxes.EndGameUI;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes
{
    [UsedImplicitly]
    public class BoxesGamePlayAppState : ApplicationState, IAppState
    {
        const string k_GamePlaySceneName = "BoxesGamePlayLevel";
        const string k_InGameUISceneName = "BoxesInGameUI";
        const string k_PauseUISceneName = "BoxesPauseUI";
        const string k_VictoryEndGameUISceneName = "BoxesVictoryUI";
        const string k_DefeatEndGameUISceneName = "BoxesDefeatUI";

        readonly ISceneService m_SceneService;

        public AppState StateId => AppState.Game;
        BoxesGame m_BoxesGame;
        IBoxesInGameUI m_InGameUI;

        public BoxesGamePlayAppState()
        {
            m_SceneService = App.Services.Get<ISceneService>();
        }

        public override void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter progressReporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    m_SceneActionsQueue.AddAction(SceneActionType.Load, k_GamePlaySceneName);
                    m_SceneActionsQueue.AddAction(SceneActionType.Load, k_InGameUISceneName);

                    m_SceneActionsQueue.Start(progressReporter.UpdateProgress, () =>
                    {
                        var gamePlayScene = m_SceneActionsQueue.GetLoadedScene(k_GamePlaySceneName);
                        m_BoxesGame = new BoxesGame(gamePlayScene);
                        m_BoxesGame.OnVictory += () => ShowEndGameScreen(k_VictoryEndGameUISceneName);
                        m_BoxesGame.OnDefeat += () => ShowEndGameScreen(k_DefeatEndGameUISceneName);
                        m_BoxesGame.Start(progressReporter.SetDone);

                        m_InGameUI = m_SceneActionsQueue.GetLoadedSceneManager<IBoxesInGameUI>();
                        m_InGameUI.OnPauseRequest += ShowPause;
                    });
                    break;
                case StackAction.Removed:
                    m_BoxesGame.Destroy();
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_GamePlaySceneName);
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_InGameUISceneName);
                    m_SceneActionsQueue.Start(progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                case StackAction.Paused:
                    PauseGame();
                    break;
                case StackAction.Resumed:
                    UnpauseGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }

        void ShowPause()
        {
            PauseGame();
            m_SceneService.Load<IBoxesPauseUI>(k_PauseUISceneName, (scene, manager) =>
            {
                manager.OnBack += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    UnpauseGame();
                };

                manager.OnMainMenu += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    App.State.Set(AppState.MainMenu);
                };

                manager.OnRestart += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    UnpauseGame();
                    m_BoxesGame.Restart();
                };
            });
        }

        void PauseGame()
        {
            m_BoxesGame.Pause(true);
            m_InGameUI.SetActive(false);
        }

        void UnpauseGame()
        {
            m_BoxesGame.Pause(false);
            m_InGameUI.SetActive(true);
        }

        void ShowEndGameScreen(string endGameSceneName)
        {
            PauseGame();
            m_SceneService.Load<IBoxesEndGameUI>(sceneName: endGameSceneName, onComplete: (scene, manager) =>
            {
                manager.OnMainMenu += () =>
                {
                    m_SceneService.Unload(endGameSceneName, () => { });
                    App.State.Set(AppState.MainMenu);
                };

                manager.OnRestart += () =>
                {
                    m_SceneService.Unload(endGameSceneName, () => { });
                    UnpauseGame();
                    m_BoxesGame.Restart();
                };
            });
        }
    }
}
