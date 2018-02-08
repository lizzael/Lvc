using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
	public class Entity<TId> : IEquatable<Entity<TId>>
	{
		public TId Id { get; protected set; }

		protected Entity() {}

		public Entity(TId id) {
			if (object.Equals(id, default(TId)))
			{
				var paramName = nameof(id);
				throw new ArgumentException($"{paramName} can not have the default value", paramName);
			}
			Id = id;
		}

		public override int GetHashCode() =>
			Id.GetHashCode();

		public override bool Equals(object obj)
		{
			var entity = obj as Entity<TId>;
			return entity == null
				? false
				: Equals(entity);
		}

		public bool Equals(Entity<TId> entity) =>
			object.Equals(Id, entity.Id);
	}
}
