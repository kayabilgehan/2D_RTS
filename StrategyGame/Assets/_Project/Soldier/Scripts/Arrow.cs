using JetBrains.Annotations;
using StrategyGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StrategyGame.Soldiers {
	public class Arrow : MonoBehaviour {
		private float speed = 10f;
		private float damage = 15f;
		private Health targetHealth;
		private Transform startPosition;

		public void Initialize(Health targetHealth, Transform startPosition, float speed, float damage) {
			this.targetHealth = targetHealth;
			this.speed = speed;
			this.damage = damage;
			this.startPosition = startPosition;
			transform.position = this.startPosition.position;
		}

		void Update() {
			if (targetHealth != null) {
				Vector3 direction = (targetHealth.gameObject.transform.position - transform.position).normalized;
				transform.position += direction * speed * Time.deltaTime;

				if (Vector3.Distance(transform.position, targetHealth.gameObject.transform.position) < 0.1f) {
					HitTarget();
				}
			}
			else {
				gameObject.SetActive(false);
			}
		}
		void HitTarget() {
			if (targetHealth != null) {
				targetHealth.TakeDamage(damage);
			}
			GameController.Instance.ArrowPool.ReturnObject(gameObject);
		}
	}
}