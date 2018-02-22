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
	public abstract class AggregateRoot<TKey> : IEquatable<AggregateRoot<TKey>>
	{
		public TKey Id { get; protected set; }

		protected AggregateRoot() {}

		public AggregateRoot(TKey id) {
			if (Equals(id, default(TKey)))
			{
				var paramName = nameof(id);
				throw new ArgumentException($"{paramName} can not have the default value", paramName);
			}
			Id = id;
		}

		public override int GetHashCode() =>
			Id.GetHashCode();

		public override bool Equals(object obj) =>
			Equals(obj as AggregateRoot<TKey>);

		public bool Equals(AggregateRoot<TKey> entity) =>
			entity == null
				? false
				: Equals(Id, entity.Id);

		public static bool operator ==(AggregateRoot<TKey> x, AggregateRoot<TKey> y) =>
			Equals(x, y);

		public static bool operator !=(AggregateRoot<TKey> x, AggregateRoot<TKey> y) =>
			!(x == y);

	}
}
