using StrategyGame.Buildings;
using StrategyGame.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace StrategyGame.UI {
	public class GameUIController : MonoBehaviour {
		[SerializeField] private GridLayoutGroup buildingsGrid;
		[SerializeField] private BuildingConstructionUIElement buildingUIElementPrefab;

		private GameSettingsSo gameSettings;

		void FillBuildingsGrid() {
			foreach (BuildingBaseSo building in gameSettings.BuildingsList) {
				BuildingConstructionUIElement buildingConstuctionUIElement = Instantiate(buildingUIElementPrefab, buildingsGrid.transform);
				buildingConstuctionUIElement.Init(building);
			}
		}

		void Start() {
			FillBuildingsGrid();
		}
		[Inject]
		void Construct(GameSettingsSo gameSettings) {
			this.gameSettings = gameSettings;
		}
	}
}