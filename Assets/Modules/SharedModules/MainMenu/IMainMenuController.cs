using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public interface IMainMenuController : ISceneManager
    {
        event Action OnGameRequest;
        event Action OnSettingsRequest;

        void Active();
        void Deactivate();
    }
}
