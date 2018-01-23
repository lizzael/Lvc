using Lvc.Performance.Core.ADTs.Graphs;
using Lvc.Performance.Core.ADTs.Graphs.Edges;

namespace Lvc.Performance.ADTs.Graphs
{
	public class DirectedGraph : Graph<IDirectedEdge>, IDirectedGraph
	{
		public DirectedGraph(int countOfNodes) : base(countOfNodes)
		{
		}

		protected override void AddIt(IDirectedEdge edge)
		{
			if (_mat[edge.V1, edge.V2] == null)
			{
				_adjacencyLists[edge.V1].AddLast(edge.V2);

				_edges.AddLast(edge);

				_mat[edge.V1, edge.V2] = edge;
			}
			else
				_mat[edge.V1, edge.V2].Cost = edge.Cost;
		}
	}
}
