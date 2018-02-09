using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
	/// <summary>
	/// Base class for entities.
	/// Note: This class only looks at scalar properties for comparison, not collection properties.
	/// </summary>
	/// <typeparam name="TKey">
	/// Type of the Id.
	/// </typeparam>
	public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
	{
		public TKey Id { get; protected set; }

		protected Entity() {}

		public Entity(TKey id) {
			if (Equals(id, default(TKey)))
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
			var entity = obj as Entity<TKey>;
			return entity == null
				? false
				: Equals(entity);
		}

		public bool Equals(Entity<TKey> entity) =>
			entity == null
				? false
				: Equals(Id, entity.Id);
	}
}
