using FakeItEasy;
using Lvc.IO;
using System;
using System.IO;
using Xunit;

namespace Lvc.Tests.IO
{
	public class ScannerTests
	{
		#region ctor Scanner()

		[Fact]
		public void Scanner_Default() 
		{
			// Act
			var sut = new Scanner();

			// Assert
			Assert.Same(sut.In, Console.In);
		}

		#endregion ctor Scanner()

		#region ctor Scanner(TextReader)

		[Fact]
		public void Scanner_TextReader()
		{
			// Arrange
			var textReader = A.Fake<TextReader>();

			// Act
			var sut = new Scanner(textReader);

			// Assert
			Assert.Same(sut.In, textReader);
		}

		[Fact]
		public void Scanner_TextReader_Null_Throw_ArgumentNullException()
		{
			// Act
			Action act = () => new Scanner(null);

			// Validate
			Assert.Throws<ArgumentNullException>(act);
		}

		#endregion ctor Scanner(TextReader)

		#region ReadLine

		[Theory]
		[InlineData(0, 0)]
		[InlineData(-1, -1)]
		[InlineData(1, 1)]
		[InlineData(int.MinValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		public void ReadLine_Int(int intValue, int expectedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(intValue.ToString());

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLine<int>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(result, expectedResult);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(-1, -1)]
		[InlineData(1, 1)]
		[InlineData(int.MinValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		[InlineData(long.MinValue, long.MinValue)]
		[InlineData(long.MaxValue, long.MaxValue)]
		public void ReadLine_Long(long longValue, long expectedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(longValue.ToString());

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLine<long>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(result, expectedResult);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("a", "a")]
		[InlineData("123", "123")]
		[InlineData("asdfasd", "asdfasd")]
		public void ReadLine_String(string str, string expectedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(str);

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLine<string>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(result, expectedResult);
		}

		[Theory]
		[InlineData("01-15-1980", "1/15/1980 00:00:00")]
		[InlineData("10-15-1980", "10/15/1980 00:00:00")]
		[InlineData("01/15/1980", "1/15/1980 00:00:00")]
		[InlineData("02-13-2016 10:30:30", "2/13/2016 10:30:30")]
		[InlineData("02-13-2016 10:30:30 pm", "2/13/2016 22:30:30")]
		[InlineData("02-13-2016 20:30:30", "2/13/2016 20:30:30")]
		public void ReadLine_DateTime(string str, string expectedFormattedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(str);

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLine<DateTime>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(result.ToString(), expectedFormattedResult);
		}

		#endregion ReadLine

		#region ReadLineData

		[Theory]
		[InlineData("0", "0")]
		[InlineData("-1 5", "-1 5")]
		[InlineData("1 3 -4", "1 3 -4")]
		public void ReadLineData_Int(string str, string expectedFormattedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(str);

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLineData<int>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("0", "0")]
		[InlineData("-1 5", "-1 5")]
		[InlineData("1 3 -4", "1 3 -4")]
		public void ReadLineData_Long(string str, string expectedFormattedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(str);

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLineData<long>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("a", "a")]
		[InlineData("1 2 3", "1 2 3")]
		[InlineData("as dfa sd", "as dfa sd")]
		public void ReadLineData_String(string str, string expectedFormattedResult)
		{
			// Arrange
			var textReader = A.Fake<TextReader>();
			A.CallTo(() => textReader.ReadLine())
				.Returns(str);

			var sut = new Scanner(textReader);

			// Act
			var result = sut.ReadLineData<string>();

			// Assert
			A.CallTo(() => textReader.ReadLine())
				.MustHaveHappened(Repeated.Exactly.Once);

			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		#endregion ReadLineData
	}
}
