using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.Handlers;

namespace Lvc.BackendPatterns.Core.Events
{
	public static class DomainEvents
	{
		[ThreadStatic]
		private static List<Type> _staticHandlers;
		
		/// <summary>
		/// Mostly used in tests.
		/// </summary>
		[ThreadStatic]
		private static Dictionary<Type, LinkedList<Delegate>> _dynamicHandlers;

		/// <summary>
		/// Call this method on the "StartApp".
		/// </summary>
		public static void Init()
		{
			var types = Assembly
				.GetExecutingAssembly()
				.GetTypes();

			_dynamicHandlers =
				types
				.Where(type => typeof(IDomainEvent).IsAssignableFrom(type) && !type.IsInterface)
				.ToDictionary(type => type, type => new LinkedList<Delegate>());

			_staticHandlers =
				types
				.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>)))
				.ToList();
		}

		public static void Register<TDomainEvent>(Action<TDomainEvent> eventHandler)
			where TDomainEvent : IDomainEvent
		=>
			_dynamicHandlers[typeof(TDomainEvent)].AddLast(eventHandler);

		public static void ClearCallBacks() =>
			_dynamicHandlers.Clear();

		public static void Raise<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
			var selectedDynamicHandlers =
				_dynamicHandlers[domainEvent.GetType()]
				.Select(s => s as Action<TDomainEvent>)
				.Where(w => w != null);
			foreach (var action in selectedDynamicHandlers)
				action(domainEvent);

			var iHandlerType = typeof(IHandler<>);
			var selectedStaticHandlers =
				_staticHandlers
				.Where(h => iHandlerType.IsAssignableFrom(h));
			foreach (var handler in selectedStaticHandlers)
			{
				var instance = (IHandler<TDomainEvent>)Activator.CreateInstance(handler);
				instance.Handle(domainEvent);
			}
		}
	}
}
