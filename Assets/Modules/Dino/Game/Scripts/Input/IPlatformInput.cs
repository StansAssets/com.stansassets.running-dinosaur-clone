
using System;

public interface IPlatformInput
{
    event Action<string> OnPressed, OnReleased;
}