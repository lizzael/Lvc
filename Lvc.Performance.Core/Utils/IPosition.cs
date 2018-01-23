namespace Lvc.Performance.Core.Utils
{
	public interface IPosition
	{
		int X { get; }
		int Y { get; }

		IPosition Add(IPosition pos);
		IPosition Sub(IPosition pos);
		IPosition Mult(int v);
		IPosition Div(int v);
		bool IsInside(int minX, int minY, int maxX, int maxY);
	}
}