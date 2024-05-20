using StrategyGame.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace StrategyGame.DI {
	public class SplashLifetimeScope : LifetimeScope {
		[SerializeField] private SplashSettingsSo splashSettings;
		[SerializeField] private GeneralSettingsSo generalSettings;

		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(splashSettings);
			builder.RegisterInstance(generalSettings);


			//builder.RegisterComponentInNewPrefab(GameplaySettingsManagerPrefab, Lifetime.Singleton).DontDestroyOnLoad();
			//builder.RegisterComponentOnNewGameObject<WwiseSoundManager>(Lifetime.Singleton, "WwiseSoundManager").DontDestroyOnLoad();
		}
	}
}

