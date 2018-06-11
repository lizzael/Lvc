using CommonPatterns.Core.EventAggregatorPattern;

namespace CommonPatterns.EventAggregatorPattern
{
	public abstract class Subscriber<T> : IEventSubscriber<T>
	{
		public IEventAggregator EventAggregator { get; }

		public Subscriber(IEventAggregator eventAggregator)
		{
			EventAggregator = eventAggregator;
			EventAggregator.Subscribe(this);
		}

		public abstract void OnEvent(T e);
	}
}
