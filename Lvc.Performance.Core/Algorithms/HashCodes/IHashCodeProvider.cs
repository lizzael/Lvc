namespace Lvc.Performance.Core.Algorithms.HashCodes
{
	public interface IHashCodeProvider
	{
		int Prime1 { get; }
		int Prime2 { get; }

		int GetHashCode(params object[] arr);
	}
}