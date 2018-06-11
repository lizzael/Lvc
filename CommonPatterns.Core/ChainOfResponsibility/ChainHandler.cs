namespace CommonPatterns.Core.ChainOfResponsibility
{
	public abstract class ChainHandler<TResponse, TData>
	{
		public IIndividualHandler<TResponse, TData> IndividualHandler { get; }

		public ChainHandler(IIndividualHandler<TResponse, TData> individualHandler)
		=>
			IndividualHandler = individualHandler;

		/// <summary>
		/// Use individual handler and decides and ends (LastHandler) or call to Next Handler (RegularHandler).
		/// Return can be composite or not.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public abstract TResponse Handle(TData data);
	}
}
