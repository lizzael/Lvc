using Lvc.Performance.ATDs.Trees;
using Lvc.Performance.Core.ADTs.Graphs;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.ADTs.Graphs
{
	public abstract class Graph<TEdge> : IGraph<TEdge>
		where TEdge : IEdge
	{
		protected TEdge[,] _mat;
		protected LinkedList<TEdge> _edges;
		protected LinkedList<int>[] _adjacencyLists;

		public int CountOfNodes { get; set; }
		public IEnumerable<TEdge> Edges =>
			_edges.Select(s => s);

		public Graph(int countOfNodes)
		{
			Validate.GreaterThan(countOfNodes, -1, nameof(countOfNodes));

			CountOfNodes = countOfNodes;
			_mat = new TEdge[countOfNodes, countOfNodes];
			_edges = new LinkedList<TEdge>();
			_adjacencyLists = Enumerable.Range(0, countOfNodes)
				.Select(s => new LinkedList<int>())
				.ToArray();
		}

		public int? GetCost(int n1, int n2)
		{
			Validate.CheckRange(n1, 0, CountOfNodes, nameof(n1));
			Validate.CheckRange(n2, 0, CountOfNodes, nameof(n2));

			return _mat[n1, n2]?.Cost;
		}

		public IEnumerable<int> GetAdjacentNodes(int n) 
		{
			Validate.CheckRange(n, 0, CountOfNodes, nameof(n));

			return _adjacencyLists[n]
				.Select(s => s);
		}

		public NTree<int> Bfs(int startingNode) =>
			Algorithms.Graphs.Bfs.Execute(this, startingNode);

		public NTree<int> Dfs(int startingNode) =>
			Algorithms.Graphs.Dfs.Execute(this, startingNode);

		public void AddEdge(TEdge edge)
		{
			Validate.NotNull(edge, nameof(edge));
			Validate.CheckRange(edge.V1, 0, CountOfNodes, nameof(edge.V1));
			Validate.CheckRange(edge.V2, 0, CountOfNodes, nameof(edge.V2));
			Validate.NotValidValue(edge.V2, edge.V1, nameof(edge.V2));

			AddIt((TEdge)edge.Clone());
		}

		protected abstract void AddIt(TEdge edge);
	}
}
