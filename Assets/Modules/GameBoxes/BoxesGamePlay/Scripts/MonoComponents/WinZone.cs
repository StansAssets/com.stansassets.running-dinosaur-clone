using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    class WinZone : CollisionZone<IMainCharacter>
    {
        protected override Color GizmoColor => Color.green;
    }
}
