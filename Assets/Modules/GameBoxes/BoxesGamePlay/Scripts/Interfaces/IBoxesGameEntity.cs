using System;
using StansAssets.Foundation.Patterns;

namespace StansAssets.ProjectSample.Boxes
{
    interface IBoxesGameEntity
    {
        void Init(IReadOnlyServiceLocator services, Action onComplete);
        void Pause(bool isPaused);
        void Restart();
        void Destroy();
    }
}

