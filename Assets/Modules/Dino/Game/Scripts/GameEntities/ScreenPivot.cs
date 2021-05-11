using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
	public class ScreenPivot : ScreenSizeDependent
	{ 
		[SerializeField] Vector3 m_ScreenPivotPoint;
		[SerializeField] Vector3 m_ScreenPivotOffset;

		Vector3 GetPivotPointPosition(Vector2 screenSize) => new Vector3(screenSize.x * (m_ScreenPivotPoint.x - 0.5f), screenSize.y * (m_ScreenPivotPoint.y - 0.5f));

		private void OnValidate()
		{
			var pivotPoint = GetPivotPointPosition(new Vector2(Screen.width, Screen.height));
			m_ScreenPivotOffset = new Vector2(transform.position.x - pivotPoint.x, transform.position.y - pivotPoint.y);
		}

		public override void UpdateScreenSize(Vector2 fromSize, Vector2 toSize)
		{
			var pivotPoint = GetPivotPointPosition(toSize) + m_ScreenPivotOffset;
			transform.SetPositionAndRotation(pivotPoint, Quaternion.identity);
		}
    }
}
