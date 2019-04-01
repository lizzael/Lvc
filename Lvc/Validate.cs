using Lvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc
{
    public static class Validate
    {
		public static void NotNullReference(object obj)
		{
			if (obj == null)
				throw new NullReferenceException();
		}

		public static void NotNull(
            object obj, 
            string paramName) 
		{
			if (obj == null)
				throw new ArgumentNullException(paramName);
		}

		public static void AllNotNull(
            IEnumerable<object> items, 
            string paramName)
		{
			if (items.Any(a => a == null))
				throw new ArgumentNullException(
                    paramName, 
                    "Null value found.");
		}

		public static void NotValidValue<T>(
            T value, 
            T notValidValue, 
            string paramName)
		{
			if (Equals(value, notValidValue))
				throw new ArgumentException(
                    "Not valid value.", 
                    paramName);
		}

		public static void NotValidState(
            bool notValidState, 
            string message)
		{
			if (notValidState)
				throw new InvalidOperationException(message);
		}

		public static void LowerThan<T>(
            T value, 
            T maxValue, 
            string paramName)
			where T : IComparable<T>
		{
			if (value.CompareTo(maxValue) >= 0)
				throw new ArgumentOutOfRangeException(
					paramName, 
                    value, 
                    $"must be lower than {maxValue}.");
		}

		public static void AllLowerThan<T>(
            IEnumerable<T> items, 
            T maxValue, 
            string paramName)
			where T : IComparable<T>
		{
			if (!items.All(a => a.CompareTo(maxValue) < 0))
				throw new ArgumentOutOfRangeException(
					paramName, 
                    $"All items of must be lower than {maxValue}.");
		}

		public static void GreaterThan<T>(
            T value, 
            T minValue, 
            string paramName)
			where T : IComparable<T>
		{
			if (value.CompareTo(minValue) <= 0)
				throw new ArgumentOutOfRangeException(
					paramName, 
                    value, 
                    $"must be greater than {minValue}.");
		}

		public static void AllGreaterThan<T>(
            IEnumerable<T> items, 
            T minValue, 
            string paramName)
			where T : IComparable<T>
		{
			if (!items.All(a => a.CompareTo(minValue) > 0))
				throw new ArgumentOutOfRangeException(
					paramName, 
                    $"All items of must be greater than {minValue}.");
		}

		public static void CheckRange<T>(
            T value, 
            T minInclusive, 
            T maxExclusive, 
            string paramName)
			where T : IComparable<T>
		{
			if (!value.IsInRange(minInclusive, maxExclusive))
				throw new ArgumentOutOfRangeException(
					paramName, 
                    value, 
                    $"is not in the range [{minInclusive}, {maxExclusive}).");
		}

		public static void CheckRangeAll<T>(
			IEnumerable<T> enumerable, 
            T minInclusive, 
            T maxExclusive, 
            string paramName)
			where T : IComparable<T>
		{
			if (!enumerable.All(a => a.IsInRange(minInclusive, maxExclusive)))
				throw new ArgumentOutOfRangeException(
					paramName, 
                    $"Some item is not in the range [{minInclusive}, {maxExclusive}).");
		}
	}
}
