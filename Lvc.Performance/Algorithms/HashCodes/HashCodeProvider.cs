using System.Linq;
using Lvc.Performance.Core.Algorithms.HashCodes;

namespace Lvc.Performance.Algorithms.HashCodes
{
	public class HashCodeProvider : IHashCodeProvider
	{
		public HashCodeProvider(int prime1 = 7, int prime2 = 71)
		{
			Prime1 = prime1;
			Prime2 = prime2;
		}

		public int Prime1 { get; protected set; }

		public int Prime2 { get; protected set; }

		public int GetHashCode(params object[] arr) =>
			arr.Aggregate(Prime1, (result, current) =>
			{
				unchecked
				{
					var hashCode = current == null
						? 0
						: current.GetHashCode();
					return result * Prime2 + hashCode;
				}
			});
	}
}
