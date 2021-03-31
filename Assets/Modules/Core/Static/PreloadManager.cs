using System;
using System.Collections.Generic;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    class PreloadManager : IApplicationStateDelegate<AppState>
    {
        readonly IScenePreloader m_Preloader;

        public PreloadManager(IApplicationStateStack<AppState> stateStack, IScenePreloader preloader)
        {
            m_Preloader = preloader;
            stateStack.AddDelegate(this);

            stateStack.SetPreprocessAction(OnStackPreprocess);
            stateStack.SetPostprocessAction(OnStackPostprocess);
        }

        void OnStackPreprocess(StackOperationEvent<AppState> e, Action onComplete)
        {
            if (e.State.Equals(AppState.Game) || e.State.Equals(AppState.MainMenu))
            {
                m_Preloader.FadeIn(onComplete.Invoke);
            }
            else
            {
                onComplete.Invoke();
            }
        }

        void OnStackPostprocess(StackOperationEvent<AppState> e, Action onComplete)
        {
            if (e.State.Equals(AppState.Game) || e.State.Equals(AppState.MainMenu))
            {
                m_Preloader.FadeOut(onComplete.Invoke);
            }
            else
            {
                onComplete.Invoke();
            }
        }

        public void ApplicationStateChanged(StackOperationEvent<AppState> e) { }
        public void OnApplicationStateWillChanged(StackOperationEvent<AppState> e) { }

        public void ApplicationStateChangeProgressChanged(float progress, StackChangeEvent<AppState> e)
        {
            m_Preloader.OnProgress(progress);
        }
    }
}
