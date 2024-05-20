using StrategyGame.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace StrategyGame.DI {
    public class GameLifetimeScope : LifetimeScope {
		[SerializeField] private GameSettingsSo gameSettings;
		[SerializeField] private GeneralSettingsSo generalSettings;
		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(gameSettings);
			builder.RegisterInstance(generalSettings);
		}
    }
}
