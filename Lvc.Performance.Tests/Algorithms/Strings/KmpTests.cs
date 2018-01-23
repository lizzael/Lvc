using Lvc.Performance.Algorithms.Strings;
using Lvc.Utils;
using System;
using Xunit;

namespace Lvc.Performance.Tests.Algorithms.Strings
{
	public class KmpTests
	{
		#region ctor Kmp(substring)

		[Fact]
		public void Kmp_GivenNull_Throws_ArgumentNullException()
		{
			// Arrange
			string substring = null;

			// Act
			Action act = () => new Kmp(substring);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		#endregion ctor Kmp(substring)

		#region Execute

		[Fact]
		public void Execute_GivenNull_Throws_ArgumentNullException()
		{
			// Arrange
			var substring = "";
			string mainString = null;

			var sut = new Kmp(substring);

			// Act
			Action act = () => sut.Execute(mainString);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Theory]
		[InlineData("abad", "aca")]
		[InlineData("abxabcabcaby", "abcaby")]
		[InlineData("abxabcabcabyy", "abcaby")]
		[InlineData("abxabcabcabyasd", "abcaby")]
		[InlineData("ababa", "aba")]
		public void Execute(string mainString, string substring)
		{
			// Arrange
			var expectedResult = mainString.IndexOf(substring);

			var sut = new Kmp(substring);

			// Act
			var result = sut.Execute(mainString);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public void Execute_StressTest()
		{
			// Arrange
			var randomStringGenerator = new RandomStringGenerator();

			var substring = randomStringGenerator.Execute(1000);
			var sut = new Kmp(substring);

			var max = 10;
			for (int i = 0; i < max; i++)
			{
				var mainString = randomStringGenerator.Execute(999000);
				mainString = $"{mainString.Substring(0, 500000)}{substring}{mainString.Substring(500000)}";

				var expectedResult = mainString.IndexOf(substring);
				
				// Act
				var result = sut.Execute(mainString);

				// Assert
				Assert.Equal(expectedResult, result);
			}
		}

		#endregion Execute
	}
}
