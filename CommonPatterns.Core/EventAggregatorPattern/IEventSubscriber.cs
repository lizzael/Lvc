namespace CommonPatterns.Core.EventAggregatorPattern
{
	public interface IEventSubscriber<T>
	{
		void OnEvent(T e);
	}
}
