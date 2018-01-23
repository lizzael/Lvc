using System.Collections.Generic;
using System;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using Lvc.Performance.Core.ADTs.Graphs;

namespace Lvc.Performance.ADTs.Graphs
{
	public class UndirectedGraph : Graph<IUndirectedEdge>, IUndirectedGraph
	{
		public UndirectedGraph(int countOfNodes) : base(countOfNodes)
		{
		}

		protected override void AddIt(IUndirectedEdge edge)
		{
			if (_mat[edge.V1, edge.V2] == null)
			{
				_adjacencyLists[edge.V1].AddLast(edge.V2);
				_adjacencyLists[edge.V2].AddLast(edge.V1);

				_edges.AddLast(edge);

				_mat[edge.V1, edge.V2] = _mat[edge.V2, edge.V1] = edge;
			}
			else
				_mat[edge.V1, edge.V2].Cost = _mat[edge.V2, edge.V1].Cost = edge.Cost;
		}

		public IEnumerable<IUndirectedEdge> Kruskal(Comparison<IUndirectedEdge> comparison) =>
			Algorithms.Graphs.Kruskal.Execute(
				this, comparison);

		public IEnumerable<IUndirectedEdge> Prim(Comparison<int> comparison, int startingNode) =>
			Algorithms.Graphs.Prim.Execute(
				this, comparison, startingNode);
	}
}
