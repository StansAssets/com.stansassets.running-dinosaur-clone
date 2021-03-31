using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public interface ISceneService : ISceneLoadService
    {
        IScenePreloader Preloader { get; }
    }
}
