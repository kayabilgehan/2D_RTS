using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimations : MonoBehaviour
{
	[SerializeField] private Animator animator;
	public void SetIsMoving(bool isMoving) {
		if (isMoving) { ResetAnimations(); }
		SetWalking(isMoving);
	}
	public void SetIsAttacking(bool isAttacking) {
		if (isAttacking) { ResetAnimations(); }
		SetAttacking(isAttacking);
	}
	public void SetIsDead(bool isDead) {
		if (isDead) { ResetAnimations(); }
		SetDead(isDead);
	}

	void ResetAnimations() {
		SetAttacking(false);
		SetWalking(false);
	}
	void SetWalking(bool isWalking) {
		animator.SetBool("walking", isWalking);
	}
	void SetAttacking(bool isAttacking) {
		animator.SetBool("attacking", isAttacking);
	}
	void SetDead(bool isDead) {
		animator.SetBool("dead", isDead);
	}
}
