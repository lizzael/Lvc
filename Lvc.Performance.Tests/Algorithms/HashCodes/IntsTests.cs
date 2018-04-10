﻿using Lvc.Extensions;
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
			var sut = new HashCodeProvider();
			var expectedResult = sut.Prime1;

			// Act
			var result = sut.GetHashCode();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData(0, 1, 2, 2505450)]
		[InlineData(-124, 34, 1, 1882708)]
		[InlineData(999999, -88888, 7777, 742229769)]
		public void GetHashCode_CtorWithDefaultValues_Params_ReturnsExpectedResult(int n1, int n2, int n3, int expectedResult)
		{
			// Arrange
			var sut = new HashCodeProvider();

			// Act
			var result = sut.GetHashCode(n1, n2, n3);

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
			var sut = new HashCodeProvider(p1, p2);
			var expectedResult = sut.Prime1;

			// Act
			var result = sut.GetHashCode();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData(11, 19, 0, 1, 2, 75470)]
		[InlineData(13, 29, -124, 34, 1, 213760)]
		[InlineData(31, 57, 999999, -88888, 7777, -1045288401)]
		public void GetHashCode_CtorWithValues_Params_ReturnsExpectedResult(int p1, int p2, int n1, int n2, int n3, int expectedResult)
		{
			// Arrange
			var sut = new HashCodeProvider(p1, p2);

			// Act
			var result = sut.GetHashCode(n1, n2, n3);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion GetHashCode
	}
}
