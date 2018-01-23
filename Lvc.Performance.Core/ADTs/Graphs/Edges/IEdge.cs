using System;

namespace Lvc.Performance.Core.ADTs.Graphs.Edges
{
	public interface IEdge : ICloneable
	{
		int V1 { get; }
		int V2 { get; }
		int Cost { get; set; }
	}
}