using Lvc.Performance.ADTs.Graphs.Edges;

namespace Lvc.Performance.Tests.ADTs.Graphs.Edges
{
	public class DirectedEdgeTests : EdgeTests
	{
		public override GetEdge GetEdge =>
			(n1, n2, cost) => new DirectedEdge(n1, n2, cost);
	}
}
