using Lvc.Performance.Core.Utils;
using Lvc.Performance.Core.Utils.PositionMovements;
using System.Collections.Generic;

namespace Lvc.Performance.Core.Algorithms.Graphs
{
	public interface ILee
	{
		ILeeResult Execute();
	}

	public interface ILeeResult
	{
		int CountOfXs { get; }
		int CountOfY { get; }
		int LastX { get; }
		int LastY { get; }
		IPosition Start { get; }
		IPosition End { get; }
		bool HasSolution { get; }
		int SolutionLength { get; }
		IMovements Movements { get; }

		IEnumerable<IPosition> GetOneRoute();
	}
}