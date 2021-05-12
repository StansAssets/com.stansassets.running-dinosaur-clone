using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.Dino.PauseUI
{
    public interface IPauseUIController : ISceneManager
    {
        event UnityAction OnBack;
        event UnityAction OnMainMenu;
        event UnityAction OnRestart;
    }
}
