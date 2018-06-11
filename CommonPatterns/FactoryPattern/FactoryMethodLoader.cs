using System.Configuration;
using System.Reflection;
using CommonPatterns.Core.FactoryPattern;

namespace CommonPatterns.FactoryPattern
{
	public class FactoryMethodLoader : IFactoryMethodLoader
	{
		public IFactoryMethod LoadFactory()
		{
			string factoryName = ConfigurationManager.AppSettings["key"];
			return Assembly.GetExecutingAssembly().CreateInstance(factoryName) as IFactoryMethod;
		}
	}
}
