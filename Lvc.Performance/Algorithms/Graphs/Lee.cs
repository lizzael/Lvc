using Lvc.Performance.Core.Algorithms.Graphs;
using Lvc.Performance.Core.Utils;
using Lvc.Performance.Core.Utils.PositionMovements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Graphs
{
	public enum LeeEnum 
	{
		Obst = -1,
		Empt = 1,
		Star = 2,
		Exit = 4,
	}

	public class Lee : ILee
	{
		protected internal int[,] _mat;
		protected IMovements _movements;

		protected int CountOfY { get; set; }

		protected int LastY => CountOfY - 1;

		protected int CountOfX { get; set; }

		protected int LastX => CountOfX - 1;

		protected IPosition Start { get; set; }

		protected IPosition End { get; set; }

		public Lee(LeeEnum[,] tab, IMovements movements)
		{
			Validate.NotNull(tab, nameof(tab));
			Validate.NotNull(movements, nameof(movements));

			_movements = movements;
			CreateLeeMat(tab);
		}

		protected void CreateLeeMat(LeeEnum[,] tab)
		{
			CountOfY = tab.GetLength(0) + 2;
			CountOfX = tab.GetLength(1) + 2;
			_mat = new int[CountOfY, CountOfX];

			InitLeeMat();
			FillLeeMat(tab);

			if (Start == null)
				throw new ArgumentException("No start position found.");

			if (End == null)
				throw new ArgumentException("No end position found.");
		}

		private void FillLeeMat(LeeEnum[,] tab)
		{
			var rLength = tab.GetLength(0);
			var cLength = tab.GetLength(1);

			for (var r = 0; r < rLength; r++)
				for (var c = 0; c < cLength; c++)
					_mat[r + 1, c + 1] = ValueOf(tab, r, c);
		}

		protected void InitLeeMat()
		{
			InitBorderRows();
			InitBorderCols();
		}

		protected void InitBorderCols()
		{
			for (var c = 0; c < CountOfX; c++)
				_mat[0, c] = _mat[LastY, c] = (int)LeeEnum.Obst;
		}

		protected void InitBorderRows()
		{
			for (var r = 0; r < CountOfY; r++)
				_mat[r, 0] = _mat[r, LastX] = (int)LeeEnum.Obst;
		}

		protected int ValueOf(LeeEnum[,] tab, int r, int c)
		{
			switch (tab[r, c])
			{
				case LeeEnum.Star:
					SetStart(r + 1, c + 1);
					return LeeResult.Empty;

				case LeeEnum.Exit:
					SetEnd(r + 1, c + 1);
					return LeeResult.Empty;

				case LeeEnum.Empt:
					return LeeResult.Empty;

				case LeeEnum.Obst:
					return LeeResult.Obstacle;

				default:
					throw new ArgumentOutOfRangeException("This should never happend.");
			}
		}

		private void SetEnd(int r, int c)
		{
			if (End != null)
				throw new ArgumentException("More than one end found.");

			End = _movements.PositionFactory.Create(c, r);
		}

		private void SetStart(int r, int c)
		{
			if (Start != null)
				throw new ArgumentException("More than one start found.");

			Start = _movements.PositionFactory.Create(c, r);
		}

		public ILeeResult Execute()
		{
			var queue = new Queue<IPosition>(CountOfY * CountOfX);
			queue.Enqueue(Start);
			_mat[Start.Y, Start.X] = 0;

			while (_mat[End.Y, End.X] == LeeResult.Empty && queue.Any())
			{
				var pos = queue.Dequeue();
				var newValue = _mat[pos.Y, pos.X] + 1;
				foreach (var newPos in _movements.GetMovements(pos, 0, 0, LastX, LastY))
					if (_mat[newPos.Y, newPos.X] > newValue)
					{
						queue.Enqueue(newPos);
						_mat[newPos.Y, newPos.X] = newValue;
					}
			}

			return new LeeResult(_mat, Start, End, _movements);
		}

		#region LeeResult

		public class LeeResult : ILeeResult
		{
			public const int Obstacle = -1;
			public const int Empty = int.MaxValue - 1;

			protected int[,] _leeMat;

			public int CountOfY =>
				_leeMat.GetLength(0);

			public int LastY =>
				CountOfY - 1;

			public int CountOfXs =>
				_leeMat.GetLength(1);

			public int LastX =>
				CountOfXs - 1;

			public IMovements Movements { get; protected set; }

			protected internal LeeResult(
				int[,] leeMat, 
				IPosition start, IPosition end, 
				IMovements movements)
			{
				_leeMat = leeMat;

				Start = start;
				End = end;

				Movements = movements;
			}

			public IPosition Start { get; set; }

			public IPosition End { get; set; }

			public bool HasSolution =>
				_leeMat[End.Y, End.X] != Empty
				&& _leeMat[End.Y, End.X] != Obstacle;

			public int SolutionLength =>
					HasSolution ? _leeMat[End.Y, End.X] : -1;

			public IEnumerable<IPosition> GetOneRoute() 
			{
				Validate.NotValidState(!HasSolution, "There is no solution.");

				return FindOneRoute().Reverse();
			}

			protected IEnumerable<IPosition> FindOneRoute()
			{
				var pos = End;
				while (true)
				{
					yield return pos;
					if (pos.Equals(Start))
						break;

					pos = Next(pos);
				}
			}

			protected IPosition Next(IPosition pos)
			{
				var previousValue = _leeMat[pos.Y, pos.X] - 1;
				return Movements.GetMovements(pos, 0, 0, LastX, LastY)
					.First(p => _leeMat[p.Y, p.X] == previousValue);
			}
		}

		#endregion LeeResult
	}
}