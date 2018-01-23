using Lvc.Performance.Core.Utils;
using Lvc.Performance.Utils;
using Lvc.Performance.Utils.PositionMovements;
using System;
using Xunit;

namespace Lvc.Performance.Tests.Utils.PositionMovements
{
	public class MovementsFactoryTests
	{
		#region Create

		[Fact]
		public void Create_RelativePositionsNull_Throws_ArgumentNullException()
		{
			// Arrange
			IPosition[] relativePositions = null;
			PositionFactory positionFactory = new PositionFactory();

			var sut = new MovementsFactory();

			// Act
			Action act = () => sut.Create(relativePositions, positionFactory);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Create_PositionFactoryNull_Throws_ArgumentNullException()
		{
			// Arrange
			IPosition[] relativePositions = new IPosition[0];
			PositionFactory positionFactory = null;

			var sut = new MovementsFactory();

			// Act
			Action act = () => sut.Create(relativePositions, positionFactory);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void Create()
		{
			// Arrange
			var relativePositions = new IPosition[0];
			var positionFactory = new PositionFactory();

			var sut = new MovementsFactory();

			// Act
			var result = sut.Create(relativePositions, positionFactory);

			// Assert
			Assert.Equal(relativePositions, result.RelativePositions);
			Assert.Equal(positionFactory, result.PositionFactory);
		}

		#endregion Create
	}
}
