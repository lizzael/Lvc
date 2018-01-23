namespace Lvc.Performance.Core.ADTs.DisjointSets
{
	public interface IDisjointSet
	{
		int Count { get; }

		int FindRoot(int p);
		void Union(int p1, int p2);
	}

	public interface IData
	{
		int Parent { get; }
		int Rank { get; }

		string ToString();
	}
}