using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Soldiers {
	public interface IMovement {
		void MoveTo(List<Vector3> path, Action reachedDestination);
	}
}