using StansAssets.SceneManagement;

public interface IDinoInGameUI : ISceneManager
{
    void Reset ();
    void AddPoints (float pts);
    void SetLivesAmount(int lives);
    void SetPause (bool paused);
}
