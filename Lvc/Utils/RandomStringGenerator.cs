﻿using Lvc.Core.Utils;
using System;
using System.Linq;

namespace Lvc.Utils
{
	public class RandomStringGenerator : IRandomStringGenerator
	{
		protected Random _randomGenerator;

		public RandomStringGenerator(int? seed = null)
		{
			_randomGenerator = seed.HasValue
				? new Random(seed.Value)
				: new Random();
		}

		public string Execute(int length)
		{
			Validate.GreaterThan(length, -1, nameof(length));

			return new string(
				Enumerable.Range(0, length)
					.Select(s => (char)_randomGenerator.Next(byte.MaxValue + 1))
					.ToArray()
			);
		}
	}
}
