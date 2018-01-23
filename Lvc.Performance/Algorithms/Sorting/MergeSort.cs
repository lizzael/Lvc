using System;
using System.Linq;

namespace Lvc.Performance.Algorithms.Sorting
{
	public class MergeSort<T> : SortingBase<T>
	{
		public MergeSort(Comparison<T> comparison = null)
			: base(comparison)
		{
		}

		protected override T[] Execute(T[] items)
		{
			ExecuteMergeSort(items);

			return items;
		}

		protected void ExecuteMergeSort(T[] items)
		{
			if (items.Length < 2)
				return;

			var m = items.Length / 2;

			var a1 = items.Take(m).ToArray();
			ExecuteMergeSort(a1);

			var a2 = items.Skip(m).ToArray();
			ExecuteMergeSort(a2);

			Merge(a1, a2, items);
		}

		protected void Merge(T[] a1, T[] a2, T[] arr)
		{
			int p = 0, p1 = 0, p2 = 0;
			while (p1 < a1.Length && p2 < a2.Length)
				if (Comparison(a1[p1], a2[p2]) <= 0)
					arr[p++] = a1[p1++];
				else
					arr[p++] = a2[p2++];

			while (p1 < a1.Length)
				arr[p++] = a1[p1++];

			while (p2 < a2.Length)
				arr[p++] = a2[p2++];
		}
	}
}
