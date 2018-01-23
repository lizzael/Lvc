using Lvc.Performance.Core.Utils;
using Lvc.Performance.Utils;
using Lvc.Tests;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.Utils
{
	public class PositionTests
	{
		#region ctor Position(x, y)

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(0, -1)]
		[InlineData(-1, -0)]
		[InlineData(-1, -1)]
		[InlineData(int.MinValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		public void Position(int x, int y)
		{
			// Arrange

			// Act
			var sut = new Position(x, y);

			// Assert
			Assert.Equal(x, sut.X);
			Assert.Equal(y, sut.Y);
		}

		#endregion ctor Position(x, y)

		#region Equal_GetHashCode_ToString

		[Theory]
		[InlineData(0, 0, 0, 0, true)]
		[InlineData(0, 0, 1, 0, false)]
		[InlineData(0, 0, 0, 1, false)]
		[InlineData(0, 0, 1, 1, false)]
		public void Equal_GetHashCode_ToString(int x1, int y1, int x2, int y2, bool expectedResult)
		{
			// Arrange
			var p1 = new Position(x1, y1);
			var p2 = new Position(x2, y2);

			CommonTest.TestOverridedObjectMethods(expectedResult, p1, p2);
		}

		[Theory]
		[InlineData(0, 0, 0, 0, true)]
		[InlineData(0, 0, 1, 0, false)]
		[InlineData(0, 0, 0, 1, false)]
		[InlineData(0, 0, 1, 1, false)]
		public void EqualityOperators(int x1, int y1, int x2, int y2, bool expectedResult)
		{
			// Arrange
			var p1 = new Position(x1, y1);
			var p2 = new Position(x2, y2);

			// Act and Assert
			Assert.Equal(expectedResult, p1 == p2);
			Assert.Equal(!expectedResult, p1 != p2);

			Assert.Equal(expectedResult, p2 == p1);
			Assert.Equal(!expectedResult, p2 != p1);
		}

		#endregion Equal_GetHashCode_ToString

		#region Add and +

		[Theory]
		[InlineData(0, 0, 0, 0, 0, 0)]
		[InlineData(1, 0, 0, 0, 1, 0)]
		[InlineData(0, 1, 0, 0, 0, 1)]
		[InlineData(1, 1, 0, 0, 1, 1)]
		[InlineData(1, 1, -1, -1, 0, 0)]
		[InlineData(2, 3, 4, 5, 6, 8)]
		[InlineData(-20, 30, 40, -50, 20, -20)]
		public void Add(int x1, int y1, int x2, int y2, int eX, int eY)
		{
			// Arrange
			IPosition expectedResult = new Position(eX, eY);

			var p1 = new Position(x1, y1);
			var p2 = new Position(x2, y2);

			// Act
			var results = new[] {
				p1.Add(p2),
				p2.Add(p1),
				p1 + p2,
				p2 + p1
			};

			// Assert
			Assert.True(results.All(a => a.Equals(expectedResult)));
		}

		#endregion Add and +

		#region Unary -

		[Theory]
		[InlineData(0, 0, 0, 0)]
		[InlineData(1, 0, -1, 0)]
		[InlineData(0, 1, 0, -1)]
		[InlineData(1, 1, -1, -1)]
		[InlineData(2, 3, -2, -3)]
		[InlineData(-20, 30, 20, -30)]
		public void UnaryMinus(int x, int y, int eX, int eY)
		{
			// Arrange
			IPosition expectedResult = new Position(eX, eY);

			var p = new Position(x, y);

			// Act
			var result = -p;

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion Unary -

		#region Sub and -

		[Theory]
		[InlineData(0, 0, 0, 0, 0, 0)]
		[InlineData(1, 0, 0, 0, 1, 0)]
		[InlineData(0, 1, 0, 0, 0, 1)]
		[InlineData(1, 1, 0, 0, 1, 1)]
		[InlineData(1, 1, -1, -1, 2, 2)]
		[InlineData(2, 3, 4, 5, -2, -2)]
		[InlineData(-20, 30, 40, -50, -60, 80)]
		public void Sub(int x1, int y1, int x2, int y2, int eX, int eY)
		{
			// Arrange
			IPosition expectedResult = new Position(eX, eY);

			var p1 = new Position(x1, y1);
			var p2 = new Position(x2, y2);

			// Act
			var results = new[] {
				p1.Sub(p2),
				p2.Sub(p1).Mult(-1),
				p1 - p2,
				-(p2 - p1),
			};

			// Assert
			Assert.True(results.All(a => a.Equals(expectedResult)));
		}

		#endregion Sub and -

		#region Mult and *

		[Theory]
		[InlineData(0, 0, 0, 0, 0)]
		[InlineData(1, 0, 0, 0, 0)]
		[InlineData(0, 1, 0, 0, 0)]
		[InlineData(1, 1, 0, 0, 0)]
		[InlineData(123, -987, 0, 0, 0)]
		[InlineData(1, 1, 1, 1, 1)]
		[InlineData(2, -3, 1, 2, -3)]
		[InlineData(2, 3, -1, -2, -3)]
		[InlineData(-2, -3, -1, 2, 3)]
		[InlineData(-2, 3, 10, -20, 30)]
		[InlineData(-2, 3, -10, 20, -30)]
		public void Mult(int x, int y, int m, int eX, int eY)
		{
			// Arrange
			IPosition expectedResult = new Position(eX, eY);

			var p = new Position(x, y);

			// Act
			var results = new[] {
				p.Mult(m),
				p * m,
				m * p,
			};

			// Assert
			Assert.True(results.All(a => a.Equals(expectedResult)));
		}

		#endregion Mult and *

		#region Div and /

		[Theory]
		[InlineData(0, 0, 1, 0, 0)]
		[InlineData(1, 1, 1, 1, 1)]
		[InlineData(2, -3, 1, 2, -3)]
		[InlineData(2, 3, -1, -2, -3)]
		[InlineData(-2, -3, -1, 2, 3)]
		[InlineData(-2, 4, 2, -1, 2)]
		[InlineData(-2, 4, -2, 1, -2)]
		[InlineData(3, 5, 2, 1, 2)]
		public void Div(int x, int y, int m, int eX, int eY)
		{
			// Arrange
			IPosition expectedResult = new Position(eX, eY);

			var p = new Position(x, y);

			// Act
			var results = new[] {
				p.Div(m),
				p / m,
			};

			// Assert
			Assert.True(results.All(a => a.Equals(expectedResult)));
		}

		#endregion Div and /

		#region IsInside

		[Theory]
		[InlineData(0, 0, 0, 0, 0, 0, true)]
		[InlineData(0, 0, -1, -1, 0, 0, true)]
		[InlineData(0, 0, 0, 0, 1, 1, true)]
		[InlineData(0, 0, -1, -1, 1, 1, true)]
		[InlineData(1, 0, 0, 0, 0, 0, false)]
		[InlineData(0, -1, 0, 0, 0, 0, false)]
		[InlineData(1, 0, -1, -1, 0, 0, false)]
		[InlineData(0, -1, 0, 0, 1, 1, false)]
		[InlineData(-1, -2, -1, -1, 1, 1, false)]
		[InlineData(2, 1, -1, -1, 1, 1, false)]
		public void MyTheoryName(int x, int y, int minX, int minY, int maxX, int maxY, bool expectedResult)
		{
			// Arrange
			var sut = new Position(x, y);

			// Act
			var result = sut.IsInside(minX, minY, maxX, maxY);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion IsInside
	}
}
