using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes.EndGameUI
{
    public interface IDinoEndGameUI : ISceneManager
    {
        event Action OnMainMenu;
        event Action OnRestart;
    }
}
