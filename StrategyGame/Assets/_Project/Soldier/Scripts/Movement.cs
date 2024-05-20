using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace StrategyGame.Soldiers {
	public class Movement : MonoBehaviour, IMovement {
		[SerializeField] private Soldier soldier;

		private IVelocity velocity;
		private List<Vector3> path = new List<Vector3>();
		private int pathPosition = 0;
		private Vector3 movePosition;
		private Action reachedDestination;
		private bool wasMoving = false;
		private Vector3 initialScale;
		private Vector3 reversedScale;

		private void Awake() {
			velocity = GetComponent<IVelocity>();
			velocity.Init(soldier.SoldierSetting.Speed);
			movePosition = transform.position;
			initialScale = soldier.gameObject.transform.localScale;
			reversedScale = initialScale;
			reversedScale.x *= -1;
		}
		public void MoveTo(List<Vector3> path, Action reachedDestination) {
			this.reachedDestination = reachedDestination;
			pathPosition = 0;
			this.path = path;
			if (path.Count > 0) {
				movePosition = path[pathPosition];
			}
		}

		private void Update() {
			Vector3 moveDir = (movePosition - transform.position).normalized;
			if (moveDir.x >= 0) {
				soldier.gameObject.transform.localScale = initialScale;
			}
			else { 
				soldier.gameObject.transform.localScale = reversedScale; 
			}
			if (Vector3.Distance(movePosition, transform.position) < 0.1f) {
				if (path.Count > 0 && pathPosition < path.Count - 1) {
					pathPosition++;
					movePosition = path[pathPosition];
					wasMoving = true;
				}
				else {
					moveDir = Vector3.zero;
					if (wasMoving) {
						reachedDestination?.Invoke();
						wasMoving = false;
					}
				}
			}
			velocity.SetVelocity(moveDir);
		}
	}
}