using Lvc.Performance.Core.Utils;

namespace Lvc.Performance.Utils
{
    public class PositionFactory : IPositionFactory
	{
		public IPosition Create(int x, int y) =>
			new Position(x, y);
	}
}
