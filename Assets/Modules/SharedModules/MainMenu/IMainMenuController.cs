using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public interface IMainMenuController : ISceneManager
    {
        event Action OnGameRequest;

        void Active();
        void Deactivate();
    }
}
