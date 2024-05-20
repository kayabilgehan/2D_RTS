using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Soldiers {
	public interface IVelocity {
		void Init(float speed);
		void SetVelocity(Vector3 velocityVector);
		void Disable();
		void Enable();
	}
}