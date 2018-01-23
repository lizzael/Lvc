using Lvc.Performance.Core.ADTs.DisjointSets;
using System.Linq;

namespace Lvc.Performance.ADTs.DisjointSets
{
	public class DisjointSet : IDisjointSet
	{
		protected Data[] _items;
		public IData[] Items => 
			_items;

		public int Count { get; protected set; }

		public DisjointSet(int count)
		{
			Validate.GreaterThan(count, -1, nameof(count));

			Count = count;
			_items = Enumerable.Range(0, count)
				.Select(p => new Data(p))
				.ToArray();
		}

		public int FindRoot(int p)
		{
			Validate.CheckRange(p, 0, Count, nameof(p));

			if (_items[p].Parent != p)
				_items[p].Parent = FindRoot(_items[p].Parent);

			return _items[p].Parent;
		}

		public void Union(int p1, int p2)
		{
			var r1 = FindRoot(p1);
			var r2 = FindRoot(p2);
			if (r1 == r2)
				return;

			if (_items[r1].Rank < _items[r2].Rank)
				_items[r1].Parent = r2;
			else
				if (_items[r1].Rank > _items[r2].Rank)
					_items[r2].Parent = r1;
				else
				{
					_items[r2].Parent = r1;
					_items[r1].Rank++;
				}
		}

		public class Data : IData
		{
			public int Parent { get; protected internal set; }

			public int Rank { get; protected internal set; }

			protected internal Data(int parent)
			{
				Parent = parent;
				Rank = 0;
			}

			public override string ToString()
			{
				return $"(P: {Parent}, R: {Rank})";
			}
		}
	}
}
