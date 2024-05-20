using System;
using System.Collections.Generic;
using UnityEngine;


namespace StrategyGame.AStar {
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using UnityEngine;

	public class Astar {
		public Spot[,] Spots;

		private int columns = 0;
		private int rows = 0;

		public Astar(Vector3Int[,] grid, int columns, int rows) {
			this.columns = columns;
			this.rows = rows;
			Spots = new Spot[columns, rows];
		}
		private bool IsValidPath(Vector3Int[,] grid, Spot start, Spot end) {
			if (end == null)
				return false;
			if (start == null)
				return false;
			if (end.Height >= 1)
				return false;
			return true;
		}
		public List<Spot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length) {
			Spot End = null;
			Spot Start = null;
			var columns = Spots.GetUpperBound(0) + 1;
			var rows = Spots.GetUpperBound(1) + 1;
			Spots = new Spot[columns, rows];

			for (int i = 0; i < columns; i++) {
				for (int j = 0; j < rows; j++) {
					Spots[i, j] = new Spot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
				}
			}

			for (int i = 0; i < columns; i++) {
				for (int j = 0; j < rows; j++) {
					Spots[i, j].AddNeighboors(Spots, i, j, columns, rows);
					if (Spots[i, j].X == start.x && Spots[i, j].Y == start.y)
						Start = Spots[i, j];
					else if (Spots[i, j].X == end.x && Spots[i, j].Y == end.y)
						End = Spots[i, j];
				}
			}
			if (!IsValidPath(grid, Start, End))
				return null;
			List<Spot> OpenSet = new List<Spot>();
			List<Spot> ClosedSet = new List<Spot>();

			OpenSet.Add(Start);

			while (OpenSet.Count > 0) {
				int winner = 0;
				for (int i = 0; i < OpenSet.Count; i++)
					if (OpenSet[i].F < OpenSet[winner].F)
						winner = i;
					else if (OpenSet[i].F == OpenSet[winner].F)
						if (OpenSet[i].H < OpenSet[winner].H)
							winner = i;

				var current = OpenSet[winner];

				if (End != null && OpenSet[winner] == End) {
					List<Spot> Path = new List<Spot>();
					var temp = current;
					Path.Add(temp);
					while (temp.previous != null) {
						Path.Add(temp.previous);
						temp = temp.previous;
					}
					if (length - (Path.Count - 1) < 0) {
						Path.RemoveRange(0, (Path.Count - 1) - length);
					}
					return Path;
				}

				OpenSet.Remove(current);
				ClosedSet.Add(current);

				var neighboors = current.Neighboors;
				for (int i = 0; i < neighboors.Count; i++)
				{
					var n = neighboors[i];
					if (!ClosedSet.Contains(n) && n.Height < 1)
					{
						var tempG = current.G + 1;
						bool newPath = false;
						if (OpenSet.Contains(n))
						{
							if (tempG < n.G)
							{
								n.G = tempG;
								newPath = true;
							}
						}
						else
						{
							n.G = tempG;
							newPath = true;
							OpenSet.Add(n);
						}
						if (newPath)
						{
							n.H = HeuristicCost(n, End);
							n.F = n.G + n.H;
							n.previous = current;
						}
					}
				}
			}
			return null;
		}

		private int HeuristicCost(Spot a, Spot b) {
			var dx = Math.Abs(a.X - b.X);
			var dy = Math.Abs(a.Y - b.Y);
			return 1 * (dx + dy);
		}
	}
	public class Spot {
		public int X;
		public int Y;
		public int F;
		public int G;
		public int H;
		public int Height = 0;
		public List<Spot> Neighboors;
		public Spot previous = null;
		public Spot(int x, int y, int height) {
			X = x;
			Y = y;
			F = 0;
			G = 0;
			H = 0;
			Neighboors = new List<Spot>();
			Height = height;
		}
		public void AddNeighboors(Spot[,] grid, int x, int y, int columns, int rows) {
			if (x < grid.GetUpperBound(0))
				Neighboors.Add(grid[x + 1, y]);
			if (x > 0)
				Neighboors.Add(grid[x - 1, y]);
			if (y < grid.GetUpperBound(1))
				Neighboors.Add(grid[x, y + 1]);
			if (y > 0)
				Neighboors.Add(grid[x, y - 1]);
			#region diagonal
			if (x > 0 && y > 0)
				Neighboors.Add(grid[x - 1, y - 1]);
			if (x < columns - 1 && y > 0)
				Neighboors.Add(grid[x + 1, y - 1]);
			if (x > 0 && y < rows - 1)
				Neighboors.Add(grid[x - 1, y + 1]);
			if (x < columns - 1 && y < rows - 1)
				Neighboors.Add(grid[x + 1, y + 1]);
			#endregion
		}
	}
}