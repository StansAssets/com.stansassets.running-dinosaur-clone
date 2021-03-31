using System;
using StansAssets.SceneManagement;
using UnityEngine.Assertions;

namespace StansAssets.ProjectSample.Core
{
    class DefaultSceneLoadService : SceneLoadService, ISceneService
    {
        public IScenePreloader Preloader { get; private set; }

        public void PreparePreloader(Action onInit)
        {
            Load<ISceneManager>(AppConfig.MobilePreloaderSceneName, (scene, sceneManager) =>
            {
                Preloader = (IScenePreloader)sceneManager;
                Assert.IsNotNull(Preloader);

                onInit.Invoke();
            });
        }
    }
}
