namespace Lvc.Performance.Core.Algorithms.Strings
{
	public interface IKmp
	{
		string Substring { get; }

		int Execute(string mainString);
	}
}