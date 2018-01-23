using System.Collections.Generic;
using System.IO;

namespace Lvc.Core.IO
{
	public interface IScanner
	{
		TextReader In { get; }

		T ReadLine<T>();
		IEnumerable<T> ReadLineData<T>(char splitChar = ' ');
	}
}