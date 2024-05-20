using System.Collections.Generic;
using UnityEngine;
using StrategyGame.Utils;
using StrategyGame.Settings;
using VContainer;
using StrategyGame.Soldiers;
using StrategyGame.AStar;
using StrategyGame.Buildings;
using StrategyGame.UI;
using UnityEngine.EventSystems;

namespace StrategyGame.Core {
	public class GameController : MonoBehaviour {
		[SerializeField] private Transform selectionArea;
		[SerializeField] private AStarGrid aStarGrid;
		[SerializeField] private ObjectPool arrowPool;
		[SerializeField] private SoldiersManager soldiersManager;
		[SerializeField] private EnergyManager energyManager;

		private Vector3 startPosition;
		private List<Soldier> selectedSoldiers;
		private GameSettingsSo gameSettings;
		private bool isSelecting = false;

		#region Singleton - Game Controller
		private static GameController _instance;
		public static GameController Instance => _instance;
		#endregion Singleton - Game Controller

		public List<Soldier> activeSoldiers = new List<Soldier>();
		public List<BuildingManager> constructedBuildings = new List<BuildingManager>();
		public AStarGrid AStarGrid => aStarGrid;
		public ObjectPool ArrowPool => arrowPool;
		public void MoveSoldiers() {
			Vector3 moveToPosition = GeneralUtils.GetMouseWorldPosition();

			List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 1.5f, 2f }, new int[] { 5, 10, 20 });

			int targetPositionListIndex = 0;

			foreach (Soldier soldier in selectedSoldiers) {
				targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;

				Vector3? targetPosition = aStarGrid.FindClosestEmptyCell(targetPositionList[targetPositionListIndex]);
				List<Vector3> pathPositions = aStarGrid.GetPath((Vector3)targetPosition, soldier.gameObject.transform.position);

				soldier.MoveTo(pathPositions);
			}
		}

		#region Buildings
		public void AddBuilding(BuildingManager constructedBuilding) {
			if (!constructedBuildings.Contains(constructedBuilding)) {
				constructedBuildings.Add(constructedBuilding);
			}
		}
		public void RemoveBuilding(BuildingManager destructedBuilding) {
			if (constructedBuildings.Contains(destructedBuilding)) {
				constructedBuildings.Remove(destructedBuilding);
			}
		}
		public void ClearBuildingSelections() {
			UnitCreationGridController.Instance.ClearGrid();
			foreach (BuildingManager buildingManager in constructedBuildings) {
				buildingManager.BuildingSelection(false);
			}
		}
		#endregion Buildings


		private void DrawSelectionArea() {
			Vector3 currentMousePosition = GeneralUtils.GetMouseWorldPosition();
			Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x),
				Mathf.Min(startPosition.y, currentMousePosition.y));
			Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x),
				Mathf.Max(startPosition.y, currentMousePosition.y));
			selectionArea.position = lowerLeft;
			selectionArea.localScale = upperRight - lowerLeft;
			isSelecting = true;
		}
		private void StartSelection() {
			selectionArea.gameObject.SetActive(true);
			startPosition = GeneralUtils.GetMouseWorldPosition();
			isSelecting = true;
		}
		private void EndSelection() {
			selectionArea.gameObject.SetActive(false);
			DetectSelectedSoldiers();
			SelectSoldiers();
			isSelecting = false;
		}
		private void SelectSoldiers() {
			Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, GeneralUtils.GetMouseWorldPosition(), gameSettings.SoldierLayer);
			foreach (var collider2D in collider2DArray) {
				Soldier selectedSoldier = collider2D.gameObject.GetComponent<Soldier>();
				if (selectedSoldier.Health.IsAlive()) {
					selectedSoldier.SetSelectedVisible(true);
					selectedSoldiers.Add(selectedSoldier);
				}
			}
		}
		private void DetectSelectedSoldiers() {
			foreach (Soldier soldier in selectedSoldiers) {
				soldier.SetSelectedVisible(false);
			}

			selectedSoldiers.Clear();
		}
		
		private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray) {
			List<Vector3> positionList = new List<Vector3>();
			positionList.Add(startPosition);
			for (int i = 0; i < ringDistanceArray.Length; i++) {
				positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
			}
			return positionList;
		}
		private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount) {
			List<Vector3> positionList = new List<Vector3>();
			for (int i = 0; i < positionCount; i++) {
				float angle = i * (360f / positionCount);
				Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
				Vector3 position = startPosition + dir * distance;
				positionList.Add(position);
			}
			return positionList;
		}
		private Vector3 ApplyRotationToVector(Vector3 vec, float angle) {
			return Quaternion.Euler(0, 0, angle) * vec;
		}

		private void Awake() {
			if (_instance == null) {
				_instance = this;
			}

			CustomizeIgnoreLayers();

			selectedSoldiers = new List<Soldier>();
			selectionArea.gameObject.SetActive(false);
		}
		bool IsMouseOverUI() {
			return EventSystem.current.IsPointerOverGameObject();
		}
		bool ControlIfClickedForAttacking() {
			Vector3 mouseWorldPos = GeneralUtils.GetMouseWorldPosition();
			Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

			RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero);

			if (hit.collider != null) {
				foreach (Soldier soldier in selectedSoldiers) {
					soldier.Attack(hit.collider.gameObject.transform);
				}
				return true;
			}
			return false;
		}
		void Update() {
			bool isMouseOver = IsMouseOverUI();
			if (!isMouseOver && GameInput.Instance.LeftMouseDown) {
				ClearBuildingSelections();
				StartSelection();
			}
			if (GameInput.Instance.LeftMouseHold) {
				DrawSelectionArea();
			}
			if ((GameInput.Instance.LeftMouseUp)
				|| (isMouseOver && isSelecting && !GameInput.Instance.LeftMouseHold)) {
				EndSelection();
			}
			if (GameInput.Instance.RightMouseHold) {
				if (!ControlIfClickedForAttacking()) {
					MoveSoldiers();
				}
			}
		}
		void CustomizeIgnoreLayers() {
			Physics2D.IgnoreLayerCollision(Layers.Soldier, Layers.Soldier);
		}
		[Inject]
		void Construct(GameSettingsSo gameSettings) {
			this.gameSettings = gameSettings;
		}
	}
}