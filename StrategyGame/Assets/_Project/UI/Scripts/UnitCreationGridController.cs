using StrategyGame.Buildings;
using StrategyGame.Settings;
using StrategyGame.Soldiers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace StrategyGame.UI {
	public class UnitCreationGridController : MonoBehaviour {
		[SerializeField] private GridLayoutGroup unitsGrid;
		[SerializeField] private UnitCreateUIElement unitUIElementPrefab;
		[SerializeField] private TextMeshProUGUI txtBuildingName;
		[SerializeField] private Image imgBuilding;

		private static UnitCreationGridController _instance;
		private GameSettingsSo gameSettings;
		private BuildingManager buildingManager;
		private UnitCreatingBuildingManager unitCreatingBuildingManager;

		public static UnitCreationGridController Instance => _instance;

		public void Init(BuildingManager buildingManager) {
			ClearGrid();
			this.buildingManager = buildingManager;

			txtBuildingName.text = buildingManager.BuildingSetting.BuildingName;
			imgBuilding.sprite = buildingManager.BuildingSetting.BuildingImage;

			imgBuilding.enabled = true;
			txtBuildingName.enabled = true;

			if (buildingManager.BuildingType == BuildingTypes.barracks) {
				unitCreatingBuildingManager = buildingManager.gameObject.GetComponent<UnitCreatingBuildingManager>();
				FillUnitsGrid();
			}
		}

		public void ClearGrid() {
			unitCreatingBuildingManager = null;
			imgBuilding.enabled = false;
			txtBuildingName.enabled = false;
			for (int i = 0; i < unitsGrid.transform.childCount; i++) {
				Destroy(unitsGrid.transform.GetChild(i).gameObject);
			}
		}

		void FillUnitsGrid() {
			foreach (Soldier soldier in ((BarracksSettingsSo)buildingManager.BuildingSetting).Units) {
				UnitCreateUIElement unitCreationnUIElement = Instantiate(unitUIElementPrefab, unitsGrid.transform);
				unitCreationnUIElement.Init(soldier.SoldierSetting, unitCreatingBuildingManager);
			}
		}

		private void Awake() {
			_instance = this;
			ClearGrid();
		}
		[Inject]
		void Construct(GameSettingsSo gameSettings) {
			this.gameSettings = gameSettings;
		}
	}
}
