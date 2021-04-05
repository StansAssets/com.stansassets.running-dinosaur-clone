using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.ProjectSample.Boxes.PauseUI
{
    public interface IDinoPauseUI : ISceneManager
    {
        event UnityAction OnBack;
        event UnityAction OnMainMenu;
        event UnityAction OnRestart;
    }
}
