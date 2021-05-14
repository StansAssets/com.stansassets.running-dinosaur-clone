using UnityEngine;

namespace StansAssets.Dino.Game
{
	public class ScreenPivot : MonoBehaviour
	{ 
		[SerializeField] Vector3 m_ScreenPivotPoint;
		[SerializeField] Vector3 m_ScreenPivotOffset;

		Vector3 GetPivotPointPosition(Vector2 screenSize) => new Vector3(screenSize.x * (m_ScreenPivotPoint.x - 0.5f), screenSize.y * (m_ScreenPivotPoint.y - 0.5f));

		private void OnValidate()
		{
			var pivotPoint = GetPivotPointPosition(new Vector2(Screen.width, Screen.height));
			m_ScreenPivotOffset = new Vector2(transform.position.x - pivotPoint.x, transform.position.y - pivotPoint.y);
		}
	}
}
