using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.ProjectSample.Core
{
    public interface IMainMenuController : ISceneManager
    {
        event UnityAction OnGameRequest;

        void Active();
        void Deactivate();
    }
}
