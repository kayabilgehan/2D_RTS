using StrategyGame.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyAndSoldiersVisualizerUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtEnergy;
	[SerializeField] private TextMeshProUGUI txtSoldiers;

	private void Start() {
		SoldiersManager.Instance.soldierCountChanged += SoldiersManager_soldierCountChanged;
		EnergyManager.Instance.energyAmountChanged += EnergyManager_energyAmountChanged;
	}
	private void EnergyManager_energyAmountChanged(int energyAmount) {
		SetEnergyAmount(energyAmount);
	}

	private void SoldiersManager_soldierCountChanged(int soldierCount) {
		SetSoldierAmount(soldierCount);
	}

	void SetEnergyAmount(int energyAmount) {
		txtEnergy.text = energyAmount.ToString();
	}
	void SetSoldierAmount(int soldierCount) {
		txtSoldiers.text = soldierCount.ToString();
	}
}
