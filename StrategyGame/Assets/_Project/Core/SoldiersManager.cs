using StrategyGame.Soldiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersManager : MonoBehaviour {
	private static SoldiersManager _instance;
	private List<Soldier> soldiersList = new List<Soldier>();

	public int SoldierCount => soldiersList.Count;
	public event Action<int> soldierCountChanged;
	public static SoldiersManager Instance => _instance;

	public void AddSoldier(Soldier soldier) {
		soldiersList.Add(soldier);
		SoldierCountChanged();
	}
	public void RemoveSoldier(Soldier soldier) {
		soldiersList.Remove(soldier);
		SoldierCountChanged();
	}
	private void SoldierCountChanged() {
		soldierCountChanged?.Invoke(SoldierCount);
	}
	private void Awake() {
		_instance = this;
	}
}
