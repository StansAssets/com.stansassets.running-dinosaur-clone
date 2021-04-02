using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes.PauseUI
{
    public interface IDinoPauseUI : ISceneManager
    {
        event Action OnBack;
        event Action OnMainMenu;
        event Action OnRestart;
    }
}
