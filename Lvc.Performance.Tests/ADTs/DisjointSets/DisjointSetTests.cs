using Lvc.Performance.ADTs.DisjointSets;
using Lvc.Performance.Core.ADTs.DisjointSets;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.DisjointSets
{
	public class DisjointSetTests
	{
		#region Count, Items

		[Theory]
		[InlineData(0, "")]
		[InlineData(1, "(P: 0, R: 0)")]
		[InlineData(5, "(P: 0, R: 0) (P: 1, R: 0) (P: 2, R: 0) (P: 3, R: 0) (P: 4, R: 0)")]
		public void MyTheoryName(int count, string expectedFormattedResult)
		{
			// Arrange

			// Act
			var sut = new DisjointSet(count);

			// Assert
			Assert.Equal(count, sut.Count);
			Assert.Equal(expectedFormattedResult, string.Join<IData>(" ", sut.Items));
		}

		#endregion Items and Count

		#region FindRoot

		[Theory]
		[InlineData(5, -1)]
		[InlineData(5, 5)]
		[InlineData(5, 6)]
		public void FindRoot_IndexOutOfRange_Throws_ArgumentOutOfRangeException(int count, int index)
		{
			// Arrange
			var sut = new DisjointSet(count);

			// Act
			Action act = () => sut.FindRoot(index);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData(0, "")]
		[InlineData(1, "0")]
		[InlineData(5, "0 1 2 3 4")]
		public void FindRoot(int count, string expectedFormattedResult)
		{
			// Arrange
			var sut = new DisjointSet(count);

			// Act
			var result = Enumerable.Range(0, count)
				.Select(sut.FindRoot);

			// Assert
			Assert.Equal(expectedFormattedResult, string.Join(" ", result));
		}

		#endregion FindRoot

		#region Union and FindRoot

		[Theory]
		[InlineData(5, -1, 0)]
		[InlineData(5, 0, -1)]
		[InlineData(5, 5, 2)]
		[InlineData(5, 2, 5)]
		[InlineData(5, 6, 4)]
		[InlineData(5, 4, 6)]
		public void Union_IndexOutOfRange_Throws_ArgumentOutOfRangeException(int count, int p1, int p2)
		{
			// Arrange
			var sut = new DisjointSet(count);

			// Act
			Action act = () => sut.Union(p1, p2);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Fact]
		public void Union_TwoSingleNodes()
		{
			// Arrange
			var count = 2;

			var sut = new DisjointSet(count);

			// Act
			sut.Union(0, 1);

			// Assert
			Assert.Equal(0, sut.FindRoot(0));
			Assert.Equal(1, sut.Items[0].Rank);
			Assert.Equal(0, sut.FindRoot(1));
			Assert.Equal(0, sut.Items[1].Rank);
		}

		[Fact]
		public void Union_FourNodes()
		{
			// Arrange
			var count = 4;

			var sut = new DisjointSet(count);

			// Act
			sut.Union(0, 1);
			sut.Union(2, 3);
			sut.Union(1, 3);

			// Assert
			Assert.Equal(0, sut.FindRoot(0));
			Assert.Equal(2, sut.Items[0].Rank);
			Assert.Equal(0, sut.FindRoot(1));
			Assert.Equal(0, sut.Items[1].Rank);
			Assert.Equal(0, sut.FindRoot(2));
			Assert.Equal(1, sut.Items[2].Rank);
			Assert.Equal(0, sut.FindRoot(3));
			Assert.Equal(0, sut.Items[3].Rank);
		}

		#endregion Union and FindRoot
	}
}
