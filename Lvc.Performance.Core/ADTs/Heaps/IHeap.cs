using System.Collections.Generic;

namespace Lvc.Performance.Core.ADTs.Heaps
{
    public interface IHeap<T>
	{
		int Capacity { get; }
		IComparer<T> Comparer { get; set; }
		int Count { get; }
		bool IsEmpty { get; }
		bool IsFull { get; }

		T Delete();
		T[] Delete(int count);
		bool DeleteItem(T item);
		void Insert(T x);
		void Insert(IEnumerable<T> items);
		T Peek();
	}
}