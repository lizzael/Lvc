using Lvc.Performance.ADTs.Trees;
using Lvc.Performance.Core.ATDs.Trees;
using System.Collections.Generic;

namespace Lvc.Performance.ATDs.Trees
{
	public class NTree<TValue> : Tree<TValue, NTree<TValue>>, INTree<TValue, NTree<TValue>>
	{
		public IList<NTree<TValue>> ChildsList { get; protected set; }

		public override IEnumerable<NTree<TValue>> ChildsEnumerable => ChildsList;

		public NTree()
			: this(default(TValue))
		{
		}

		public NTree(TValue t)
			: this(t, new List<NTree<TValue>>())
		{
		}

		public NTree(TValue t, IList<NTree<TValue>> childs)
			: base(t)
		{
			Validate.NotNull(childs, nameof(childs));

			ChildsList = childs;
		}
	}
}
