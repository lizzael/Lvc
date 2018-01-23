using Lvc.Performance.Utils;
using Xunit;

namespace Lvc.Performance.Tests.Utils
{
	public class PositionFactoryTests
	{
		#region Create

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(0, -1)]
		[InlineData(-1, 0)]
		[InlineData(-1, -1)]
		[InlineData(int.MinValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		public void Create(int x, int y)
		{
			// Arrange
			var sut = new PositionFactory();

			// Act
			var result = sut.Create(x, y);

			// Assert
			Assert.Equal(x, result.X);
			Assert.Equal(y, result.Y);

		}

		#endregion Create
	}
}
