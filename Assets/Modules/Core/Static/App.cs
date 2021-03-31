using System;
using StansAssets.Foundation;
using StansAssets.Foundation.Patterns;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public static class App
    {
        static readonly ServiceLocator s_Services = new ServiceLocator();
        static readonly ApplicationStateStack<AppState> s_State = new ApplicationStateStack<AppState>();

        public static IReadOnlyServiceLocator Services => s_Services;
        public static IReadOnlyApplicationStateStack<AppState> State => s_State;

        internal static void Init(Action onComplete)
        {
            var sceneService = new DefaultSceneLoadService();
            s_Services.Register<ISceneService>(sceneService);
            s_Services.Register<IPoolingService>(new GameObjectsPool("GameObjects Pool"));

            sceneService.PreparePreloader(() =>
            {
                InitStatesStack();
                var unused = new PreloadManager(s_State, sceneService.Preloader);
                onComplete.Invoke();
            });
        }

        public static void RegisterState(AppState stateId, IApplicationState<AppState> state)
        {
            s_State.RegisterState(stateId, state);
        }

        static void InitStatesStack()
        {
            var stateTypes = ReflectionUtility.FindImplementationsOf<IAppState>();
            foreach (var stateType in stateTypes)
            {
                var stateInstance = Activator.CreateInstance(stateType) as IAppState;
                s_State.RegisterState(stateInstance.StateId, stateInstance);
            }
        }
    }
}
