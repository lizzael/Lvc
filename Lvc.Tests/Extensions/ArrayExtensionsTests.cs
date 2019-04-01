using Lvc.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Tests.Extensions
{
	public class ArrayExtensionsTests
    {
		#region FindIndex

		[Theory]
		[InlineData("0", 0, 0)]
		[InlineData("0", 1, -1)]
		[InlineData("0 1 2 3 4", 0, 0)]
		[InlineData("0 1 2 3 4", 1, 1)]
		[InlineData("0 1 2 3 4", 2, 2)]
		[InlineData("0 1 2 3 4", 3, 3)]
		[InlineData("0 1 2 3 4", 4, 4)]
		[InlineData("0 1 2 3 4", -1, -1)]
		[InlineData("0 1 2 3 4", -2, -1)]
		[InlineData("0 1 2 3 4", 5, -1)]
		[InlineData("0 1 2 3 4", 6, -1)]
		public void FindIndex(
            string s, 
            int t, 
            int expectedResult)
		{
			// Arrange
			var sut = s.Split(' ')
				.Select(int.Parse)
				.ToArray();

			// Act
			var result = sut.FindIndex(t);

			// Assert
			Assert.Equal(result, expectedResult);
		}

		[Fact]
		public void FindIndex_Throw_NullReferenceException()
		{
			// Arrange
			char t = ' ';
			char[] sut = null;

			// Act
			Action act = () => sut.FindIndex(t);

			// Assert
			Assert.Throws<NullReferenceException>(act);
		}

		#endregion FindIndex
	}
}
