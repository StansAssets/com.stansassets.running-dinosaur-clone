using System;

namespace StansAssets.ProjectSample.Boxes
{
    interface IEnemyCharacter : ICharacter
    {
        event Action OnDeath;
        void SetFreeze(bool frozen);
    }
}
