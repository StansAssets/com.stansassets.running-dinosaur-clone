using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public interface IAppState : IApplicationState<AppState>
    {
        AppState StateId { get; }
    }
}
