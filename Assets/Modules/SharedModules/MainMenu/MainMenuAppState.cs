using System;
using JetBrains.Annotations;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    [UsedImplicitly]
    public class MainMenuAppState : ApplicationState, IAppState
    {
        public AppState StateId => AppState.MainMenu;
        IMainMenuController m_MainMenuController;

        void Setup(IMainMenuController menuController)
        {
            m_MainMenuController = menuController;
            m_MainMenuController.OnGameRequest += () =>
            {
                App.State.Set(AppState.Game);
            };

            m_MainMenuController.OnSettingsRequest += () =>
            {
                App.State.Set(AppState.Settings);
            };
        }

        public override void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter progressReporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    if (m_MainMenuController != null)
                    {
                        m_MainMenuController.Active();
                        progressReporter.SetDone();
                    }
                    else
                    {
                        m_SceneActionsQueue.AddAction(SceneActionType.Load, AppConfig.MainMenuSceneName);
                        m_SceneActionsQueue.Start(progressReporter.UpdateProgress, () =>
                        {
                            var menuController = m_SceneActionsQueue.GetLoadedSceneManager<IMainMenuController>();
                            Setup(menuController);

                            m_MainMenuController.Active();
                            progressReporter.SetDone();
                        });
                    }

                    break;
                case StackAction.Removed:
                    m_MainMenuController.Deactivate();
                    progressReporter.SetDone();
                    break;
                case StackAction.Paused:
                    throw new NotImplementedException();
                case StackAction.Resumed:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }
    }
}
