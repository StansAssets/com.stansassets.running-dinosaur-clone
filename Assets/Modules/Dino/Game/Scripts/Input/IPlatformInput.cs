using System;

namespace StansAssets.ProjectSample.Controls
{
    public interface IPlatformInput
    {
        event Action<string> OnPressed, OnReleased;
    }
}