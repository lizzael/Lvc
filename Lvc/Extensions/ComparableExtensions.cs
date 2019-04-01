using System;

namespace Lvc.Extensions
{
    public static class ComparableExtensions
    {
		public static bool IsInRange<T>(
            this IComparable<T> value, 
            T minInclusive, 
            T maxExclusive)
			where T : IComparable<T>
		{
			Validate.NotNullReference(value);

			return value.CompareTo(minInclusive) >= 0 && value.CompareTo(maxExclusive) < 0;
		}
	}
}
