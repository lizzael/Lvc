using Lvc.Extensions;
using System;
using Xunit;

namespace Lvc.Tests.Extensions
{
	public class ComparableExtensionsTests
    {
		#region IsInRange

		[Fact]
		public void IsInRange_Throw_NullReferenceException()
		{
			// Arrange
			string minValue = "a";
			string maxValue = "b";

			string sut = null;

			// Act
			Action act = () => sut.IsInRange(minValue, maxValue);

			// Assert
			Assert.Throws<NullReferenceException>(act);
		}
		
		[Theory]
		[InlineData(-4, -3, 3, false)]
		[InlineData(-3, -3, 3, true)]
		[InlineData(-3, -3, 3, true)]
		[InlineData(-2, -3, 3, true)]
		[InlineData(-1, -3, 3, true)]
		[InlineData(0, -3, 3, true)]
		[InlineData(1, -3, 3, true)]
		[InlineData(2, -3, 3, true)]
		[InlineData(3, -3, 3, false)]
		[InlineData(4, -3, 3, false)]
		public void IsInRange_Int(int value, int minValue, int maxValue, bool expectedResult)
		{
			// Arrange

			// Act
			var result = value.IsInRange(minValue, maxValue);

			// Asert
			Assert.Equal(result, expectedResult);
		}

		[Theory]
		[InlineData("a", "b", "e", false)]
		[InlineData("b", "b", "e", true)]
		[InlineData("c", "b", "e", true)]
		[InlineData("d", "b", "e", true)]
		[InlineData("e", "b", "e", false)]
		[InlineData("f", "b", "e", false)]
		public void IsInRange_String(string value, string minValue, string maxValue, bool expectedResult)
		{
			// Act
			var result = value.IsInRange(minValue, maxValue);

			// Assert
			Assert.Equal(result, expectedResult);
		}

		#endregion IsInRange
	}
}
