using StrategyGame.AStar;
using StrategyGame.Core;
using StrategyGame.UI;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace StrategyGame.Buildings {
	public class BuildingManager : MonoBehaviour {
		[SerializeField] private SpriteRenderer buildingSelectionImage;
		[SerializeField] private SpriteRenderer buildingImage;
		[SerializeField] private BuildingBaseSo buildingSetting;
		[SerializeField] private BuildingTypes buildingType;
		[SerializeField] private BoxCollider2D collider;
		[SerializeField] private Health health;

		private bool isConstructed = false;
		private bool isSelected = false;
		private Vector2Int vector2Size;
		private List<Vector3Int> attachedTiles;
		private bool isColliding = false;

		public BuildingBaseSo BuildingSetting => buildingSetting;
		public bool IsConstructed => isConstructed;
		public bool IsSelected => isSelected;
		public BuildingTypes BuildingType => buildingType;

		public void Init(){
			buildingImage.sprite = this.buildingSetting.BuildingImage;
		}
		public void BuildingClicked() {
			GameController.Instance.ClearBuildingSelections();
			BuildingSelection(true);
			UnitCreationGridController.Instance.Init(this);
		}

		public void BuildingSelection(bool isSelected) {
			buildingSelectionImage.gameObject.SetActive(isSelected);
		}
		void RemoveBuilding() {
			GameController.Instance.AStarGrid.RemoveTileObjects(attachedTiles);
			GameController.Instance.RemoveBuilding(this);
			Destroy(this.gameObject);
		}

		private void ConstructBuilding() {
			Bounds boxBounds = collider.bounds;
			Vector3 bottomRight = new Vector3(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y, 0);

			attachedTiles = GameController.Instance.AStarGrid.AddObjectToTile(bottomRight, vector2Size);
			Init();
			isConstructed = true;
			GameController.Instance.AddBuilding(this);
			health.SetHealth(buildingSetting.Health, buildingSetting.Health);
			health.onDeathEvent += OnDestructionEvent;
		}

		private void OnDestructionEvent() {
			RemoveBuilding();
		}

		private void Start() {
			BuildingSelection(false);
			vector2Size = new Vector2Int((int)buildingSetting.Size.x, (int)buildingSetting.Size.y);
		}
		private void Update() {
			IsConstructableControl();
		}
		void IsConstructableControl() {
			if (!isConstructed) {
				gameObject.transform.position = GameController.Instance.AStarGrid.GetCellTopRightPosition(GeneralUtils.GetMouseWorldPosition());

				Bounds boxBounds = collider.bounds;
				Vector3 bottomRight = new Vector3(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y, 0);
				bool cellsAvailable = GameController.Instance.AStarGrid.AreBuildingCellsEmpty(bottomRight, vector2Size);
				if (cellsAvailable && !isColliding) {
					buildingImage.color = Color.white;
					if (GameInput.Instance.LeftMouseDown) {
						ConstructBuilding();
					}
				}
				else {
					buildingImage.color = new Color(0.5f, 0f, 0f);
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			isColliding = true;
		}
		private void OnTriggerStay2D(Collider2D collision) {
			isColliding = true;
		}
		private void OnTriggerExit2D(Collider2D collision) {
			isColliding = false;
		}
	}
}

