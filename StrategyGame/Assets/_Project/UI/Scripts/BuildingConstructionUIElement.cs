using StrategyGame.Buildings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.UI {
	public class BuildingConstructionUIElement : MonoBehaviour {
		[SerializeField] private Image buildingImage;
		[SerializeField] private TextMeshProUGUI buildingName;

		private BuildingBaseSo buildingSettings;

		public void Init(BuildingBaseSo buildingSettings) {
			buildingImage.sprite = buildingSettings.BuildingImage;
			buildingName.text = buildingSettings.BuildingName;

			this.buildingSettings = buildingSettings;
		}

		public void InstantiateBuilding() {
			Instantiate(buildingSettings.Prefab);
		}
	}
}

