using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
	public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
		where T : ValueObject<T>
	{
		public override int GetHashCode()
		{
			IEnumerable<FieldInfo> GetFields()
			{
				var type = GetType();
				var objetType = typeof(object);
				while (type != objetType)
				{
					foreach (var fieldInfo in GetTypeFields(type))
						yield return fieldInfo;

					type = type.BaseType;
				}
			}

			var fields = GetFields();

			var startValue = 17;
			var multiplier = 59;

			var sumOfValues =
				fields
				.Select(s => s.GetValue(this))
				.Where(w => w != null)
				.Select(s => s.GetHashCode())
				.Sum();

			return startValue + multiplier * sumOfValues;
		}

		public override bool Equals(object obj) =>
			Equals(obj as ValueObject<T>); 

		public bool Equals(ValueObject<T> other)
		{
			if (other == null)
				return false;

			var type = GetType();
			var otherType = other.GetType();
			var fields = GetTypeFields(type);
			return fields.All(a => object.Equals(a.GetValue(this), a.GetValue(other)));
		}

		private static FieldInfo[] GetTypeFields(Type type) =>
			type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		public static bool operator ==(ValueObject<T> x, ValueObject<T> y) =>
			Equals(x, y);

		public static bool operator !=(ValueObject<T> x, ValueObject<T> y) =>
			!(x == y);
	}
}
