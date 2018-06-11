using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonPatterns.Core.EventAggregatorPattern;

namespace CommonPatterns.EventAggregatorPattern
{
	public class SimpleEventAggregator : IEventAggregator
	{
		private readonly object _lock = new object();
		private readonly Type ISubscriberType = typeof(IEventSubscriber<>);
		private readonly Dictionary<Type, List<object>> _eventSubscribers = new Dictionary<Type, List<object>>();

		public void Publish<TEvent>(TEvent eventToPublish)
		{
			var eventSubscriberType = ISubscriberType.MakeGenericType(typeof(TEvent));
			var eventSubscribers = GetEventSubscribers(eventSubscriberType)
				.Cast<IEventSubscriber<TEvent>>();
			foreach (var eventSubscriber in eventSubscribers)
			{
				var synchContext = SynchronizationContext.Current ?? new SynchronizationContext();
				synchContext.Post(s => eventSubscriber.OnEvent(eventToPublish), null);
			}

		}

		public void Subscribe<T>(IEventSubscriber<T> eventSubscriber)
		{
			lock (_lock)
			{
				var eventSubscribersType = GetEventSubscriberTypes(eventSubscriber);
				foreach (var eventSubscriberType in eventSubscribersType)
				{
					var eventSubscribers = GetEventSubscribers(eventSubscriberType);
					eventSubscribers.Add(eventSubscriber);
				}
			}
		}

		public void Unsubscribe<T>(IEventSubscriber<T> eventSubscriber)
		{
			lock (_lock)
			{
				var eventSubscribersType = GetEventSubscriberTypes(eventSubscriber);
				foreach (var eventSubscriberType in eventSubscribersType)
				{
					var eventSubscribers = GetEventSubscribers(eventSubscriberType);
					eventSubscribers.Remove(eventSubscriber);
				}
			}
		}

		private IEnumerable<Type> GetEventSubscriberTypes(object eventSubscriber)
		=>
			eventSubscriber
				.GetType()
				.GetInterfaces()
				.Where(w => w.IsGenericType && w.GetGenericTypeDefinition() == ISubscriberType);

		private List<object> GetEventSubscribers(Type eventSubscriberType)
		{
			List<object> eventSubscribers = null;
			lock (_lock)
			{
				var found = _eventSubscribers.TryGetValue(eventSubscriberType, out eventSubscribers);
				if (!found)
				{
					eventSubscribers = new List<object>();
					_eventSubscribers.Add(eventSubscriberType, eventSubscribers);
				}
			}

			return eventSubscribers;
		}
	}
}
