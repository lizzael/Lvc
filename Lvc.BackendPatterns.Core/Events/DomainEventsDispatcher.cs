using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.Handlers;

namespace Lvc.BackendPatterns.Core.Events
{
	public static class DomainEventsDispatcher
	{
		[ThreadStatic]
		private static List<Type> _handlers;
		
		/// <summary>
		/// Call this method on the "StartApp".
		/// </summary>
		public static void Init()
		{
			var types = Assembly
				.GetExecutingAssembly()
				.GetTypes();

			_handlers =
				types
				.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>)))
				.ToList();
		}

		public static void Dispatch(IDomainEvent domainEvent)
		{
			bool IsRequiredHandler(Type @interface) =>
				@interface.IsGenericType &&
				@interface.GetGenericTypeDefinition() == typeof(IHandler<>)
				&& @interface.GetGenericArguments()[0] == domainEvent.GetType();

			bool CanHandle(Type type) =>
				type
				.GetInterfaces()
				.Any(IsRequiredHandler);

			var validHandlers = _handlers.Where(CanHandle);
			foreach (var handlerType in validHandlers)
			{
				var instance = (IHandler<IDomainEvent>)Activator.CreateInstance(handlerType);
				instance.Handle(domainEvent);
			}
		}

	}
}
