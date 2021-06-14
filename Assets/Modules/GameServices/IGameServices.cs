using SA.CrossPlatform.GameServices;

namespace StansAssets.Dino.GameServices
{
    public interface IGameServices
    {
        void Init();
        void SubmitScore(long score);
        void ShowLeaderboards();

        UM_iPlayer CurrentPlayer { get; }
    }
}
