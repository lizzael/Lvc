using Lvc.Performance.ADTs.Graphs;
using Lvc.Performance.ADTs.Graphs.Edges;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Graphs
{
	internal static class Prim
	{
		const int MaxCost = int.MaxValue;

		internal static IEnumerable<IUndirectedEdge> Execute(
			UndirectedGraph graph, Comparison<int> comparison, int startingNode)
		{
			Validate.NotNull(comparison, nameof(comparison));
			Validate.CheckRange(startingNode, 0, graph.CountOfNodes, nameof(startingNode));

			var cost = GetCosts(graph);
			return DoIt(cost, comparison, startingNode);
		}

		private static int[][] GetCosts(UndirectedGraph graph) =>
			Enumerable.Range(0, graph.CountOfNodes)
				.Select(s1 => Enumerable.Range(0, graph.CountOfNodes)
					.Select(s2 => graph.GetCost(s1, s2) ?? MaxCost)
					.ToArray())
				.ToArray();

		private static IEnumerable<IUndirectedEdge> DoIt(
			int[][] cost, Comparison<int> comparison, int startingNode)
		{
			var countOfNodes = cost.Length;
			var primArray = GetPrimArray(cost, startingNode, countOfNodes);

			var marked = new bool[countOfNodes];
			marked[startingNode] = true;

			for (var i = 0; i < countOfNodes; i++)
			{
				var node = GetSelectedNode(primArray, marked, countOfNodes, comparison);
				if (node == -1)
					break;

				yield return new UndirectedEdge(primArray[node].FromNode, node, primArray[node].Cost);
				marked[node] = true;

				for (var p = 0; p < countOfNodes; p++)
					if (!marked[p] && comparison(primArray[p].Cost, cost[node][p]) > 0)
					{
						primArray[p].Cost = cost[node][p];
						primArray[p].FromNode = node;
					}
			}
		}

		private static PrimData[] GetPrimArray(int[][] cost, int startingNode, int countOfNodes)
		{
			return Enumerable.Range(0, countOfNodes)
				.Select(s => new PrimData
				{
					Cost = cost[startingNode][s],
					FromNode = startingNode,
				})
				.ToArray();
		}

		private static int GetSelectedNode(PrimData[] prim, bool[] marked, int countOfNodes, Comparison<int> comparison)
		{
			var selectedNode = -1;
			var selectedCost = MaxCost;
			for (var node = 0; node < countOfNodes; node++)
				if (!marked[node] && comparison(selectedCost, prim[node].Cost) > 0)
				{
					selectedNode = node;
					selectedCost = prim[node].Cost;
				}

			return selectedNode;
		}

		private class PrimData
		{
			public int Cost { get; set; }
			public int FromNode { get; set; }
		}
	}
}
