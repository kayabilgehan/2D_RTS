using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame.AStar {
	public class AStarGrid : MonoBehaviour {
		[SerializeField] private Grid aStarGrid;
		[SerializeField] private Tilemap walkableTile;
		[SerializeField] private Tilemap buildingsTile;
		[SerializeField] private TileBase emptyTileBase;
		[SerializeField] private int maxSteps = 1000;

		private Vector3Int[] directions = new Vector3Int[] {
			new Vector3Int(1, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(-1, 0, 0),
			new Vector3Int(0, -1, 0)};

		public Vector3Int[,] spots;

		private BoundsInt bounds;
		private Astar aStar;


		private Astar AStar => aStar;

		float nodeDiameter;
		public int gridSizeX, gridSizeY;

		private void Start() {
			walkableTile.CompressBounds();
			buildingsTile.CompressBounds();
			bounds = walkableTile.cellBounds;
			
		}

		void CreateGrid() {
			spots = new Vector3Int[bounds.size.x, bounds.size.y];
			for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++) {
				for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++) {
					Vector3Int myGridPos = new Vector3Int(x, y, 0);

					TileBase myTile = buildingsTile.GetTile(myGridPos);
					if (myTile == null) {
						spots[i, j] = new Vector3Int(x, y, 0);
					}
					else {
						spots[i, j] = new Vector3Int(x, y, 1);
					}
				}
			}

		}
		public List<Vector3> GetPath(Vector3 startPosition, Vector3 endPosition) {
			CreateGrid();
			aStar = new Astar(spots, bounds.size.x, bounds.size.y);

			Vector2Int startCell = ToVector2Int(walkableTile.WorldToCell(startPosition));
			Vector2Int endCell = ToVector2Int(walkableTile.WorldToCell(endPosition));
			List<Spot> pathSpots = aStar.CreatePath(spots, startCell, endCell, maxSteps);

			List<Vector3> path = new List<Vector3>();
			//List<Spot> pathSpots = aStar.CreatePath(GetAllGridCellsCombined(walkableTile, buildingsTile), ToVector2Int(buildingsTile.WorldToCell(startPosition)), ToVector2Int(buildingsTile.WorldToCell(endPosition)), 1000);

			if (pathSpots != null) {
				foreach (Spot spot in pathSpots) {
					path.Add(buildingsTile.GetCellCenterWorld(new Vector3Int(spot.X, spot.Y, 0)));
				}
			}
			return path;
		}

		/*public List<Vector3> GetPath(Vector3 startPosition, Vector3 endPosition) {
			List<Vector3> path = new List<Vector3>();
			List<Spot> pathSpots = aStar.CreatePath(GetAllGridCellsCombined(walkableTile, buildingsTile), ToVector2Int(buildingsTile.WorldToCell(startPosition)), ToVector2Int(buildingsTile.WorldToCell(endPosition)), 1000);
			if (pathSpots != null) {
				foreach (Spot spot in pathSpots) {
					path.Add(buildingsTile.GetCellCenterWorld(new Vector3Int(spot.X, spot.Y, 0)));
				}
			}
			return path;
		}
		
		Vector3Int[,] GetAllGridCellsCombined(Tilemap tilemap, Tilemap blockingTilemap) {
			// Get the bounds of the tilemap
			BoundsInt bounds = tilemap.cellBounds;
			Vector3Int min = bounds.min;
			Vector3Int max = bounds.max;

			// Create a 2D array with the size of the bounds
			Vector3Int[,] gridCells = new Vector3Int[max.x - min.x, max.y - min.y];

			// Loop through the bounds and fill the array
			for (int x = min.x; x < max.x; x++) {
				for (int y = min.y; y < max.y; y++) {
					if (blockingTilemap.GetTile(new Vector3Int(x, y, 0)) == null) {
						gridCells[x - min.x, y - min.y] = new Vector3Int(x, y, 0);
					}
				}
			}

			return gridCells;
		}*/

		Vector2Int ToVector2Int(Vector3Int v) {
			return new Vector2Int(v.x, v.y);
		}
		public Rect MapLimits() {
			return new Rect(walkableTile.localBounds.center - walkableTile.localBounds.extents, walkableTile.localBounds.size);
		}

		#region Get cell position from world position
		public Vector3 GetCellCenterPosition(Vector3 position) {
			Vector3Int gridPos = walkableTile.WorldToCell(position);
			Vector3 worldPositionOfGridCenter = walkableTile.GetCellCenterWorld(gridPos);
			return worldPositionOfGridCenter;
		}
		public Vector3 GetCellTopRightPosition(Vector3 position) {
			Vector3Int gridPos = walkableTile.WorldToCell(position);
			Vector3 worldPositionOfGrid = walkableTile.CellToWorld(gridPos) + walkableTile.cellSize;
			return worldPositionOfGrid;
		}
		#endregion Get cell position from world position

		#region add-remove building tile objects
		public List<Vector3Int> AddObjectToTile(Vector3 rightBottomPosition, Vector2Int size) {
			Vector3Int startPosition = buildingsTile.WorldToCell(rightBottomPosition);
			List<Vector3Int> filledTiles = new List<Vector3Int>();
			for (int x = 0; x < size.x; x++) {
				for (int y = 0; y < size.y; y++) {
					Vector3Int cellPosition = new Vector3Int(startPosition.x + x, startPosition.y + y, 0);
					filledTiles.Add(cellPosition);
					buildingsTile.SetTile(cellPosition, emptyTileBase);
				}
			}
			return filledTiles;
		}
		public void RemoveTileObjects(List<Vector3Int> tilesToRemove) {
			foreach (Vector3Int tile in tilesToRemove)
				buildingsTile.SetTile(tile, null);
		}
		#endregion add-remove building tile objects

		public Vector3? FindClosestEmptyCell(Vector3 position) {
			Vector3Int startPosition = buildingsTile.WorldToCell(position);
			HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
			Queue<Vector3Int> queue = new Queue<Vector3Int>();

			queue.Enqueue(startPosition);
			visited.Add(startPosition);

			while (queue.Count > 0) {
				Vector3Int current = queue.Dequeue();

				if (buildingsTile.GetTile(current) == null) {
					return buildingsTile.CellToWorld(current); ; // Found an empty cell
				}

				foreach (Vector3Int direction in directions) {
					Vector3Int neighbor = current + direction;

					if (!visited.Contains(neighbor)) {
						visited.Add(neighbor);
						queue.Enqueue(neighbor);
					}
				}
			}

			return null; // No empty cells found
		}

		#region control if cells are empty
		public bool AreBuildingCellsEmpty(Vector3 startPosition, Vector2Int size) {
			return AreCellsEmpty(buildingsTile, buildingsTile.WorldToCell(startPosition), size);
		}
		bool AreCellsEmpty(Tilemap tilemap, Vector3Int startPosition, Vector2Int size) {
			for (int x = 0; x < size.x; x++) {
				for (int y = 0; y < size.y; y++) {
					Vector3Int cellPosition = new Vector3Int(startPosition.x + x, startPosition.y + y, 0);
					if (tilemap.GetTile(cellPosition) != null) {
						return false;
					}
				}
			}
			return true;
		}
		#endregion control if cells are empty
	}
}

