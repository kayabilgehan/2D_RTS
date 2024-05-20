using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core {
	[CreateAssetMenu(fileName = "ObjectPoolingSettingsSo", menuName = "Settings/ObjectPoolingSettingsSo", order = 1)]
	public class ObjectPoolingSettingsSo : ScriptableObject {

		[SerializeField] private GameObject objectPrefab;
		[SerializeField] private float poolSize;

		public GameObject ObjectPrefab => objectPrefab;
		public float PoolSize => poolSize;
	}
}

