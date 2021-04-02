using StansAssets.SceneManagement;

public interface IDinoInGameUI : ISceneManager
{
    void Reset ();
    void AddPoints (float pts);
    void SetPause (bool paused);
}
