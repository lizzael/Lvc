using Lvc.Extensions;
using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ATDs.Trees;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Graphs
{
	public delegate Graph<TEdge> GetGraph<TEdge>(int countOfNodes)
		where TEdge : IEdge;

	public abstract class CommonGraphTests<TEdge>
		where TEdge : IEdge
	{
		public abstract GetGraph<TEdge> GetGraph { get; }

		public abstract TEdge GetEdge(int n1, int n2, int cost = 1);

		public TEdge[] GetEdges(string edgesStr) =>
			edgesStr == string.Empty
				? new TEdge[0]
				: edgesStr.Split(' ')
					.Select(s =>
					{
						var data = s.ConvertToMany<int>("-", ",")
							.ToArray();

						return data.Length == 3 
							? GetEdge(data[0], data[1], data[2])
							: GetEdge(data[0], data[1]);
					})
					.ToArray();

		[Fact]
		public void CreateGraph_Given_NegativeCountOfNodes_Throws_ArgumentOutOfRangeException()
		{
			// Arrange
			var countOfNodes = -1;

			// Act
			Action act = () => GetGraph(countOfNodes);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.CreateGraph),
			MemberType = typeof(CommonGraphTests_Data))]
		public void CreateGraph(int countOfNodes)
		{
			// Arrange

			// Act
			var sut = GetGraph(countOfNodes);

			// Assert
			Assert.Equal(countOfNodes, sut.CountOfNodes);
			Assert.False(sut.Edges.Any());
		}

		#region GetAjacentNodes

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.GetAjacentNodes_Given_OutOfRangeValues_Throws_ArgumentOutRangeException),
			MemberType = typeof(CommonGraphTests_Data))]
		public void GetAjacentNodes_Given_OutOfRangeValues_Throws_ArgumentOutRangeException(
			int countOfNodes, int node)
		{
			// Arrange
			var sut = GetGraph(countOfNodes);

			// Act
			Action act = () => sut.GetAdjacentNodes(node);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.GetAjacentNodes_GivenNodeWithNoEdges_Returns_EmptyEnumerable),
			MemberType = typeof(CommonGraphTests_Data))]
		public void GetAjacentNodes_GivenNodeWithNoEdges_Returns_EmptyEnumerable(
			int countOfNodes, int node)
		{
			// Arrange
			var sut = GetGraph(countOfNodes);

			// Act
			var result = sut.GetAdjacentNodes(node);

			// Assert
			Assert.False(result.Any());
		}

		#endregion GetAjacentNodes

		#region GetCost

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.GetCost_GivenValuesOutOfRange_Throws_ArgumentOutOfRangeException),
			MemberType = typeof(CommonGraphTests_Data))]
		public void GetCost_GivenValuesOutOfRange_Throws_ArgumentOutOfRangeException(
			int countOfNodes, int n1, int n2)
		{
			// Arrange
			var sut = GetGraph(countOfNodes);

			// Act
			Action act = () => sut.GetCost(n1, n2);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.GetCost_GivenNodesWithoutEdgeBetweenThem),
			MemberType = typeof(CommonGraphTests_Data))]
		public void GetCost_GivenNodesWithoutEdgeBetweenThem(
			int countOfNodes, int n1, int n2)
		{
			// Arrange
			var sut = GetGraph(countOfNodes);

			// Act
			var result = sut.GetCost(n1, n2);

			// Assert
			Assert.Null(result);
		}

		#endregion GetCost

		#region AddEdge

		[Fact]
		public void AddEdge_GivenNull_Throws_ArgumentNullException()
		{
			// Arrange
			var countOfNodes = 10;
			TEdge edge = default(TEdge);

			var sut = GetGraph(countOfNodes);

			// Act
			Action act = () => sut.AddEdge(edge);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.AddEdge_GivenNodesOutOfRange_Throws_ArgumentOutOfRangeException),
			MemberType = typeof(CommonGraphTests_Data))]
		public void AddEdge_GivenNodesOutOfRange_Throws_ArgumentOutOfRangeException(
			int countOfNodes, int n1, int n2)
		{
			// Arrange
			TEdge edge = GetEdge(n1, n2);

			var sut = GetGraph(countOfNodes);

			// Act
			Action act = () => sut.AddEdge(edge);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.AddEdge_GivenEqualsNodes_Throws_ArgumentException),
			MemberType = typeof(CommonGraphTests_Data))]
		public void AddEdge_GivenEqualsNodes_Throws_ArgumentException(
			int countOfNodes, int n1, int n2)
		{
			// Arrange
			TEdge edge = GetEdge(n1, n2);

			var sut = GetGraph(countOfNodes);

			// Act
			Action act = () => sut.AddEdge(edge);

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.AddEdge_AddingOneEdgeWithDefaultCost),
			MemberType = typeof(CommonGraphTests_Data))]
		public void AddEdge_AddingOneEdgeWithDefaultCost(
			int countOfNodes, int n1, int n2
		) =>
			AddEdge(countOfNodes, n1, n2, GetEdge(n1, n2));

		[Theory, MemberData(
			nameof(CommonGraphTests_Data.AddEdge_AddingOneEdgeWithCost),
			MemberType = typeof(CommonGraphTests_Data))]
		public void AddEdge_AddingOneEdgeWithCost(
			int countOfNodes, int n1, int n2, int cost
		) =>
			AddEdge(countOfNodes, n1, n2, GetEdge(n1, n2, cost), cost);

		private void AddEdge(int countOfNodes, int n1, int n2, TEdge edge, int cost = 1)
		{
			// Arrange
			var sut = GetGraph(countOfNodes);

			// Act
			sut.AddEdge(edge);

			// Assert
			Assert.Equal(new[] { edge }, sut.Edges);
			Assert.Equal(cost, sut.GetCost(n1, n2));
			Assert.Equal(new[] { n2 }, sut.GetAdjacentNodes(n1));

			AddEdge_NodesWithoutOtherEdges_OtherAsserts(edge, sut);
		}

		[Fact]
		public void AddEdge_TwoEdgeWithSameNodes_LasCostStays()
		{
			// Arrange
			var n1 = 1;
			var n2 = 2;
			var cost1 = 2;
			var cost2 = 3;

			var edge1 = GetEdge(n1, n2, cost1);
			var edge2 = GetEdge(n1, n2, cost2);

			var countOfNodes = 4;
			var sut = GetGraph(countOfNodes);

			// Act
			sut.AddEdge(edge1);
			sut.AddEdge(edge2);

			// Assert
			Assert.Equal(new[] { edge2 }, sut.Edges);
			Assert.Equal(edge2.Cost, sut.GetCost(n1, n2));
			Assert.Equal(new[] { n2 }, sut.GetAdjacentNodes(n1));

			AddEdge_NodesWithoutOtherEdges_OtherAsserts(edge2, sut);
		}

		protected abstract void AddEdge_NodesWithoutOtherEdges_OtherAsserts(
			TEdge e, Graph<TEdge> sut);

		[Fact]
		public void AddEdge_XToY_And_YToX()
		{
			// Arrange
			var x = 1;
			var y = 2;
			var c1 = 3;
			var c2 = 4;

			var xtoY = GetEdge(x, y, c1);
			var yToX = GetEdge(y, x, c2);

			var countOfNodes = 4;
			var sut = GetGraph(countOfNodes);

			// Act
			sut.AddEdge(xtoY);
			sut.AddEdge(yToX);

			// Assert
			Assert.Equal(new[] { y }, sut.GetAdjacentNodes(x));
			Assert.Equal(new[] { x }, sut.GetAdjacentNodes(y));

			AddEdge_XToY_And_YToX_OtherAsserts(xtoY, yToX, sut);
		}

		protected abstract void AddEdge_XToY_And_YToX_OtherAsserts(
			TEdge xToY, TEdge yToX, Graph<TEdge> sut);

		[Theory]
		[InlineData(3, "0-1 1-2 2-0")]
		[InlineData(6, "0-1 0-2 0-3 0-4 0-5")]
		[InlineData(6, "0-1 0-2 0-3 0-4 0-5 1-2 1-3 1-4 1-5 2-3 2-4 2-5 3-4 3-5 4-5")]
		public void AddEdge_NodesWithSeveralEdges(int countOfNodes, string strEdges)
		{
			// Arrange
			var edges = GetEdges(strEdges);

			var sut = GetGraph(countOfNodes);

			// Act
			foreach (var e in edges)
				sut.AddEdge(e);

			// Assert
			var expectedAdjacencyLists = GetExpectedAdjacencyLists(edges, sut.CountOfNodes);
			foreach( var node in Enumerable.Range(0, sut.CountOfNodes))
				Assert.Equal(expectedAdjacencyLists[node], sut.GetAdjacentNodes(node));
		}

		public LinkedList<int>[] GetExpectedAdjacencyLists(TEdge[] edges, int countOfNodes)
		{
			var adjacencyList = Enumerable.Range(0, countOfNodes)
				.Select(s => new LinkedList<int>())
				.ToArray();

			foreach (var e in edges)
				AddEdge(e, adjacencyList);

			return adjacencyList;
		}

		protected abstract void AddEdge(TEdge e, LinkedList<int>[] adjacencyList);

		#endregion AddEdge

		#region Bfs

		[Fact]
		public void Bfs_GraphWith1Node_ReturnsNTreeWith1Node()
		{
			// Arrange
			var countOfNodes = 1;
			var expectedResult = new NTree<int>(0);

			var sut = GetGraph(countOfNodes);

			// Act
			var result = sut.Bfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		[Fact]
		public void Bfs_GraphWith2Nodes_ReturnsNTreeWith2Levels1NodePerLevel()
		{
			// Arrange
			var expectedResult = new NTree<int>(0, new List<NTree<int>>
			{
				new NTree<int>(1),
			});

			var countOfNodes = 2;
			var sut = GetGraph(countOfNodes);
			var edge = GetEdge(0, 1);
			sut.AddEdge(edge);

			// Act
			var result = sut.Bfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		[Fact]
		public void Bfs_Graph_ReturnsExpectedExpansionTree()
		{
			// Arrange
			var expectedResult = new NTree<int>(0, new List<NTree<int>>
			{
				new NTree<int>(1, new List<NTree<int>>
				{
					new NTree<int>(3, new List<NTree<int>>
					{
						new NTree<int>(5),
						new NTree<int>(6),
					}),
				}),
				new NTree<int>(2, new List<NTree<int>>
				{
					new NTree<int>(4, new List<NTree<int>>
					{
						new NTree<int>(7),
						new NTree<int>(8),
						new NTree<int>(9),
					}),
				}),
			});

			var countOfNodes = 10;
			var sut = GetGraph(countOfNodes);
			sut.AddEdge(GetEdge(0, 1));
			sut.AddEdge(GetEdge(1, 3));
			sut.AddEdge(GetEdge(3, 5));
			sut.AddEdge(GetEdge(3, 6));
			sut.AddEdge(GetEdge(0, 2));
			sut.AddEdge(GetEdge(2, 4));
			sut.AddEdge(GetEdge(4, 7));
			sut.AddEdge(GetEdge(4, 8));
			sut.AddEdge(GetEdge(4, 9));

			// Act
			var result = sut.Bfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		// Todo: Add more complex cases. 
		//		 Differences answers on directed and undirected graphs.

		#endregion Bfs

		#region Dfs

		[Fact]
		public void Dfs_GraphWith1Node_ReturnsNTreeWith1Node()
		{
			// Arrange
			var expectedResult = new NTree<int>(0);

			var countOfNodes = 1;
			var sut = GetGraph(countOfNodes);

			// Act
			var result = sut.Dfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		[Fact]
		public void Dfs_GraphWith2Nodes_ReturnsNTreeWith2Levels1NodePerLevel()
		{
			// Arrange
			var expectedResult = new NTree<int>(0, new List<NTree<int>>
			{
				new NTree<int>(1),
			});

			var countOfNodes = 2;
			var sut = GetGraph(countOfNodes);
			sut.AddEdge(GetEdge(0, 1));

			// Act
			var result = sut.Dfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		[Fact]
		public void Dfs_Graph_ReturnsExpectedExpansionTree()
		{
			// Arrange
			var expectedResult = new NTree<int>(0, new List<NTree<int>>
				{
					 new NTree<int>(1, new List<NTree<int>>
					 {
						new NTree<int>(3, new List<NTree<int>>
						{
							new NTree<int>(2, new List<NTree<int>>
							{
								new NTree<int>(4, new List<NTree<int>>
								{
									new NTree<int>(7, new List<NTree<int>>
									{
										new NTree<int>(5),
										new NTree<int>(6),
									}),
									new NTree<int>(8),
									new NTree<int>(9),
								}),
							}),
						}),
					 }),
				});

			var countOfNodes = 10;
			var sut = GetGraph(countOfNodes);
			sut.AddEdge(GetEdge(0, 1));
			sut.AddEdge(GetEdge(1, 3));
			sut.AddEdge(GetEdge(3, 2));
			sut.AddEdge(GetEdge(2, 4));
			sut.AddEdge(GetEdge(4, 7));
			sut.AddEdge(GetEdge(7, 5));
			sut.AddEdge(GetEdge(7, 6));
			sut.AddEdge(GetEdge(4, 8));
			sut.AddEdge(GetEdge(4, 9));

			// Act
			var result = sut.Dfs(0);

			// Assert
			Assert.True(expectedResult.Equals(result));
		}

		// Todo: Add more complex cases. 
		//		 Differences answers on directed and undirected graphs.

		#endregion Dfs
	}
}
