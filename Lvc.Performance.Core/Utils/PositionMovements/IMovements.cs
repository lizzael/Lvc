using System.Collections.Generic;

namespace Lvc.Performance.Core.Utils.PositionMovements
{
	public interface IMovements
	{
		IPosition[] RelativePositions { get; }
		IPositionFactory PositionFactory { get; }

		IEnumerable<IPosition> GetMovements(
			IPosition pos, 
			int minX = int.MinValue, int minY = int.MinValue, 
			int maxX = int.MaxValue, int maxY = int.MaxValue);
	}
}
