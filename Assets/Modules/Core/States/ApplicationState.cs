using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public abstract class ApplicationState : IApplicationState<AppState>
    {
        protected readonly SceneActionsQueue m_SceneActionsQueue;

        protected ApplicationState()
        {
            var sceneService = App.Services.Get<ISceneService>();
            m_SceneActionsQueue = new SceneActionsQueue(sceneService);
        }

        protected void AddSceneAction(SceneActionType type, string sceneName)
        {
            m_SceneActionsQueue.AddAction(type, sceneName);
        }

        public abstract void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter progressReporter);
    }
}
