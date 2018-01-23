namespace Lvc.Performance.Core.Algorithms.HashCodes
{
	public interface IIntsHashCodeProvider
	{
		int Prime1 { get; }
		int Prime2 { get; }

		int GetHashCode(params int[] arr);
	}
}