using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ATDs.Trees;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Graphs
{
	internal static class Bfs
	{
		internal static NTree<int> Execute<TEdge>(Graph<TEdge> graph, int startingNode)
			where TEdge : IEdge
		{
			var root = new NTree<int>(startingNode);

			var marked = new bool[graph.CountOfNodes];
			marked[startingNode] = true;

			var queue = new Queue<NTree<int>>(graph.CountOfNodes);
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				var currentTree = queue.Dequeue();
				var notVisitedQuery = graph.GetAdjacentNodes(currentTree.Value)
					.Where(w => !marked[w]);
				foreach (var childIndex in notVisitedQuery)
				{
					marked[childIndex] = true;

					var child = new NTree<int>(childIndex);
					currentTree.ChildsList.Add(child);

					queue.Enqueue(child);
				}
			}

			return root;
		}
	}
}
