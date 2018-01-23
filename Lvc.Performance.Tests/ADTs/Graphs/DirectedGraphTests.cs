using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ADTs.Graphs.Edges;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System.Collections.Generic;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Graphs
{
	public class DirectedGraphTests : CommonGraphTests<IDirectedEdge>
	{
		public override GetGraph<IDirectedEdge> GetGraph =>
			countOfNodes => new DirectedGraph(countOfNodes);

		public override IDirectedEdge GetEdge(int n1, int n2, int cost = 1) =>
			new DirectedEdge(n1, n2, cost);

		#region AddEdge

		protected override void AddEdge_NodesWithoutOtherEdges_OtherAsserts(
			IDirectedEdge e, Graph<IDirectedEdge> sut)
		{
			Assert.Null(sut.GetCost(e.V2, e.V1));
			Assert.Empty(sut.GetAdjacentNodes(e.V2));
		}

		protected override void AddEdge_XToY_And_YToX_OtherAsserts(
			IDirectedEdge xToY, IDirectedEdge yToX, Graph<IDirectedEdge> sut)
		{
			Assert.Equal(xToY.Cost, sut.GetCost(xToY.V1, xToY.V2));
			Assert.Equal(yToX.Cost, sut.GetCost(yToX.V1, yToX.V2));
			Assert.Equal(new[] { xToY, yToX }, sut.Edges);
		}

		protected override void AddEdge(IDirectedEdge e, LinkedList<int>[] adjacencyList) =>
			adjacencyList[e.V1].AddLast(e.V2);

		#endregion AddEdge
	}
}
