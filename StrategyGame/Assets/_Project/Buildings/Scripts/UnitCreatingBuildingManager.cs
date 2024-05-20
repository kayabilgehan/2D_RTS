using StrategyGame.Buildings;
using StrategyGame.Core;
using StrategyGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreatingBuildingManager : MonoBehaviour
{
    [SerializeField] private BuildingManager buildingManager;
	[SerializeField] private GameObject unitCreatingPoint;

	public BuildingManager BuildingManager => buildingManager;
	public GameObject UnitCreatingPoint => unitCreatingPoint;
}
