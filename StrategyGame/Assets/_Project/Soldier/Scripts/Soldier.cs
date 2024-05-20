using StrategyGame.AStar;
using StrategyGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StrategyGame.Soldiers {
	public class Soldier : MonoBehaviour, IAttack {
		[SerializeField] private GameObject selectedGameObject;
		[SerializeField] private SoldierSettingsSo soldierSetting;
		[SerializeField] private SoldierAnimations soldierAnimations;
		[SerializeField] private Health health;
		[SerializeField] private GameObject interactionPoint;

		private bool isAttacking = false;
		private Health targetHealth;
		private Transform target;
		private IMovement movement;

		public SoldierSettingsSo SoldierSetting => soldierSetting;
		public Health Health => health;

		public void SetSelectedVisible(bool visible) {
			selectedGameObject.SetActive(visible);
		}
		public void MoveTo(List<Vector3> path, Action reachedDestination = null) {
			if (!health.IsAlive()) { return; }
			StopAttacking();
			movement.MoveTo(path, reachedDestination);
		}
		public void Attack(Transform target) {
			if (!health.IsAlive() || target.Equals(gameObject.transform)) { return; }
			this.target = target;
			targetHealth = target.GetComponent<Health>();
			if (targetHealth == null) {
				targetHealth = target.GetComponentInParent<Health>();
			}
			isAttacking = true;
			List<Vector3> distancePath = GameController.Instance.AStarGrid.GetPath(
				 (Vector3)GameController.Instance.AStarGrid.FindClosestEmptyCell(target.position), gameObject.transform.position);
			if (distancePath.Count <= soldierSetting.Range) {
				Attack();
			}
			else {
				distancePath.RemoveRange(distancePath.Count - (int)soldierSetting.Range, (int)soldierSetting.Range);
				movement.MoveTo(distancePath, ReachedDestination);
			}
		}

		public void makeDamageToTarget() {
			if (targetHealth != null && targetHealth.IsAlive()) {
				if (soldierSetting.Range > 1) {
					GameObject arrowObj = GameController.Instance.ArrowPool.GetObject();
					Arrow arrow = arrowObj.GetComponent<Arrow>();
					arrow.Initialize(targetHealth, interactionPoint.transform, soldierSetting.ArrowSpeed, soldierSetting.Damage);
				}
				else {
					targetHealth.TakeDamage(soldierSetting.Damage);
				}
				
				if (!targetHealth.IsAlive()) {
					StopAttacking();
				}
			}
			else {
				StopAttacking();
			}
		}
		private void ReachedDestination() {
			if (isAttacking && target != null) {
				Attack();
			}
		}
		private void Awake() {
			movement = GetComponent<IMovement>();
			health.SetHealth(soldierSetting.Health, soldierSetting.Health);
			health.onDeathEvent += onDeathEvent;
		}
		private void onDeathEvent() {
			StartCoroutine(DeathEnumerator());
		}
		private IEnumerator DeathEnumerator() {
			SoldiersManager.Instance.RemoveSoldier(this);
			soldierAnimations.SetIsDead(true);
			yield return new WaitForSeconds(10);
			Destroy(gameObject);
		}
		private void StopAttacking() {
			isAttacking = false;
			target = null;
			targetHealth = null;
			soldierAnimations.SetIsAttacking(false);
		}
		private void Start() {
			selectedGameObject.SetActive(false);
		}
		private void Attack() {
			if (!health.IsAlive()) { return; }
			soldierAnimations.SetIsAttacking(true);
		}
		
	}
}