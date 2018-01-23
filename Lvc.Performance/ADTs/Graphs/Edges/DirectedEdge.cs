using Lvc.Performance.Core.ADTs.Graphs.Edges;

namespace Lvc.Performance.ADTs.Graphs.Edges
{
	public class DirectedEdge : Edge, IDirectedEdge
	{
		public DirectedEdge(int v1, int v2, int weight = 1) : base(v1, v2, weight)
		{
		}

		public override string ToString() =>
			$"({V1} -> {V2}, {Cost})";
	}
}
