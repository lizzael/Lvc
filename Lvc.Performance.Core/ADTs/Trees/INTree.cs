using Lvc.Performance.Core.ADTs.Trees;
using System.Collections.Generic;

namespace Lvc.Performance.Core.ATDs.Trees
{
	public interface INTree<TValue, TTree> : ITree<TValue, TTree>
		where TTree : INTree<TValue, TTree>
	{
		IList<TTree> ChildsList { get; }
	}
}