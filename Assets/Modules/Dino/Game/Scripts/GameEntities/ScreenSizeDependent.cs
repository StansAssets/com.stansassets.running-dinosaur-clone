using UnityEngine;

namespace StansAssets.Dino.Game
{
	public abstract class ScreenSizeDependent : MonoBehaviour
	{
		public abstract void UpdateScreenSize(Vector2 fromSize, Vector2 toSize);
	}
}
