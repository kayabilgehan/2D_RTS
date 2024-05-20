using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.Core {
	public class Health : MonoBehaviour {
		[HideInInspector] public event Action onDeathEvent;

		[SerializeField] private Image healthBar;

		private float maxHealth = 0;
		private float currentHealth = 0;

		public void SetHealth(float healtAmount, float maxHealthAmount) {
			currentHealth = healtAmount;
			maxHealth = maxHealthAmount;
			HealthChanged();
		}
		public bool IsAlive() {
			return currentHealth > 0;
		}
		public void TakeDamage(float amount) {
			currentHealth -= amount;
			HealthChanged();

			if (!IsAlive()) {
				onDeathEvent?.Invoke();
			}
		}
		private void HealthChanged() {
			healthBar.fillAmount = currentHealth / maxHealth;
		}
	}
}

