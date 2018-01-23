using Lvc.Performance.Core.Algorithms.HashCodes;
using System.Linq;

namespace Lvc.Performance.Algorithms.HashCodes
{
	public class IntsHashCodeProvider : IIntsHashCodeProvider
	{
		public IntsHashCodeProvider(int prime1 = 7, int prime2 = 71)
		{
			Prime1 = prime1;
			Prime2 = prime2;
		}

		public int Prime1 { get; protected set; }

		public int Prime2 { get; protected set; }

		public int GetHashCode(params int[] arr) =>
			arr.Aggregate(Prime1, (result, current) => 
			{
				unchecked
				{
					return result * Prime2 + current;
				}
			});
	}
}
