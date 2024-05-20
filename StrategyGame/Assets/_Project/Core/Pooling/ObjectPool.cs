using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core {
	public class ObjectPool : MonoBehaviour {
		[SerializeField] private ObjectPoolingSettingsSo ObjectPoolingSettings;

		private Queue<GameObject> objectPool;

		void Awake() {
			objectPool = new Queue<GameObject>();

			for (int i = 0; i < ObjectPoolingSettings.PoolSize; i++) {
				GameObject obj = Instantiate(ObjectPoolingSettings.ObjectPrefab, gameObject.transform);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
		}

		public GameObject GetObject() {
			if (objectPool.Count > 0) {
				GameObject obj = objectPool.Dequeue();
				obj.SetActive(true);
				return obj;
			}
			else {
				GameObject obj = Instantiate(ObjectPoolingSettings.ObjectPrefab, gameObject.transform);
				obj.SetActive(true);
				return obj;
			}
		}

		public void ReturnObject(GameObject obj) {
			obj.SetActive(false);
			objectPool.Enqueue(obj);
		}
	}
}

