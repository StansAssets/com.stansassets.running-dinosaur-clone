using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.Dino.EndGame
{
    public interface IDinoEndGameUI : ISceneManager
    {
        event UnityAction OnMainMenu;
        event UnityAction OnRestart;
    }
}
