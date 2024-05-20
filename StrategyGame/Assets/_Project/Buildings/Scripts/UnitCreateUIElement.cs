using StrategyGame.Buildings;
using StrategyGame.Core;
using StrategyGame.Soldiers;
using StrategyGame.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreateUIElement : MonoBehaviour
{
	[SerializeField] private Image unitImage;
	[SerializeField] private TextMeshProUGUI unitName;
	[SerializeField] private TextMeshProUGUI unitCost;

	private SoldierSettingsSo soldierSettings;
	private UnitCreatingBuildingManager unitCreatingBuildingManager;

	public void Init(SoldierSettingsSo soldierSettings, UnitCreatingBuildingManager unitCreatingBuildingManager) {
		this.soldierSettings = soldierSettings;
		this.unitCreatingBuildingManager = unitCreatingBuildingManager;
		unitImage.sprite = soldierSettings.SoldierImage;
		unitName.text = soldierSettings.SoldierName;
		unitCost.text = soldierSettings.CreationEnergyCost.ToString();
	}

	public void CreateUnit() {
		if (EnergyManager.Instance.IsEnergyAvailable(soldierSettings.CreationEnergyCost)) {
			Soldier soldierUnit = Instantiate(soldierSettings.Prefab);
			soldierUnit.transform.position = unitCreatingBuildingManager.UnitCreatingPoint.transform.position;
			soldierUnit.MoveTo(new List<Vector3> { (Vector3)GameController.Instance.AStarGrid.FindClosestEmptyCell(unitCreatingBuildingManager.UnitCreatingPoint.transform.position) });

			EnergyManager.Instance.RemoveEnergy(soldierSettings.CreationEnergyCost);
			SoldiersManager.Instance.AddSoldier(soldierUnit);
		}
	}
}
