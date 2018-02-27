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
		private static List<Type> _staticHandlers;
		
		/// <summary>
		/// Call this method on the "StartApp".
		/// </summary>
		public static void Init()
		{
			var types = Assembly
				.GetExecutingAssembly()
				.GetTypes();

			_staticHandlers =
				types
				.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>)))
				.ToList();
		}

		public static void Dispatch<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
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
