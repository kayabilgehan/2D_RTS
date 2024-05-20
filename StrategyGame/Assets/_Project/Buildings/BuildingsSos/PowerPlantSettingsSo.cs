using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Buildings {
	[CreateAssetMenu(fileName = "PowerPlantSettingsSo", menuName = "Buildings/PowerPlantSettingsSo", order = 1)]
	public class PowerPlantSettingsSo : BuildingBaseSo {
		[SerializeField] int powerGenerationPerSecond;

		public int PowerGenerationPerSecond => powerGenerationPerSecond;
	}
}
