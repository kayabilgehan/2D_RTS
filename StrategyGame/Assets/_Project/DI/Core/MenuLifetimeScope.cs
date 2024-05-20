using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace StrategyGame.DI {
    public class MenuLifetimeScope : LifetimeScope {
		[SerializeField] private GeneralSettingsSo generalSettings;
		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(generalSettings);
		}
    }
}
