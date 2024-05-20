using StrategyGame.Buildings;
using StrategyGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Buildings {
	public class PowerPlantBuildingManager : MonoBehaviour {
		[SerializeField] private BuildingManager buildingManager;
		private PowerPlantSettingsSo powerPlantSettings;
		private void Awake() {
			powerPlantSettings = (PowerPlantSettingsSo)buildingManager.BuildingSetting;
		}
		private void Start() {
			InvokeRepeating("GenerateEnergy", 1, 1);
		}
		private void GenerateEnergy() {
			EnergyManager.Instance.AddEnergy((int)powerPlantSettings.PowerGenerationPerSecond);
		}
	}
}

