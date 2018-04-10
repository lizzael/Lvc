using Lvc;
using Lvc.Performance.Algorithms.HashCodes;
using Lvc.Performance.Core.Algorithms.HashCodes;
using Lvc.Performance.Core.Utils;
using System;

namespace Lvc.Performance.Utils
{
    public struct Position : IPosition, IEquatable<Position>
	{
		static readonly IHashCodeProvider IntsHashCodeProvider 
            = new HashCodeProvider();

		public int X { get; private set; }

		public int Y { get; private set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		#region Equals, GetHashCode, and ToString

		public override bool Equals(object obj) =>
			obj is Position
			? Equals((Position)obj)
			: false;

		public override int GetHashCode() =>
			IntsHashCodeProvider.GetHashCode(X, Y);

		public override string ToString() =>
			$"({X}, {Y})";

		#endregion Equals, GetHashCode, and ToString

		public bool Equals(Position other) =>
			other != null 
			&& X == other.X && Y == other.Y;

		public IPosition Add(IPosition pos) =>
			new Position(X + pos.X, Y + pos.Y);

		public IPosition Sub(IPosition pos) => 
			new Position(X - pos.X, Y - pos.Y);

		public IPosition Mult(int v) =>
			new Position(X * v, Y * v);

		public IPosition Div(int v)
		{
			Validate.NotValidValue(v, 0, nameof(v));

			return new Position(X / v, Y / v);
		}

		public bool IsInside(int minX, int minY, int maxX, int maxY) =>
			X >= minX && X <= maxX
			&& Y >= minY && Y <= maxY;

		public static bool operator ==(Position p1, Position p2) =>
			p1.Equals(p2);

		public static bool operator !=(Position p1, Position p2) =>
			!p1.Equals(p2);

		public static Position operator +(Position p1, Position p2) =>
			(Position)p1.Add(p2);

		public static Position operator -(Position p) =>
			(Position)p.Mult(-1);

		public static Position operator -(Position p1, Position p2) =>
			(Position)p1.Sub(p2);

		public static Position operator *(Position p, int v) =>
			(Position)p.Mult(v);

		public static Position operator *(int v, Position p) =>
			(Position)p.Mult(v);

		public static Position operator /(Position p, int v) =>
			(Position)p.Div(v);
	}
}
