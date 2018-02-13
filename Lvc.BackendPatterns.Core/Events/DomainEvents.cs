using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.Handlers;

namespace Lvc.BackendPatterns.Core.Events
{
	public static class DomainEvents
	{
		[ThreadStatic]
		private static LinkedList<Delegate> _actions;

		static DomainEvents()
		{
			_actions = new LinkedList<Delegate>();
		}

		public static void Register<TDomainEvent>(Action<TDomainEvent> callback)
			where TDomainEvent : IDomainEvent
		=>
			_actions.AddLast(callback);

		public static void ClearCallBacks() =>
			_actions.Clear();

		public static void Raise<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
			var selectedActions = 
				_actions
				.Select(s => s as Action<TDomainEvent>)
				.Where(w => w != null);
			foreach (var action in selectedActions)
				action(domainEvent);
		}
	}
}
