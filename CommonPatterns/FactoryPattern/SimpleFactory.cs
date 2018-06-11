using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonPatterns.Core.FactoryPattern;

namespace CommonPatterns.FactoryPattern
{
	public class SimpleFactory : ISimpleFactory
	{
		protected Dictionary<string, Type> _types;

		public SimpleFactory()
		=>
			_types = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(w => w.GetInterface(typeof(IFactoryTarget).ToString()) != null)
				.ToDictionary(t => t.Name.ToLower());

		public IFactoryTarget Create(string name)
		{
			var t = _types[name];
			return t != null
				? Activator.CreateInstance(t) as IFactoryTarget
				: null;
		}
	}
}
