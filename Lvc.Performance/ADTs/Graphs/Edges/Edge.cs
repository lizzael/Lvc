using Lvc.Performance.Algorithms.HashCodes;
using Lvc.Performance.Core.ADTs.Graphs.Edges;
using Lvc.Performance.Core.Algorithms.HashCodes;
using System;

namespace Lvc.Performance.ADTs.Graphs.Edges
{
	public abstract class Edge : IEdge, IEquatable<Edge>
	{
		public static IIntsHashCodeProvider IntHashCodeProvider => 
			new IntsHashCodeProvider();

		public int V1 { get; protected set;  }
		public int V2 { get; protected set; }
		public int Cost { get; set; }

		public Edge(int v1, int v2, int weight = 1)
		{
			Validate.GreaterThan(v1, -1, nameof(v1));
			Validate.GreaterThan(v2, -1, nameof(v2));

			V1 = v1;
			V2 = v2;
			Cost = weight;
		}

		#region Equals, HashCode, and ToString

		public override bool Equals(object obj) =>
			Equals(obj as Edge);

		public override int GetHashCode() =>
			IntHashCodeProvider.GetHashCode(V1, V2, Cost);

		#endregion Equals, HashCode, and ToString

		public bool Equals(Edge item) =>
			item != null
			&& GetType() == item.GetType()
			&& V1 == item.V1 && V2 == item.V2 && Cost == item.Cost;

		public object Clone() =>
			MemberwiseClone();
	}
}
