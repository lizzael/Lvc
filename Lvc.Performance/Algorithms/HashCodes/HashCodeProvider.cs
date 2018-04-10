using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

		public int GetHashCode(object obj)
		{
			FieldInfo[] GetTypeFields(Type type)
			=>
				type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			IEnumerable<object> GetFieldsValues()
			{
				var type = obj.GetType();
				var objetType = typeof(object);
				while (type != objetType)
				{
					foreach (var fieldInfo in GetTypeFields(type))
						yield return fieldInfo.GetValue(obj);

					type = type.BaseType;
				}
			}

			var fieldsValues = GetFieldsValues();
			return HashCodeSolver(fieldsValues);
		}

		public int GetHashCode(params object[] fieldsValues)
		=>
			HashCodeSolver(fieldsValues);

		protected int HashCodeSolver(IEnumerable<object> fieldsValues)
		=>
			fieldsValues
			.Where(w => w != null)
			.Aggregate(Prime1, (result, fieldValue) =>
			{
				unchecked
				{
					return result * Prime2 + fieldValue.GetHashCode();
				}
			});
	}
}
