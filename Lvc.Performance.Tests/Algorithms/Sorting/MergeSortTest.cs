using Lvc.Extensions;
using Lvc.Performance.Algorithms.Sorting;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.Algorithms.Sorting
{
    public class MergeSortTest
	{
		#region ctor MergeSort(comparison)

		[Theory]
		[InlineData(0)]
		public void MergeSort_GivenANotComparableTypeAndNull_Throws_ArgumentException(int paramName)
		{
			// Arrange
			Comparison<MergeSortTest> comparison = null;

			// Act
			Action act = () => new MergeSort<MergeSortTest>(comparison);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		#endregion ctor MergeSort(comparison)

		#region Sort

		[Fact]
		public void Sort_GivenNull_Throws_ArgumentNullException()
		{
			// Arrange
			int[] items = null;

			var sut = new MergeSort<int>();

			// Act
			Action act = () => sut.Sort(items);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("0", "0")]
		[InlineData("1", "1")]
		[InlineData("-1", "-1")]
		[InlineData("0 1", "0 1")]
		[InlineData("1 0", "0 1")]
		[InlineData("-1 0 1", "-1 0 1")]
		[InlineData("0 1 -1", "-1 0 1")]
		[InlineData("1 -1 0", "-1 0 1")]
		[InlineData("1 0 -1", "-1 0 1")]
		[InlineData("3 -2 0 3 0 -2 3", "-2 -2 0 0 3 3 3")]
		public void Sort_DefaultIntComparer(string data, string expectedResult)
		{
			var items = data == string.Empty
				? new int[0]
				: data.ConvertToMany<int>(' ').ToArray();

			Sort(items, expectedResult);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("0", "0")]
		[InlineData("1", "1")]
		[InlineData("-1", "-1")]
		[InlineData("0 1", "1 0")]
		[InlineData("1 0", "1 0")]
		[InlineData("-1 0 1", "1 0 -1")]
		[InlineData("0 1 -1", "1 0 -1")]
		[InlineData("1 -1 0", "1 0 -1")]
		[InlineData("1 0 -1", "1 0 -1")]
		[InlineData("3 -2 0 3 0 -2 3", "3 3 3 0 0 -2 -2")]
		public void Sort_DescendingIntComparer(string data, string expectedResult)
		{
			var items = data == string.Empty
				? new int[0]
				: data.ConvertToMany<int>(' ').ToArray();

			Sort(items, expectedResult, (x, y) => y - x);
		}

		private static void Sort<T>(T[] items, string expectedResult, Comparison<T> comparison = null)
		{
			// Arrange
			var sut = new MergeSort<T>(comparison);

			// Act
			var result = sut.Sort(items);

			// Assert
			Assert.Equal(expectedResult, string.Join(" ", result));
		}

		#endregion Sort
	}
}
