using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPatterns.Core.ChainOfResponsibility
{
	public abstract class LastHandler<TResponse, TData> : ChainHandler<TResponse, TData>
	{
		public LastHandler(
			IIndividualHandler<TResponse, TData> responsableHandler)
		: base(responsableHandler)
		{
		}
	}
}
