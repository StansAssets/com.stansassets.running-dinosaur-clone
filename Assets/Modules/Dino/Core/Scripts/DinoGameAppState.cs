using System;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Boxes.EndGameUI;
using StansAssets.ProjectSample.Boxes.PauseUI;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino
{
    [UsedImplicitly]
    public class DinoGameAppState : ApplicationState, IAppState
    {
        const string k_GamePlaySceneName = "DinoGame";
        const string k_PauseUISceneName = "DinoPauseUI";
        const string k_GameOverUISceneName = "DinoGameOverUI";

        readonly ISceneService m_SceneService;

        public AppState StateId => AppState.Game;
        DinoGame m_DinoGame;

        public DinoGameAppState ()
        {
            m_SceneService = App.Services.Get<ISceneService> ();
        }

        public override void ChangeState (StackChangeEvent<AppState> evt, IProgressReporter progressReporter)
        {
            switch (evt.Action) {
                case StackAction.Added:
                    m_SceneActionsQueue.AddAction (SceneActionType.Load, k_GamePlaySceneName);
                    m_SceneActionsQueue.Start (
                                               progressReporter.UpdateProgress,
                                               () => {
                                                   var gamePlayScene = m_SceneActionsQueue.GetLoadedScene (k_GamePlaySceneName);
                                                   m_DinoGame = new DinoGame (gamePlayScene);
                                                   m_DinoGame.OnGameOver += ShowEndGameScreen;
                                                   progressReporter.SetDone ();
                                                   m_DinoGame.Start ();
                                               });
                    break;
                case StackAction.Removed:
                    m_DinoGame.Destroy ();
                    m_SceneActionsQueue.AddAction (SceneActionType.Unload, k_GamePlaySceneName);
                    m_SceneActionsQueue.Start (progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                case StackAction.Paused:
                    PauseGame ();
                    break;
                case StackAction.Resumed:
                    UnpauseGame ();
                    break;
                default: throw new ArgumentOutOfRangeException (nameof(evt.Action), evt.Action, null);
            }
        }

        void ShowPause ()
        {
            PauseGame ();
            m_SceneService.Load<IDinoPauseUI> (
                                                k_PauseUISceneName,
                                                (scene, manager) => {
                                                    manager.OnBack += () => {
                                                        m_SceneService.Unload (k_PauseUISceneName, () => { });
                                                        UnpauseGame ();
                                                    };

                                                    manager.OnMainMenu += () => {
                                                        m_SceneService.Unload (k_PauseUISceneName, () => { });
                                                        App.State.Set (AppState.MainMenu);
                                                    };

                                                    manager.OnRestart += () => {
                                                        m_SceneService.Unload (k_PauseUISceneName, () => { });
                                                        UnpauseGame ();
                                                        m_DinoGame.Restart ();
                                                    };
                                                });
        }

        void PauseGame ()
        {
            m_DinoGame.Pause (true);
        }

        void UnpauseGame ()
        {
            m_DinoGame.Pause (false);
        }

        void ShowEndGameScreen ()
        {
            PauseGame ();
            m_SceneService.Load<IDinoEndGameUI> (
                                                  k_GameOverUISceneName,
                                                  (scene, manager) => {
                                                      Debug.Assert (manager != null);
                                                      manager.OnMainMenu += () => {
                                                          m_SceneService.Unload (k_GameOverUISceneName, () => { });
                                                          App.State.Set (AppState.MainMenu);
                                                      };

                                                      manager.OnRestart += ReloadScene;
                                                  });
        }

        void ReloadScene ()
        {
            try {
                m_SceneService.Unload (
                                       k_GameOverUISceneName,
                                       () => {
                                           UnpauseGame ();
                                           m_DinoGame.Restart ();
                                       });
            }
            catch (Exception e) {
                Debug.LogError (e);
            }
        }
    }
}
