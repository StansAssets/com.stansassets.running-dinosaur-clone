using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    class DeathZone : CollisionZone<ICharacter>
    {
        protected override Color GizmoColor => Color.red;
    }
}
