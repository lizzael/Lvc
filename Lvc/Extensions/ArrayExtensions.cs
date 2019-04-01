using System;

namespace Lvc.Extensions
{
	public static class ArrayExtensions
	{
		public static int FindIndex<T>(
            this T[] array, 
            T t)
		{
			Validate.NotNullReference(array);

			return Array.FindIndex(array, x => Equals(x, t));
		}
	}
}