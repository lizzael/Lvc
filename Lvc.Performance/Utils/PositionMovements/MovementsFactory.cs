using Lvc.Performance.Core.Utils;
using Lvc.Performance.Core.Utils.PositionMovements;

namespace Lvc.Performance.Utils.PositionMovements
{
    public class MovementsFactory : IMovementsFactory
	{
		public IMovements Create(
			IPosition[] relativePositions, 
			IPositionFactory positionFactory
		) => 
			new Movements(
				relativePositions,
				positionFactory);
	}
}
