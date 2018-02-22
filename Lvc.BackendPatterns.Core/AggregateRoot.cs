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
		protected List<IDomainEvent> Events { get; set; }
	}
}
