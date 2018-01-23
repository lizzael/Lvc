using System;

namespace Lvc.Performance.Core.Algorithms.Sorting
{
    public interface ISorting<T>
	{
		Comparison<T> Comparison { get; }

		T[] Sort(T[] items);
	}
}
