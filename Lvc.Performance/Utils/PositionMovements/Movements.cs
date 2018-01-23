using Lvc.Performance.Core.Utils;
using Lvc.Performance.Core.Utils.PositionMovements;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Utils.PositionMovements
{
    public class Movements : IMovements
	{
		public IPosition[] RelativePositions { get; protected set; }
		public IPositionFactory PositionFactory { get; protected set; }

		public Movements(
			IPosition[] relativePositions,
			IPositionFactory positionFactory)
		{
			Validate.NotNull(relativePositions, nameof(relativePositions));
			Validate.NotNull(positionFactory, nameof(positionFactory));

			RelativePositions = relativePositions;
			PositionFactory = positionFactory;
		}

		public IEnumerable<IPosition> GetMovements(
			IPosition pos,
			int minX = int.MinValue, int minY = int.MinValue, 
			int maxX = int.MaxValue, int maxY = int.MaxValue
		) =>
			RelativePositions
				.Select(s => pos.Add(s))
				.Where(w => w.IsInside(minX, minY, maxX, maxY));
	}
}
