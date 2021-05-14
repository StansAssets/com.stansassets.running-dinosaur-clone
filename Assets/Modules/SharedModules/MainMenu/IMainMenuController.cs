using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.ProjectSample.Core
{
    public interface IMainMenuController : ISceneManager
    {
        event UnityAction OnPlayRequested;

        void Active();
        void Deactivate();
    }
}
