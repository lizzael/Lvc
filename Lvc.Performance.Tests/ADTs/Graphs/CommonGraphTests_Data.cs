using System.Collections.Generic;

namespace Lvc.Performance.Tests.ADTs.Graphs
{
	public static class CommonGraphTests_Data
	{
		public static IEnumerable<object[]> CreateGraph =>
			new object[][] {
				new object[] {0},
				new object[] {1},
				new object[] {10},
				new object[] {100}
			};

		#region GetAjacentNodes

		public static IEnumerable<object[]> GetAjacentNodes_Given_OutOfRangeValues_Throws_ArgumentOutRangeException =>
			new object[][] {
				new object[] {0, 0},
				new object[] {10, -1},
				new object[] {10, 10},
				new object[] {10, 11}
			};

		public static IEnumerable<object[]> GetAjacentNodes_GivenNodeWithNoEdges_Returns_EmptyEnumerable =>
			new object[][] {
				new object[] {3, 0},
				new object[] {3, 1},
				new object[] {3, 2}
			};

		#endregion GetAjacentNodes

		#region GetCost

		public static IEnumerable<object[]> GetCost_GivenValuesOutOfRange_Throws_ArgumentOutOfRangeException =>
			new object[][] {
				new object[] {0, 0, 0},
				new object[] {3, 0, -1},
				new object[] {3, -1, -1},
				new object[] {3, 0, 3},
				new object[] {3, 3, 0},
				new object[] {3, 3, 3},
				new object[] {3, 0, 4},
				new object[] {3, 4, 0},
				new object[] {3, 4, 4},
			};

		public static IEnumerable<object[]> GetCost_GivenNodesWithoutEdgeBetweenThem =>
			new object[][] {
				new object[] {3, 0, 0},
				new object[] {3, 0, 1},
				new object[] {3, 0, 2},
				new object[] {3, 1, 0},
				new object[] {3, 1, 1},
				new object[] {3, 1, 2},
				new object[] {3, 2, 0},
				new object[] {3, 2, 1},
				new object[] {3, 2, 2},
			};

		#endregion GetCost

		#region AddEgde

		// Negatives values of nodes are handled by Edge.
		public static IEnumerable<object[]> AddEdge_GivenNodesOutOfRange_Throws_ArgumentOutOfRangeException =>
			new object[][] {
				new object[] {3, 0, 3},
				new object[] {3, 3, 0},
				new object[] {3, 3, 3},
				new object[] {3, 0, 4},
				new object[] {3, 4, 0},
				new object[] {3, 4, 4},
			};

		public static IEnumerable<object[]> AddEdge_GivenEqualsNodes_Throws_ArgumentException =>
			new object[][] {
				new object[] {3, 0, 0},
				new object[] {3, 1, 1},
				new object[] {3, 2, 2},
			};

		public static IEnumerable<object[]> AddEdge_AddingOneEdgeWithDefaultCost =>
			new object[][] {
				new object[] {4, 0, 1},
				new object[] {4, 0, 2},
				new object[] {4, 0, 3},
				new object[] {4, 1, 0},
				new object[] {4, 1, 2},
				new object[] {4, 1, 3},
				new object[] {4, 2, 0},
				new object[] {4, 2, 1},
				new object[] {4, 2, 3},
			};

		public static IEnumerable<object[]> AddEdge_AddingOneEdgeWithCost =>
			new object[][] {
				new object[] {4, 0, 1, -1},
				new object[] {4, 0, 2, 0},
				new object[] {4, 0, 3, 1},
				new object[] {4, 1, 0, 2},
				new object[] {4, 1, 2, 3},
				new object[] {4, 1, 3, 4},
				new object[] {4, 2, 0, 5},
				new object[] {4, 2, 1, 6},
				new object[] {4, 2, 3, 7},
			};

		#endregion AddEgde
	}
}
