using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPatterns.Core.ChainOfResponsibility
{
	public interface IIndividualHandler<TResponse, TData>
	{
		/// <summary>
		/// Implement individual handler logic here.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		TResponse Handle(TData data);
	}
}
