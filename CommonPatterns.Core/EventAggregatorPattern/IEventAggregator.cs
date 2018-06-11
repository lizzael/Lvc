namespace CommonPatterns.Core.EventAggregatorPattern
{
	public interface IEventAggregator
	{
		void Subscribe<T>(IEventSubscriber<T> eventSubscriber);
		void Unsubscribe<T>(IEventSubscriber<T> eventSubscriber);
		void Publish<TEvent>(TEvent eventToPublish);
	}
}
