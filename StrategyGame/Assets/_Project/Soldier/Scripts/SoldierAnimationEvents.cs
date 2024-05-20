using StrategyGame.Soldiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimationEvents : MonoBehaviour
{
    [SerializeField] Soldier soldier;
    public void AttackAnimationEnded() {
        soldier.makeDamageToTarget();
    }
}
