using Lvc.Performance.Algorithms.Graphs;
using Lvc.Performance.Core.Utils;
using Lvc.Performance.Core.Utils.PositionMovements;
using Lvc.Performance.Utils;
using Lvc.Performance.Utils.PositionMovements;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.Algorithms.Graphs
{
	public class LeeTests
	{
		#region ctor Lee(tab)

		static readonly IPosition[] RelativePositions = new IPosition[] 
		{ 
			new Position(0, -1),
			new Position(1, 0),
			new Position(0, 1),
			new Position(-1, 0),
		};

		static readonly IPositionFactory PositionFactory = new PositionFactory();
		static readonly IMovements Movements = new Movements(RelativePositions, PositionFactory);

		[Fact]
		public void Lee_GivenNullTab_Throws_ArgumentNullException()
		{
			// Arrange
			LeeEnum[,] tab = null;

			// Act
			Action act = () => new Lee(tab, Movements);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Lee_GivenNullMovements_Throws_ArgumentNullException()
		{
			// Arrange
			LeeEnum[,] tab = null;

			// Act
			Action act = () => new Lee(tab, null);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Lee_GivenNoStart_Throws_ArgumentException()
		{
			// Arrange
			LeeEnum[,] tab = new[,] 
			{ 
				{ LeeEnum.Empt, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Exit },
			};

			// Act
			Action act = () => new Lee(tab, Movements);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		[Fact]
		public void Lee_GivenNoEnd_Throws_ArgumentException()
		{
			// Arrange
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Empt },
			};

			// Act
			Action act = () => new Lee(tab, Movements);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		[Fact]
		public void Lee_GivenMultipleStart_Throws_ArgumentException()
		{
			// Arrange
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Star },
				{ LeeEnum.Empt, LeeEnum.Exit },
			};

			// Act
			Action act = () => new Lee(tab, Movements);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		[Fact]
		public void Lee_GivenMultipleEnd_Throws_ArgumentException()
		{
			// Arrange
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Empt },
				{ LeeEnum.Exit, LeeEnum.Exit },
			};

			// Act
			Action act = () => new Lee(tab, Movements);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		#endregion ctor Lee(tab)

		#region Execute

		[Fact]
		public void Lee_1()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Exit },
				{ LeeEnum.Empt, LeeEnum.Empt },
			};

			var expectedStartRow = 1;
			var expectedStartCol = 1;
			var expectedEndRow = 1;
			var expectedEndCol = 2;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 2;

			Lee_HappyTest(
				tab, 
				expectedStartRow, expectedStartCol, 
				expectedEndRow, expectedEndCol, 
				expectedLastRow, expectedLastCol, 
				expectedPathLength);
		}

		[Fact]
		public void Lee_2()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Empt},
				{ LeeEnum.Exit, LeeEnum.Empt },
			};

			var expectedStartRow = 1;
			var expectedStartCol = 1;
			var expectedEndRow = 2;
			var expectedEndCol = 1;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 2;

			Lee_HappyTest(
				tab, 
				expectedStartRow, expectedStartCol, 
				expectedEndRow, expectedEndCol, 
				expectedLastRow, expectedLastCol, 
				expectedPathLength);
		}

		[Fact]
		public void Lee_3()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Empt},
				{ LeeEnum.Empt, LeeEnum.Exit },
			};

			var expectedStartRow = 1;
			var expectedStartCol = 1;
			var expectedEndRow = 2;
			var expectedEndCol = 2;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 3;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		[Fact]
		public void Lee_4()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Empt, LeeEnum.Exit },
				{ LeeEnum.Empt, LeeEnum.Star },
			};

			var expectedStartRow = 2;
			var expectedStartCol = 2;
			var expectedEndRow = 1;
			var expectedEndCol = 2;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 2;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		[Fact]
		public void Lee_5()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Empt, LeeEnum.Empt},
				{ LeeEnum.Exit, LeeEnum.Star },
			};

			var expectedStartRow = 2;
			var expectedStartCol = 2;
			var expectedEndRow = 2;
			var expectedEndCol = 1;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 2;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		[Fact]
		public void Lee_6()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Exit, LeeEnum.Empt},
				{ LeeEnum.Empt, LeeEnum.Star },
			};

			var expectedStartRow = 2;
			var expectedStartCol = 2;
			var expectedEndRow = 1;
			var expectedEndCol = 1;
			var expectedLastRow = 3;
			var expectedLastCol = 3;

			var expectedPathLength = 3;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		[Fact]
		public void Lee_Complex1()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Star, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Exit, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt },
			};

			var expectedStartRow = 1;
			var expectedStartCol = 1;
			var expectedEndRow = 8;
			var expectedEndCol = 1;
			var expectedLastRow = 9;
			var expectedLastCol = 6;

			var expectedPathLength = 26;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		[Fact]
		public void Lee_Complex2()
		{
			LeeEnum[,] tab = new[,]
			{
				{ LeeEnum.Exit, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Obst, LeeEnum.Empt },
				{ LeeEnum.Star, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt, LeeEnum.Empt },
			};

			var expectedStartRow = 8;
			var expectedStartCol = 1;
			var expectedEndRow = 1;
			var expectedEndCol = 1;
			var expectedLastRow = 9;
			var expectedLastCol = 6;

			var expectedPathLength = 26;

			Lee_HappyTest(
				tab,
				expectedStartRow, expectedStartCol,
				expectedEndRow, expectedEndCol,
				expectedLastRow, expectedLastCol,
				expectedPathLength);
		}

		private static void Lee_HappyTest(
			LeeEnum[,] tab, 
			int expectedStartRow, int expectedStartCol, 
			int expectedEndRow, int expectedEndCol, 
			int expectedLastRow, int expectedLastCol, 
			int expectedPathLength)
		{
			// Arrange
			var expectedStart = new Position(expectedStartCol, expectedStartRow);
			var expectedEnd = new Position(expectedEndCol, expectedEndRow);

			var sut = new Lee(tab, Movements);

			// Act
			var result = sut.Execute();

			// Assert
			Assert.Equal(expectedLastRow, result.LastY);
			Assert.Equal(expectedLastCol, result.LastX);

			Assert.Equal(expectedStart, result.Start);
			Assert.Equal(expectedEnd, result.End);

			Assert.True(result.HasSolution);

			Assert.Equal(expectedPathLength - 1, result.SolutionLength);

			var path = result.GetOneRoute().ToArray();
			Assert.Equal(expectedPathLength, path.Length);
			Assert.Equal(path[0], expectedStart);
			Assert.Equal(path[path.Length - 1], expectedEnd);
		}

		#endregion Execute
	}
}
