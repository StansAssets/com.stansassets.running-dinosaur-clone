using System;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;

namespace StansAssets.Dino.Game
{
    [UsedImplicitly]
    public class DinoGameAppState : ApplicationState, IAppState
    {
        const string k_GamePlaySceneName = "DinoGame";

        public AppState StateId => AppState.Game;
        DinoGame m_DinoGame;

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
                                                   m_DinoGame.OnGameOver += OnGameOverHandler;
                                                   progressReporter.SetDone ();
                                                   m_DinoGame.Start ();
                                               });
                    break;
                
                case StackAction.Removed:
                    m_DinoGame.OnGameOver -= OnGameOverHandler;
                    m_DinoGame.Destroy ();
                    
                    m_SceneActionsQueue.AddAction (SceneActionType.Unload, k_GamePlaySceneName);
                    m_SceneActionsQueue.Start (progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                
                case StackAction.Paused:
                    m_DinoGame.Pause();
                    progressReporter.SetDone();
                    break;
                
                case StackAction.Resumed:
                    if (m_DinoGame.IsGameOver)
                    {
                        m_DinoGame.Restart();
                    }
                    else
                    {
                        m_DinoGame.Resume();
                    }
                    progressReporter.SetDone();
                    break;
                
                default: throw new ArgumentOutOfRangeException (nameof(evt.Action), evt.Action, null);
            }
        }

        void OnGameOverHandler()
        {
            App.State.Push (AppState.EndGame);
        }
    }
}
