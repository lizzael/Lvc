using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.Events;

namespace Lvc.BackendPatterns.Core.Handlers
{
	public interface IHandle<T>
		where T : IDomainEvent
	{
		void Handle(T t);
	}
}
