using Lvc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lvc.Tests.Utils
{
	public class RandomStringGeneratorTests
	{
		#region Execute

		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(-100)]
		public void Execute_GivenNegativeValues_Throws_ArgumentOutOfRangeException(int length)
		{
			// Arrange
			var sut = new RandomStringGenerator();

			// Act
			Action act = () => sut.Execute(length);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(1000)]
		[InlineData(1000000)]
		public void Execute(int length)
		{
			// Arrange
			var sut = new RandomStringGenerator();

			// Act
			var result = sut.Execute(length);

			// Assert
			Assert.Equal(length, result.Length);
		}

		#endregion Execute
	}
}
