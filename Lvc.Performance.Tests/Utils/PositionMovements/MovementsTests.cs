using Lvc.Performance.Core.Utils;
using Lvc.Performance.Utils;
using Lvc.Performance.Utils.PositionMovements;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.Utils.PositionMovements
{
	public class MovementsTests
	{
		#region ctor Movements(relativePositions, positionFactory)

		[Fact]
		public void Movements_GivenRelativePositionsNull_Throws_ArgumentNullException()
		{
			// Arrange
			IPosition[] relativesPosition = null;
			IPositionFactory positionFactory = new PositionFactory();

			// Act
			Action act = () => new Movements(relativesPosition, positionFactory);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Movements_GivenPositionFactoryNull_Throws_ArgumentNullException()
		{
			// Arrange
			IPosition[] relativesPosition = new IPosition[0];
			IPositionFactory positionFactory = null;

			// Act
			Action act = () => new Movements(relativesPosition, positionFactory);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Movements()
		{
			// Arrange
			IPosition[] relativesPosition = new IPosition[0];
			IPositionFactory positionFactory = new PositionFactory();

			// Act
			var sut = new Movements(relativesPosition, positionFactory);

			// Assert
			Assert.Equal(relativesPosition, sut.RelativePositions);
			Assert.Equal(positionFactory, sut.PositionFactory);
		}

		#endregion ctor Movements(relativePositions, positionFactory)

		#region GetMovements

		[Fact]
		public void GetMovements_X0Y0Unlimited_EmptyRelativePositions()
		{
			var expectedResult = new IPosition[0];
			var position = new Position(0, 0);
			var relativePositions = new IPosition[0];
			
			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X0Y0Unlimited_RowCol4Movements()
		{
			var expectedResult = new IPosition[] 
			{ 
				new Position(0, 1),
				new Position(1, 0),
				new Position(0, -1),
				new Position(-1, 0),
			};
			var position = new Position(0, 0);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 0),
				new Position(0, -1),
				new Position(-1, 0),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X0Y0Unlimited_X4Movements()
		{
			var expectedResult = new IPosition[]
			{
				new Position(1, 1),
				new Position(1, -1),
				new Position(-1, -1),
				new Position(-1, 1),
			};
			var position = new Position(0, 0);
			var relativePositions = new IPosition[]
			{
				new Position(1, 1),
				new Position(1, -1),
				new Position(-1, -1),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X0Y0Unlimited_8CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};
			var position = new Position(0, 0);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X2Y3Unlimited_EmptyRelativePositions()
		{
			var expectedResult = new IPosition[0];
			var position = new Position(2, 3);
			var relativePositions = new IPosition[0];

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X2Y3Unlimited_RowCol4Movements()
		{
			var expectedResult = new IPosition[]
			{
				new Position(2, 4),
				new Position(3, 3),
				new Position(2, 2),
				new Position(1, 3),
			};
			var position = new Position(2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 0),
				new Position(0, -1),
				new Position(-1, 0),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X2Y3Unlimited_X4Movements()
		{
			var expectedResult = new IPosition[]
			{
				new Position(3, 4),
				new Position(3, 2),
				new Position(1, 2),
				new Position(1, 4),
			};
			var position = new Position(2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(1, 1),
				new Position(1, -1),
				new Position(-1, -1),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X2Y3Unlimited_8CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(2, 4),
				new Position(3, 4),
				new Position(3, 3),
				new Position(3, 2),
				new Position(2, 2),
				new Position(1, 2),
				new Position(1, 3),
				new Position(1, 4),
			};
			var position = new Position(2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_Xn2Y3Unlimited_EmptyRelativePositions()
		{
			var expectedResult = new IPosition[0];
			var position = new Position(-2, 3);
			var relativePositions = new IPosition[0];

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_Xn2Y3Unlimited_RowCol4Movements()
		{
			var expectedResult = new IPosition[]
			{
				new Position(-2, 4),
				new Position(-1, 3),
				new Position(-2, 2),
				new Position(-3, 3),
			};
			var position = new Position(-2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 0),
				new Position(0, -1),
				new Position(-1, 0),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_Xn2Y3Unlimited_X4Movements()
		{
			var expectedResult = new IPosition[]
			{
				new Position(-1, 4),
				new Position(-1, 2),
				new Position(-3, 2),
				new Position(-3, 4),
			};
			var position = new Position(-2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(1, 1),
				new Position(1, -1),
				new Position(-1, -1),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_Xn2Y3Unlimited_8PossibleMovements_8CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(-2, 4),
				new Position(-1, 4),
				new Position(-1, 3),
				new Position(-1, 2),
				new Position(-2, 2),
				new Position(-3, 2),
				new Position(-3, 3),
				new Position(-3, 4),
			};
			var position = new Position(-2, 3);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions);
		}

		[Fact]
		public void GetMovements_X5Y5Limited4466_8PossibleMovements_3CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(5, 6),
				new Position(6, 6),
				new Position(6, 5),
				new Position(6, 4),
				new Position(5, 4),
				new Position(4, 4),
				new Position(4, 5),
				new Position(4, 6),
			};
			var position = new Position(5, 5);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions, 4, 4, 6, 6);
		}

		[Fact]
		public void GetMovements_X5Y5Limited5566_8PossibleMovements_3CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(5, 6),
				new Position(6, 6),
				new Position(6, 5),
			};
			var position = new Position(5, 5);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions, 5, 5, 6, 6);
		}

		[Fact]
		public void GetMovements_X5Y5Limited5465_8PossibleMovements_3CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(6, 5),
				new Position(6, 4),
				new Position(5, 4),
			};
			var position = new Position(5, 5);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions, 5, 4, 6, 5);
		}

		[Fact]
		public void GetMovements_X5Y5Limited4455_8PossibleMovements_3CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(5, 4),
				new Position(4, 4),
				new Position(4, 5),
			};
			var position = new Position(5, 5);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions, 4, 4, 5, 5);
		}

		[Fact]
		public void GetMovements_X5Y5Limited4556_8PossibleMovements_3CloseNeighboors()
		{
			var expectedResult = new IPosition[]
			{
				new Position(5, 6),
				new Position(4, 5),
				new Position(4, 6),
			};
			var position = new Position(5, 5);
			var relativePositions = new IPosition[]
			{
				new Position(0, 1),
				new Position(1, 1),
				new Position(1, 0),
				new Position(1, -1),
				new Position(0, -1),
				new Position(-1, -1),
				new Position(-1, 0),
				new Position(-1, 1),
			};

			GetMovements_Test(expectedResult, position, relativePositions, 4, 5, 5, 6);
		}

		private static void GetMovements_Test(
			IPosition[] expectedResult, 
			Position position, 
			IPosition[] relativePositions,
			int minX = int.MinValue, int minY = int.MinValue, 
			int maxX = int.MaxValue, int maxY = int.MaxValue)
		{
			// Arrange
			var positionFactory = new PositionFactory();

			var sut = new Movements(relativePositions, positionFactory);

			// Act
			var result = sut.GetMovements(position, minX, minY, maxX, maxY)
				.ToArray();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion GetMovements
	}
}
