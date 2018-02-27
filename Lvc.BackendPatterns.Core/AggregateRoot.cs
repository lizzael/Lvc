using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.Events;

namespace Lvc.BackendPatterns.Core
{
	public abstract class AggregateRoot<TKey> : Entity<TKey>
	{
		public virtual int Version { get; set; }
		protected LinkedList<IDomainEvent> DomainEvents { get; private set; }

		protected AggregateRoot() =>
			DomainEvents = new LinkedList<IDomainEvent>();

		protected virtual void AddDomainEvent(IDomainEvent domainEvent) =>
			DomainEvents.AddLast(domainEvent);

		public virtual void ClearEvents() =>
			DomainEvents.Clear();

		/// <summary>
		/// To be executed right after the AggregateRoot is succesfully persisted.
		/// </summary>
		public void DispatchEvents()
		{
			foreach (var domainEvent in DomainEvents)
				DomainEventsDispatcher.Dispatch(domainEvent);

			ClearEvents();
		}
	}
}
