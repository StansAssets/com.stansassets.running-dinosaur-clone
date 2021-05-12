using StansAssets.ProjectSample.Dino.Game;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino
{
	[ExecuteAlways]
	public class ScreenSizeControl : MonoBehaviour
	{
		[SerializeField] Vector2 m_CurrentScreenWidth = new Vector2(1920, 1080);
		ScreenSizeDependent[] m_ScreenSizeDependentObjects;

		private void Start()
		{
			m_ScreenSizeDependentObjects = GameObject.FindObjectsOfType<ScreenSizeDependent>();
		}

		private void Update()
		{
			if (m_ScreenSizeDependentObjects != null)
				UpdateResolution();
		}

		void UpdateResolution()
        {
		    var screenSizeDelta = new Vector2(Screen.width - m_CurrentScreenWidth.x, Screen.height - m_CurrentScreenWidth.y);
            if (screenSizeDelta.magnitude > 1)
			{
				var newSize = new Vector2(Screen.width, Screen.height);
				foreach (var spawner in m_ScreenSizeDependentObjects) {
                    spawner.UpdateScreenSize(m_CurrentScreenWidth, newSize);
                }
				m_CurrentScreenWidth = newSize;
            }
        }

	}
}

