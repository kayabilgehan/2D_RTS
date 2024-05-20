using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Soldiers {
	public interface IAttack {
		void Attack(Transform target);
	}
}