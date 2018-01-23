using Lvc.Performance.Core.ADTs.Graphs.Edges;

namespace Lvc.Performance.ADTs.Graphs.Edges
{
	public class UndirectedEdge : Edge, IUndirectedEdge
	{
		public UndirectedEdge(int v1, int v2, int weight = 1) : base(v1, v2, weight)
		{
			if (V1 > V2)
			{
				V1 = v2;
				V2 = v1;
			}
		}

		public override string ToString() =>
			$"({V1} - {V2}, {Cost})";
	}
}
