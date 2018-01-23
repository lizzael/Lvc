using Lvc.Performance.ADTs.Graphs.Edges;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using Lvc.Utils;

namespace Lvc.Performance.Tests.ADTs.Graphs.Edges
{
	public class UndirectedEdgeTests : EdgeTests
	{
		public override GetEdge GetEdge =>
			(n1, n2, cost) => new UndirectedEdge(n1, n2, cost);

		protected override void AssertEdgeCreation(int v1, int v2, int cost, IEdge sut) 
		{
			if (v1 > v2)
				Tools.Swap(ref v1, ref v2);

			base.AssertEdgeCreation(v1, v2, cost, sut);
		}
	}
}
