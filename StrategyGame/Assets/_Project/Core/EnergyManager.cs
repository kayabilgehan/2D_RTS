using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
	private static EnergyManager _instance;
	private int energyAmount = 0;

    public int EnergyAmount => energyAmount;
    public event Action<int> energyAmountChanged;
    public static EnergyManager Instance => _instance;

	public void AddEnergy(int amount) {
        energyAmount += amount;
		EnergyChanged();
	}
    public void RemoveEnergy(int amount) {
		energyAmount -= amount;
        EnergyChanged();
	}
    public bool IsEnergyAvailable(int amount) {
        return energyAmount >= amount;
    }
    private void EnergyChanged() {
        energyAmountChanged?.Invoke(EnergyAmount);
	}
	private void Awake() {
		_instance = this;
	}
}
