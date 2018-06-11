namespace CommonPatterns.Core.FactoryPattern
{
	public interface ISimpleFactory
	{
		IFactoryTarget Create(string name);
	}
}