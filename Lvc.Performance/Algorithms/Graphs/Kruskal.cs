using Lvc.Performance.ADTs.DisjointSets;
using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Graphs
{
	internal static class Kruskal
	{
		internal static IEnumerable<IUndirectedEdge> Execute(
			UndirectedGraph graph, Comparison<IUndirectedEdge> comparison)
		{
			Validate.NotNull(comparison, nameof(comparison));
					
			var queue = GetSortedEdgesQueue(graph.Edges.ToArray(), comparison);
			return Execute(queue, graph.CountOfNodes);
		}

		private static IEnumerable<IUndirectedEdge> Execute(
			Queue<IUndirectedEdge> sortedQueue, int countOfNodes)
		{
			var disjointSet = new DisjointSet(countOfNodes);
			for (var p = 1; p < countOfNodes; p++)
			{
				var edgeToAdd = GetEdgeToAdd(sortedQueue, disjointSet);
				if (edgeToAdd == null)
					break;

				disjointSet.Union(edgeToAdd.V1, edgeToAdd.V2);

				yield return edgeToAdd;
			}
		}

		private static Queue<IUndirectedEdge> GetSortedEdgesQueue(
			IUndirectedEdge[] edges, Comparison<IUndirectedEdge> comparison)
		{
			Array.Sort(edges, comparison);

			return new Queue<IUndirectedEdge>(edges);
		}

		private static IUndirectedEdge GetEdgeToAdd(Queue<IUndirectedEdge> queue, DisjointSet disjointSet)
		{
			while (queue.Any())
			{
				var edge = queue.Dequeue();
				if (disjointSet.FindRoot(edge.V1) != disjointSet.FindRoot(edge.V2))
					return edge;
			}

			return null;
		}
	}
}
