using UnityEngine;

namespace StrategyGame.Settings {
	[CreateAssetMenu(fileName = "SplashSettingsSo", menuName = "Settings/SplashSettingsSo", order = 1)]
	public class SplashSettingsSo : ScriptableObject {
		[SerializeField] private float splashDuration;

		public float SplashDuration => splashDuration;
	}
}
