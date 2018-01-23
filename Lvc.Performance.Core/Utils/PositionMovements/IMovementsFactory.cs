namespace Lvc.Performance.Core.Utils.PositionMovements
{
	public interface IMovementsFactory
	{
		IMovements Create(
			IPosition[] relativePositions,
			IPositionFactory positionFactory);
	}
}
