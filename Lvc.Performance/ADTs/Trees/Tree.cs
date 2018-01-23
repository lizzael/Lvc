using Lvc.Performance.Core.ADTs.Trees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lvc.Performance.ADTs.Trees
{
	public abstract class Tree<TValue, TTree> : ITree<TValue, TTree>, IEquatable<Tree<TValue, TTree>>
		where TTree : Tree<TValue, TTree>
	{
		public TValue Value { get; set; }

		public abstract IEnumerable<TTree> ChildsEnumerable { get; }

		public Tree()
			: this(default(TValue))
		{
		}

		public Tree(TValue t)
		{
			Value = t;
		}

		public IEnumerable<TValue> PreOrder =>
			(new[] { Value }).Concat(ChildsEnumerable.SelectMany(s => s.PreOrder));

		public IEnumerable<TValue> PostOrder =>
			ChildsEnumerable.SelectMany(s => s.PostOrder).Concat(new[] { Value });

		public IEnumerable<TValue> ByLevel
		{
			get
			{
				var queue = new Queue<Tree<TValue, TTree>>();
				queue.Enqueue(this);

				while (queue.Any())
				{
					var t = queue.Dequeue();
					yield return t.Value;

					foreach (var child in t.ChildsEnumerable)
						queue.Enqueue(child);
				}
			}
		}

		public override bool Equals(object obj) =>
			Equals(obj as Tree<TValue, TTree>);

		public bool Equals(Tree<TValue, TTree> tree)
		{
			if (tree == null 
				|| !Equals(Value, tree.Value)
				|| ChildsEnumerable.Count() != tree.ChildsEnumerable.Count())
				return false;

			return ChildsEnumerable
				.Zip(tree.ChildsEnumerable, (x, y) => new { x, y })
				.All(a => Equals(a.x, a.y));
		}
	}
}
