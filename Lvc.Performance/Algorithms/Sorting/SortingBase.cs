using Lvc.Performance.Core.Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.Algorithms.Sorting
{
	public abstract class SortingBase<T>: ISorting<T>
	{
		public Comparison<T> Comparison { get; protected set; }

		public SortingBase(Comparison<T> comparison = null)
		{
			if (comparison == null)
			{
				var typeOfT = typeof(T);
				if (typeOfT.GetInterfaces().Contains(typeof(IComparable<T>)))
					Comparison = Comparer<T>.Default.Compare;
				else
				{
					var nameOfT = typeOfT.Name;
					throw new ArgumentException($"If comparison is null {nameOfT} have to implement IComparable<{nameOfT}>.");
				}
			}
			else
				Comparison = comparison;
		}

		public T[] Sort(T[] items)
		{
			Validate.NotNull(items, nameof(items));

			return Execute(items);
		}

		protected abstract T[] Execute(T[] items);
	}
}
