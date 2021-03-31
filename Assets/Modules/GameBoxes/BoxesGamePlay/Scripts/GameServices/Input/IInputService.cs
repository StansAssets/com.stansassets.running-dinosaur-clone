using System;

namespace StansAssets.ProjectSample.Boxes
{
    interface IInputService
    {
        event Action OnJump;

        float Horizontal { get; }
        float Vertical { get; }
    }
}
