using StrategyGame.Soldiers;
using UnityEngine;

namespace StrategyGame.Buildings {
	[CreateAssetMenu(fileName = "BarracksSettingsSo", menuName = "Buildings/BarracksSettingsSo", order = 1)]
	public class BarracksSettingsSo : BuildingBaseSo {
		[SerializeField] Soldier[] units;

		public Soldier[] Units => units;
	}
}