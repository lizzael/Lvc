using Lvc.Core.IO;
using Lvc.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lvc.IO
{
	public class Scanner : IScanner
	{
		public TextReader In { get; protected set; }

		public Scanner() =>
			In = Console.In;

		public Scanner(TextReader textReader)
		{
			Validate.NotNull(textReader, nameof(textReader));

			In = textReader;
		}

		public T ReadLine<T>() =>
			In.ReadLine().ConvertTo<T>();

		public IEnumerable<T> ReadLineData<T>(char splitChar = ' ') =>
			In.ReadLine().Split(splitChar)
				.Select(s => s.ConvertTo<T>());
	}
}
