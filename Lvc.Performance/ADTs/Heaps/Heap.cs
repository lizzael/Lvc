using Lvc.Performance.Core.ADTs.Heaps;
using Lvc.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.ADTs.Heaps
{
	public class Heap<T> : IHeap<T>
	{
		protected T[] _items;
		protected IDictionary<T, int> _positions;

		public int Count { get; protected set; }

		public int Capacity { get; protected set; }

		public IComparer<T> Comparer { get; set; }

		public Heap(int capacity, IComparer<T> comparer = null)
		{
			Validate.GreaterThan(capacity, -1, nameof(capacity));

			Count = 0;
			Capacity = capacity;
			Comparer = comparer ?? Comparer<T>.Default;

			_items = new T[capacity + 1];
			_positions = new Dictionary<T, int>(capacity);
		}

		public bool IsEmpty => Count == 0;

		public bool IsFull => Count == Capacity;

		public T Peek()
		{
			Validate.NotValidState(IsEmpty, "The heap is empty.");

			return _items[1];
		}

		public void Insert(T x)
		{
			Validate.NotValidState(IsFull, "The heap is full.");

			_items[++Count] = x;
			_positions[x] = Count;

			MoveUp(Count);
		}

		public void Insert(IEnumerable<T> items)
		{
			Validate.NotValidState(items.Count() > Capacity - Count, "items.Count()");

			foreach (var x in items)
				Insert(x);
		}

		public T Delete()
		{
			var t = Peek();

			DeleteAt(1);
			MoveDown(1);

			return t;
		}

		public T[] Delete(int n)
		{
			Validate.NotValidState(n < 0 || n > Count, nameof(n));

			return Enumerable.Range(0, n)
				.Select(s => Delete())
				.ToArray();
		}

		public bool DeleteItem(T v)
		{
			int p;
			if (!_positions.TryGetValue(v, out p))
				return false;

			DeleteAt(p);
			MoveDown(p);

			return true;
		}

		protected void DeleteAt(int p)
		{
			_positions.Remove(_items[p]);
			_items[p] = _items[Count];
			_positions[_items[Count--]] = p;
		}

		protected void MoveUp(int p)
		{
			while (true)
			{
				var father = p / 2;
				if (father == 0 || Comparer.Compare(_items[father], _items[p]) <= 0)
					return;

				Swap(p, father);

				p = father;
			}
		}

		protected void MoveDown(int p)
		{
			while (true)
			{
				int minIndex = GetMinChildIndex(p);
				if (minIndex == -1 || Comparer.Compare(_items[p], _items[minIndex]) <= 0)
					return;

				Swap(p, minIndex);

				p = minIndex;
			}
		}

		private void Swap(int p1, int p2)
		{
			Tools.Swap(ref _items[p1], ref _items[p2]);

			_positions[_items[p1]] = p1;
			_positions[_items[p2]] = p2;
		}

		protected int GetMinChildIndex(int p)
		{
			var firstChild = 2 * p;
			if (firstChild > Count)
				return -1;

			var secondChild = firstChild + 1;
			if (secondChild > Count)
				return firstChild;

			return Comparer.Compare(_items[firstChild], _items[secondChild]) < 0
				? firstChild
				: secondChild;
		}
	}
}
