using StrategyGame.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StrategyGame.Settings {
	[CreateAssetMenu(fileName = "GameSettingsSo", menuName = "Settings/GameSettingsSo", order = 1)]
	public class GameSettingsSo : ScriptableObject {
		[SerializeField] private LayerMask soldierLayer;
		[SerializeField] List<BuildingBaseSo> buildingsList;

		public LayerMask SoldierLayer => soldierLayer;
		public List<BuildingBaseSo> BuildingsList => buildingsList;
	}
}
