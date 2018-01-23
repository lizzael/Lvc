using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System.Collections.Generic;

namespace Lvc.Performance.Core.ADTs.Graphs
{
	public interface IGraph<TEdge>
		where TEdge : IEdge
	{
		int CountOfNodes { get; set; }
		IEnumerable<TEdge> Edges { get; }

		void AddEdge(TEdge edge);
		IEnumerable<int> GetAdjacentNodes(int n);
		int? GetCost(int n1, int n2);
	}
}