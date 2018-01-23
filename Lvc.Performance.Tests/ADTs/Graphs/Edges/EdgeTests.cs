using Lvc.Performance.Core.ADTs.Graphs.Edges;
using Lvc.Tests;
using System;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Graphs.Edges
{
	public delegate IEdge GetEdge(int v1, int v2, int cost = 1);

	public abstract class EdgeTests
	{
		public abstract GetEdge GetEdge { get;  }
		
		#region ctor Edge(v1, v2)

		[Theory]
		[InlineData(-1, 0)]
		[InlineData(0, -1)]
		public void Edge_GivenNegativeV1OrV2_Throws_ArgumentOutOfRangeException(int v1, int v2)
		{
			// Arrange

			// Act
			Action act = () => GetEdge(v1, v2);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(2, 3)]
		[InlineData(3, 2)]
		public void Edge(int v1, int v2)
		{
			// Arrange

			// Act
			var sut = GetEdge(v1, v2);

			// Assert
			AssertEdgeCreation(v1, v2, 1, sut);
		}

		[Theory]
		[InlineData(1, 10, 5)]
		[InlineData(10, 1, 5)]
		[InlineData(20, 30, -6)]
		[InlineData(30, 20, -6)]
		public void EdgeWithCost(int v1, int v2, int cost)
		{
			// Arrange

			// Act
			var sut = GetEdge(v1, v2, cost);

			// Assert
			AssertEdgeCreation(v1, v2, cost, sut);
		}

		protected virtual void AssertEdgeCreation(int v1, int v2, int cost, IEdge sut)
		{
			Assert.Equal(v1, sut.V1);
			Assert.Equal(v2, sut.V2);
			Assert.Equal(cost, sut.Cost);
		}

		#endregion ctor Edge(v1, v2)

		#region set Cost

		[Theory]
		[InlineData(0, 1, 2)]
		[InlineData(2, 7, 3)]
		public void SetCost_WithInitialDefaultCost(int v1, int v2, int cost2) =>
			SetCost(v1, v2, cost2, GetEdge(v1, v2));

		[Theory]
		[InlineData(0, 1, 1, 2)]
		[InlineData(2, 7, 5, 3)]
		public void SetCost_WithInitialCost(int v1, int v2, int cost1, int cost2) =>
			SetCost(v1, v2, cost2, GetEdge(v1, v2, cost1));

		private void SetCost(int v1, int v2, int cost2, IEdge sut)
		{
			// Arrange
			IEdge expectedEdge = GetEdge(v1, v2, cost2);

			// Act
			sut.Cost = cost2;

			// Assert
			Assert.Equal(expectedEdge, sut);
		}

		#endregion set Cost

		#region Equal_GetHashCode_ToString

		[Theory]
		[InlineData(0, 0, 0, 0, true)]
		[InlineData(0, 1, 0, 1, true)]
		[InlineData(1, 1, 1, 1, true)]
		[InlineData(1, 0, 1, 0, true)]
		[InlineData(2, 3, 2, 3, true)]
		[InlineData(0, 0, 1, 0, false)]
		[InlineData(0, 0, 0, 1, false)]
		[InlineData(0, 0, 1, 1, false)]
		public void Equal_GetHashCode_ToString_NoCost(int v1, int v2, int v3, int v4, bool expectedResult)
		{
			// Arrange
			var p1 = GetEdge(v1, v2);
			var p2 = GetEdge(v3, v4);

			CommonTest.TestOverridedObjectMethods(expectedResult, p1, p2);
		}

		#region Equal_GetHashCode_ToString

		[Theory]
		[InlineData(0, 0, 0, 0, 0, 0, true)]
		[InlineData(0, 0, 1, 0, 0, 1, true)]
		[InlineData(0, 1, 1, 0, 1, 1, true)]
		[InlineData(1, 1, 1, 1, 1, 1, true)]
		[InlineData(2, 3, 4, 2, 3, 4, true)]
		[InlineData(0, 0, 0, 0, 0, 1, false)]
		[InlineData(0, 0, 0, 1, 0, 0, false)]
		[InlineData(0, 0, 0, 0, 1, 0, false)]
		[InlineData(0, 0, 0, 1, 1, 0, false)]
		public void Equal_GetHashCode_ToString(
			int v1, int v2, int c1, int v3, int v4, int c2, bool expectedResult)
		{
			// Arrange
			var p1 = GetEdge(v1, v2, c1);
			var p2 = GetEdge(v3, v4, c2);

			CommonTest.TestOverridedObjectMethods(expectedResult, p1, p2);
		}

		#endregion Equal_GetHashCode_ToString


		#endregion Equal_GetHashCode_ToString

		#region Clone

		[Theory]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(5, 9)]
		[InlineData(9, 5)]
		[InlineData(100, 49)]
		[InlineData(49, 100)]
		public void Clone_WithoutCost(int v1, int v2) =>
			Clone(GetEdge(v1, v2));

		[Theory]
		[InlineData(0, 1, 1)]
		[InlineData(1, 0, 2)]
		[InlineData(5, 9, 10)]
		[InlineData(9, 5, 10)]
		[InlineData(100, 49, 22)]
		[InlineData(49, 100, 22)]
		public void Clone_WithCost(int v1, int v2, int cost) =>
			Clone(GetEdge(v1, v2, cost));

		private static void Clone(IEdge sut)
		{
			// Arrange

			// Act
			var result = (IEdge)sut.Clone();

			// Assert
			Assert.True(result != sut);
			Assert.Equal(sut, result);
		}

		#endregion Clone
	}
}
