using System.Collections.Generic;

namespace Lvc.Performance.Core.ADTs.Trees
{
	public interface ITree<TValue, TTree> where TTree : ITree<TValue, TTree>
	{
		IEnumerable<TValue> ByLevel { get; }
		IEnumerable<TTree> ChildsEnumerable { get; }
		IEnumerable<TValue> PostOrder { get; }
		IEnumerable<TValue> PreOrder { get; }
		TValue Value { get; set; }
	}
}