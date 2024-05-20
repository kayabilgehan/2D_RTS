using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame.Buildings {
	[CreateAssetMenu(fileName = "BuildingBaseSo", menuName = "Buildings/BuildingBaseSo", order = 1)]
	public class BuildingBaseSo : ScriptableObject {
		[SerializeField] private Vector3Int size;
		[SerializeField] private GameObject prefab;
		[SerializeField] private float health;
		[SerializeField] private Sprite buildingImage;
		[SerializeField] private string buildingName;

		public Vector3 Size => size;
		public GameObject Prefab => prefab;
		public float Health => health;
		public Sprite BuildingImage => buildingImage;
		public string BuildingName => buildingName;
	}
}
