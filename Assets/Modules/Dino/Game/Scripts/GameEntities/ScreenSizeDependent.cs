using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public abstract class ScreenSizeDependent : MonoBehaviour
    {
        public abstract void UpdateScreenWidth(int screenWidthDelta);
    }
}
