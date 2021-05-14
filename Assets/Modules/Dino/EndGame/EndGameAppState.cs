using System;
using System.Linq;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;
using UnityEngine;

namespace StansAssets.Dino.EndGame
{
    [UsedImplicitly]
    public class EndGameAppState: ApplicationState, IAppState
    {
        const string k_sceneName = "EndGame";
        private IDinoEndGameUI m_uiManager;
        
        public AppState StateId => AppState.EndGame;

        public override void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter progressReporter)
        {
            switch (evt.Action) {
                case StackAction.Added:
                    m_SceneActionsQueue.AddAction (SceneActionType.Load, k_sceneName);
                    m_SceneActionsQueue.Start (
                        progressReporter.UpdateProgress,
                        () =>
                        {
                            var scene = m_SceneActionsQueue.GetLoadedScene (k_sceneName);
                            m_uiManager = scene.GetRootGameObjects().First().GetComponentInChildren<IDinoEndGameUI>();
                            m_uiManager.OnMainMenu += () => {
                                App.State.Set (AppState.MainMenu);
                            };

                            m_uiManager.OnRestart += () => {
                                App.State.Pop ();
                            };
                            
                            progressReporter.SetDone ();
                        });
                    break;
                
                case StackAction.Removed:
                    m_SceneActionsQueue.AddAction (SceneActionType.Unload, k_sceneName);
                    m_SceneActionsQueue.Start (progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                
                default: throw new ArgumentOutOfRangeException (nameof(evt.Action), evt.Action, null);
            }
        }
    }
}