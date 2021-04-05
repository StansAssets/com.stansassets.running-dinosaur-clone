using StansAssets.SceneManagement;
using UnityEngine.Events;

namespace StansAssets.ProjectSample.Boxes.EndGameUI
{
    public interface IDinoEndGameUI : ISceneManager
    {
        event UnityAction OnMainMenu;
        event UnityAction OnRestart;
    }
}
