using Lvc.Performance.Core.ADTs.Graphs.Edges;
using System;
using System.Collections.Generic;

namespace Lvc.Performance.Core.ADTs.Graphs
{
	public interface IUndirectedGraph : IGraph<IUndirectedEdge>
	{
		IEnumerable<IUndirectedEdge> Kruskal(Comparison<IUndirectedEdge> comparison);
	}
}