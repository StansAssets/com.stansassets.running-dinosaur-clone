using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes.GameUI
{
    public interface IBoxesInGameUI : ISceneManager
    {
        event Action OnPauseRequest;

        void SetActive (bool active);
    }
}
