using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lvc.Extensions
{
	public static class StringExtensions
	{
		public static T ConvertTo<T>(this string s)
		{
			Validate.NotNullReference(s);

			return (T)Convert.ChangeType(s, typeof(T), CultureInfo.InvariantCulture);
		}

		public static IEnumerable<T> ConvertToMany<T>(
            this string str, 
            params char[] separators)
		{
			Validate.NotNullReference(str);

			return str.Split(separators)
				.Select(s => s.ConvertTo<T>());
		}

		public static IEnumerable<T> ConvertToMany<T>(
            this string str, 
            params string[] separators)
		{
			Validate.NotNullReference(str);

			return str.Split(separators, StringSplitOptions.None)
				.Select(s => s.ConvertTo<T>());
		}
    }
}
