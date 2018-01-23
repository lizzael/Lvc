using Lvc.Extensions;
using Lvc.Performance.Algorithms.HashCodes;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.Algorithms.HashCodes
{
	public class IntsTests
	{
		#region GetHashCode

		[Fact]
		public void GetHashCode_CtorWithDefaultValues_NoParam_ReturnsExpectedResult()
		{
			// Arrange
			var sut = new IntsHashCodeProvider();
			var expectedResult = GetExpectedResult(sut.Prime1, sut.Prime2);

			// Act
			var result = sut.GetHashCode();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData("0")]
		[InlineData("1")]
		[InlineData("5")]
		[InlineData("100")]
		[InlineData("-2147483648")]
		[InlineData("2147483647")]
		[InlineData("-2147483648 2147483647")]
		[InlineData("0 1 -2 5 100 -2345 -2147483648 2147483647")]
		public void GetHashCode_CtorWithDefaultValues_SeveralParams_ReturnsExpectedResult(string str)
		{
			// Arrange
			var values = str.ConvertToMany<int>(' ').ToArray();
			
			var sut = new IntsHashCodeProvider();
			var expectedResult = GetExpectedResult(sut.Prime1, sut.Prime2, values);

			// Act
			var result = sut.GetHashCode(values);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData(11, 19)]
		[InlineData(13, 29)]
		[InlineData(31, 57)]
		public void GetHashCode_CtorWithValues_NoParam_ReturnsExpectedResult(int p1, int p2)
		{
			// Arrange
			var sut = new IntsHashCodeProvider(p1, p2);
			var expectedResult = GetExpectedResult(p1, p2);

			// Act
			var result = sut.GetHashCode();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData(11, 19, "0")]
		[InlineData(11, 19, "1")]
		[InlineData(13, 29, "5")]
		[InlineData(13, 29, "100")]
		[InlineData(31, 57, "-2147483648")]
		[InlineData(31, 57, "2147483647")]
		[InlineData(31, 57, "-2147483648 2147483647")]
		[InlineData(31, 57, "0 1 -2 5 100 -2345 -2147483648 2147483647")]
		public void GetHashCode_CtorWithValues_SeveralParams_ReturnsExpectedResult(
			int p1, int p2, string str)
		{
			// Arrange
			var values = str.ConvertToMany<int>(' ').ToArray();

			var sut = new IntsHashCodeProvider();
			var expectedResult = GetExpectedResult(sut.Prime1, sut.Prime2, values);

			// Act
			var result = sut.GetHashCode(values);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		private int GetExpectedResult(int prime1, int prime2, params int[] values)
		{
			var result = prime1;
			foreach (var value in values)
				unchecked
				{
					result = result * prime2 + value;
				}

			return result;
		}

		#endregion GetHashCode
	}
}
