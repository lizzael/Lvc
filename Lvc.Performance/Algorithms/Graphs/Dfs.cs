using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ATDs.Trees;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Graphs
{
	internal static class Dfs
	{
		internal static NTree<int> Execute<TEdge>(Graph<TEdge> graph, int startingNode)
			where TEdge : IEdge
		{
			var root = new NTree<int>(startingNode);

			var marked = new bool[graph.CountOfNodes];
			marked[startingNode] = true;

			var stack = new Stack<NTree<int>>(graph.CountOfNodes);
			stack.Push(root);

			var adjacencyQueues = Enumerable.Range(0, graph.CountOfNodes)
				.Select(s => new Queue<int>(graph.GetAdjacentNodes(s)))
				.ToArray();
			while (stack.Count > 0)
			{
				NTree<int> currentTree = stack.Peek();

				var childIndex = GetNotMarkedChildIndex(marked, adjacencyQueues[currentTree.Value]);
				if (childIndex != -1)
				{
					var child = new NTree<int>(childIndex);
					currentTree.ChildsList.Add(child);
					stack.Push(child);

					marked[childIndex] = true;
				}
				else
					stack.Pop();
			}

			return root;
		}

		private static int GetNotMarkedChildIndex(bool[] marked, Queue<int> adjacencyQueue)
		{
			while (adjacencyQueue.Any())
			{
				var childIndex = adjacencyQueue.Dequeue();
				if (!marked[childIndex])
					return childIndex;
			}

			return -1;
		}
	}
}
