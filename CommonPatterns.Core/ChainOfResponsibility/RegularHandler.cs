using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPatterns.Core.ChainOfResponsibility
{
	public abstract class RegularHandler<TResponse, TData> : ChainHandler<TResponse, TData>
	{
		public ChainHandler<TResponse, TData> NextHandler { get; }

		public RegularHandler(
			IIndividualHandler<TResponse, TData> responsableHandler,
			ChainHandler<TResponse, TData> nextHandler)
			: base(responsableHandler)
		{
			NextHandler = nextHandler;
		}
	}
}
