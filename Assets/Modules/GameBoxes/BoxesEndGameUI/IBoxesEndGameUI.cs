using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes.EndGameUI
{
    public interface IBoxesEndGameUI : ISceneManager
    {
        event Action OnMainMenu;
        event Action OnRestart;
    }
}
