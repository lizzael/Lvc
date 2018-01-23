using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ADTs.Graphs.Edges;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Graphs
{
	public class UndirectedGraphTests : CommonGraphTests<IUndirectedEdge>
	{
		public override GetGraph<IUndirectedEdge> GetGraph =>
			countOfNodes => new UndirectedGraph(countOfNodes);

		public override IUndirectedEdge GetEdge(int n1, int n2, int cost = 1) =>
			new UndirectedEdge(n1, n2, cost);

		#region AddEdge

		protected override void AddEdge_NodesWithoutOtherEdges_OtherAsserts(
			IUndirectedEdge e, Graph<IUndirectedEdge> sut)
		{
			Assert.Equal(e.Cost, sut.GetCost(e.V1, e.V2));
			Assert.Equal(new[] { e.V1 }, sut.GetAdjacentNodes(e.V2));
		}

		protected override void AddEdge_XToY_And_YToX_OtherAsserts(
			IUndirectedEdge xToY, IUndirectedEdge yToX, Graph<IUndirectedEdge> sut)
		{
			Assert.Equal(yToX.Cost, sut.GetCost(xToY.V1, xToY.V2));
			Assert.Equal(yToX.Cost, sut.GetCost(yToX.V1, yToX.V2));
			Assert.Equal(new[] { yToX }, sut.Edges);
		}

		protected override void AddEdge(IUndirectedEdge e, LinkedList<int>[] adjacencyList) 
		{
			adjacencyList[e.V1].AddLast(e.V2);
			adjacencyList[e.V2].AddLast(e.V1);
		}

		#endregion AddEdge

		#region Kruskal

		[Fact]
		public void Kruskal_GivenNullComparison_Throws_ArgumentNullException()
		{
			// Arrange
			Comparison<IUndirectedEdge> comparison = null;

			var countOfNodes = 0;
			var sut = new UndirectedGraph(countOfNodes);

			// Act
			Action act = () => sut.Kruskal(comparison);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory]
		[InlineData("", 2, 0)]
		[InlineData("0-1,1 1-2,1", 4, 2)] /* N1-N2,Weight */
		[InlineData("1-2,1 2-3,1", 4, 2)] /* N1-N2,Weight */
		public void Kruskal_NotConnectedGraph(string edgesStr, int countOfNodes, int expectedResult)
		{
			// Arrange
			Comparison<IUndirectedEdge> comparison = (x, y) => x.Cost - y.Cost;

			UndirectedGraph sut = GetSut(edgesStr, countOfNodes);

			// Act
			var totalCost = sut.Kruskal(comparison)
				.Sum(s => s.Cost);

			// Assert
			Assert.Equal(expectedResult, totalCost);
		}

		[Theory]
		[InlineData("0-1,1", 2, 1)]
		[InlineData("0-1,10 1-2,1 2-0,1", 3, 2)] /* N1-N2,Weight */
		[InlineData("0-1,10 0-2,9 0-3,9 1-2,1 1-3,1 2-3,0", 4, 10)] /* N1-N2,Weight */
		public void Kruskal_ConnectedGraph(string edgesStr, int countOfNodes, int expectedResult)
		{
			// Arrange
			Comparison<IUndirectedEdge> comparison = (x, y) => x.Cost - y.Cost;

			UndirectedGraph sut = GetSut(edgesStr, countOfNodes);

			// Act
			var totalCost = sut.Kruskal(comparison)
				.Sum(s => s.Cost);

			// Assert
			Assert.Equal(expectedResult, totalCost);
		}

		// Todo: Add more complex cases.

		#endregion Kruskal

		#region Prim

		[Fact]
		public void Prim_GivenNullComparison_Throws_ArgumentNullException()
		{
			// Arrange
			var startingNode = 0;
			Comparison<int> comparison = null;

			var countOfNodes = 0;
			var sut = new UndirectedGraph(countOfNodes);

			// Act
			Action act = () => sut.Prim(comparison, startingNode);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory]
		[InlineData(0, -1)]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(5, 5)]
		[InlineData(5, 6)]
		public void Prim_GivenStartingNodeOutOfRange_Throws_ArgumentOutOfRangeException(int countOfNodes, int startingNode)
		{
			// Arrange
			Comparison<int> comparison = Comparer<int>.Default.Compare;

			var sut = new UndirectedGraph(countOfNodes);

			// Act
			Action act = () => sut.Prim(comparison, startingNode);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("", 2, 0, 0)]
		[InlineData("0-1,1 1-2,1", 4, 0, 2)] /* N1-N2,Weight */
		[InlineData("0-1,1 1-2,1", 4, 1, 2)] /* N1-N2,Weight */
		[InlineData("0-1,1 1-2,1", 4, 2, 2)] /* N1-N2,Weight */
		[InlineData("0-1,1 1-2,1", 4, 3, 0)] /* N1-N2,Weight */
		[InlineData("1-2,1 2-3,1", 4, 0, 0)] /* N1-N2,Weight */
		[InlineData("1-2,1 2-3,1", 4, 1, 2)] /* N1-N2,Weight */
		[InlineData("1-2,1 2-3,1", 4, 2, 2)] /* N1-N2,Weight */
		[InlineData("1-2,1 2-3,1", 4, 3, 2)] /* N1-N2,Weight */
		public void Prim_NotConnectedGraph(string edgesStr, int countOfNodes, int startingNode, int expectedResult)
		{
			// Arrange
			Comparison<int> comparison = Comparer<int>.Default.Compare;

			UndirectedGraph sut = GetSut(edgesStr, countOfNodes);

			// Act
			var totalCost = sut.Prim(comparison, startingNode)
				.Sum(s => s.Cost);

			// Assert
			Assert.Equal(expectedResult, totalCost);
		}

		[Theory]
		[InlineData("0-1,1", 2, 0, 1)]
		[InlineData("0-1,1", 2, 1, 1)]
		[InlineData("0-1,10 1-2,1 2-0,1", 3, 0, 2)] /* N1-N2,Weight */
		[InlineData("0-1,10 1-2,1 2-0,1", 3, 1, 2)] /* N1-N2,Weight */
		[InlineData("0-1,10 1-2,1 2-0,1", 3, 2, 2)] /* N1-N2,Weight */
		[InlineData("0-1,10 0-2,9 0-3,9 1-2,1 1-3,1 2-3,0", 4, 0, 10)] /* N1-N2,Weight */
		[InlineData("0-1,10 0-2,9 0-3,9 1-2,1 1-3,1 2-3,0", 4, 1, 10)] /* N1-N2,Weight */
		[InlineData("0-1,10 0-2,9 0-3,9 1-2,1 1-3,1 2-3,0", 4, 2, 10)] /* N1-N2,Weight */
		[InlineData("0-1,10 0-2,9 0-3,9 1-2,1 1-3,1 2-3,0", 4, 3, 10)] /* N1-N2,Weight */
		public void Prim_ConnectedGraph(string edgesStr, int countOfNodes, int startingNode, int expectedResult)
		{
			// Arrange
			Comparison<int> comparison = Comparer<int>.Default.Compare;

			UndirectedGraph sut = GetSut(edgesStr, countOfNodes);

			// Act
			var totalCost = sut.Prim(comparison, startingNode)
				.Sum(s => s.Cost);

			// Assert
			Assert.Equal(expectedResult, totalCost);
		}

		// Todo: Add more complex cases.

		#endregion Prim

		private UndirectedGraph GetSut(string edgesStr, int countOfNodes)
		{
			IUndirectedEdge[] edges = GetEdges(edgesStr);
			var sut = new UndirectedGraph(countOfNodes);

			foreach (var edge in edges)
				sut.AddEdge(edge);

			return sut;
		}
	}
}
