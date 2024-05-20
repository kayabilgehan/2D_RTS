using UnityEngine;

namespace StrategyGame.Soldiers {
	[CreateAssetMenu(fileName = "SoldierSettingsSo", menuName = "SoldierSettings/SoldierSettingsSo", order = 1)]
	public class SoldierSettingsSo : ScriptableObject {
		[SerializeField] private float speed;
		[SerializeField] private float damage;
		[SerializeField] private float health;
		[SerializeField] private float range;
		[SerializeField] private Vector2 size;
		[SerializeField] private Sprite soldierImage;
		[SerializeField] private string soldierName;
		[SerializeField] private Soldier prefab;
		[SerializeField] private float arrowSpeed;
		[SerializeField] private int creationEnergyCost;

		public float Speed => speed;
		public float Damage => damage;
		public float Health => health;
		public float Range => range;
		public Vector2 Size => size;
		public Sprite SoldierImage => soldierImage;
		public string SoldierName => soldierName;
		public Soldier Prefab => prefab;
		public float ArrowSpeed => arrowSpeed;
		public int CreationEnergyCost => creationEnergyCost;

	}
}

