namespace CommonPatterns.Core.FactoryPattern
{
	public interface IAbstractFactory  // One class for type.
	{
		IFactoryTarget CreateTargetWithPackage1(); // One per package.
		IFactoryTarget CreateTargetWithPackage2();
	}
}
