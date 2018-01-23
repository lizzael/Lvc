using Lvc.Performance.Extensions;
using System;
using Xunit;

namespace Lvc.Performance.Tests.Extensions
{
	public class StringExtensionsTests
    {
		#region LongestCommonSubsequentLength

		[Fact]
		public void LongestCommonSubsequentLength_Throw_NullReferenceException()
		{
			// Arrange
			var str = "any";

			string sut = null;

			// Act
			Action act = () => sut.LongestCommonSubsequentLength(str);

			// Assert
			Assert.Throws<NullReferenceException>(act);
		}

		[Fact]
		public void LongestCommonSubsequentLength_GivenNull_Throws_ArgumentNullException()
		{
			// Arrange
			string str = null;

			var sut = "";

			// Act
			Action act = () => sut.LongestCommonSubsequentLength(str);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory]
		[InlineData("", "", 0)]
		[InlineData("asd", "", 0)]
		[InlineData("", "dsfer", 0)]
		[InlineData("a", "a", 1)]
		[InlineData("cbg", "brt", 1)]
		[InlineData("cbgf", "bfrft", 2)]
		[InlineData("ABCDE", "AABACCDAFF", 4)]
		public void LongestCommonSubsequentLength(string sut, string str, int expectedResult)
		{
			// Arrange

			// Act
			var result = sut.LongestCommonSubsequentLength(str);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion LongestCommonSubsequentLength
	}
}
