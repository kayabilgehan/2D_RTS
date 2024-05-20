using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Soldiers {
	public class MovementVelocity : MonoBehaviour, IVelocity {

		private float moveSpeed;
		private Vector3 velocityVector;
		private Rigidbody2D rigidbody2D;
		private SoldierAnimations soldierAnimations;

		public void Init(float moveSpeed) {
			this.moveSpeed = moveSpeed;
		}
		public void SetVelocity(Vector3 velocityVector) {
			this.velocityVector = velocityVector;
		}

		private void Awake() {
			rigidbody2D = GetComponent<Rigidbody2D>();
			soldierAnimations = GetComponent<SoldierAnimations>();
		}
		private void FixedUpdate() {
			rigidbody2D.velocity = velocityVector * moveSpeed;

			if (!rigidbody2D.velocity.Equals(Vector2.zero)) {
				soldierAnimations.SetIsMoving(true);
			}
			else {
				soldierAnimations.SetIsMoving(false);
			}
		}

		public void Disable() {
			this.enabled = false;
			rigidbody2D.velocity = Vector3.zero;
		}

		public void Enable() {
			this.enabled = true;
		}

		
	}
}