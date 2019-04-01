using Lvc.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Lvc.Tests.Extensions
{
	public class StringExtensionsTests
    {
		#region ConvertTo

		[Theory]
		[InlineData("0", 0)]
		[InlineData("-10", -10)]
		[InlineData("10", 10)]
		[InlineData("-2147483648", -2147483648)] // int.MinValue
		[InlineData("2147483647", 2147483647)] // int.MaxValue
		public void ConvertTo_Int(
            string s, 
            int expectedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertTo<int>();

			// Assert
			Assert.Equal(result, expectedResult);
		}

		[Fact]
		public void ConvertTo_Throw_NullReferenceException()
		{
			// Arrange
			string sut = null;

			// Act
			Action act = () => sut.ConvertTo<int>();

			// Assert
			Assert.Throws<NullReferenceException>(act);
		}
		
		[Theory]
		[InlineData("")]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		public void ConvertTo_Int_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertTo<int>();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("0", 0)]
		[InlineData("-10", -10)]
		[InlineData("10", 10)]
		[InlineData("-2147483648", -2147483648)] // int.MinValue
		[InlineData("2147483647", 2147483647)] // int.MaxValue
		[InlineData("-9223372036854775808", -9223372036854775808L)] // long.MinValue
		[InlineData("9223372036854775807", 9223372036854775807L)] // long.MaxValue
		public void ConvertTo_Long(
            string s, 
            long expectedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertTo<long>();

			// Assert
			Assert.Equal(result, expectedResult);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		public void ConvertTo_Long_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertTo<long>();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("01-15-1980")]
		[InlineData("01-23-2017 10:40:30")]
		[InlineData("01/23/2017 10:40:30")]
		[InlineData("01/23/2017 10:40:30 am")]
		[InlineData("01/23/2017 10:40:30 pm")]
		[InlineData("01/22/2017 20:40:30")]
		public void ConvertTo_DateTime(string s)
		{
			// Arrange

			// Act
			var result = s.ConvertTo<DateTime>();

			// Assert
			Assert.Equal(result, DateTime.Parse(s));
		}

		[Theory]
		[InlineData("a")]
		[InlineData("02-30-1980")]
		[InlineData("02-29-1981")]
		[InlineData("13-29-1980")]
		[InlineData("3-32-1980")]
		[InlineData("3-4-1980 25:30:30")]
		[InlineData("3-4-1980 5:60:30")]
		[InlineData("3-4-1980 5:30:60")]
		public void ConvertTo_DateTime_Throw(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertTo<DateTime>();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		#endregion ConvertTo

		#region ConvertToMany

		[Fact]
		public void ConvertToMany_Throw_NullReferenceException()
		{
			// Arrange
			string sut = null;

			// Act
			Action act = () => sut.ConvertToMany<int>(' ');

			// Assert
			Assert.Throws<NullReferenceException>(act);
		}

		#region CharSeparators

		[Theory]
		[InlineData("0", ' ', "0")]
		[InlineData("-2147483648 -1 0 1 2147483647", ' ', "-2147483648 -1 0 1 2147483647")]
		public void ConvertToMany_CharSeparators_Int(
			string s, 
            char separator, 
            string expectedFormattedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertToMany<int>(separator);

			// Assert
			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		[InlineData(" 1 2 3")]
		[InlineData("1  2 3")]
		[InlineData("1 a 3")]
		public void ConvertToMany_CharSeparators_Int_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<int>(' ').ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("0", ' ', "0")]
		[InlineData("-1", ' ', "-1")]
		[InlineData("1", ' ', "1")]
		[InlineData("-9223372036854775808", ' ', "-9223372036854775808")] // long.MinValue
		[InlineData("9223372036854775807", ' ', "9223372036854775807")] // long.MaxValue
		[InlineData("-9223372036854775808 -1 0 1 9223372036854775807",
					' ',
					"-9223372036854775808 -1 0 1 9223372036854775807")]
		public void ConvertToMany_CharSeparators_Long(
			string s, 
            char separator, 
            string expectedFormattedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertToMany<long>(separator);

			// Assert
			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("")]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		[InlineData(" 1 2 3")]
		[InlineData("1  2 3")]
		[InlineData("1 a 3")]
		public void ConvertToMany_CharSeparators_Long_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<long>(' ').ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("01-15-1980", ',')]
		[InlineData("01-23-2017 10:40:30", ',')]
		[InlineData("01/23/2017 10:40:30", ',')]
		[InlineData("01/23/2017 10:40:30 am", ',')]
		[InlineData("01/23/2017 10:40:30 pm", ',')]
		[InlineData("01/22/2017 20:40:30", ',')]
		[InlineData("01-15-1980,01/23/2017 10:40:30 pm", ',')]
		public void ConvertToMany_CharSeparators_DateTime(
            string str, 
            char separator)
		{
			// Arrange

			// Act
			var result = str.ConvertToMany<DateTime>(separator);

			// Assert
			var expectedFormattedResult = str.Split(separator)
				.Select(DateTime.Parse);

			Assert.True(result.SequenceEqual(expectedFormattedResult));
		}

		[Theory]
		[InlineData("a")]
		[InlineData("02-30-1980")]
		[InlineData("02-29-1981")]
		[InlineData("13-29-1980")]
		[InlineData("3-32-1980")]
		[InlineData("3-4-1980 25:30:30")]
		[InlineData("3-4-1980 5:60:30")]
		[InlineData("3-4-1980 5:30:60")]
		[InlineData("02-30-1980,3-4-1980 25:30:30")]
		public void ConvertToMany_CharSeparators_DateTime_Throws_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<DateTime>(',').ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		#endregion CharSeparators

		#region StringSeparators

		[Theory]
		[InlineData("0", " ", "0")]
		[InlineData("0", ", ", "0")]
		[InlineData("-2147483648 -1 0 1 2147483647", " ", "-2147483648 -1 0 1 2147483647")]
		[InlineData("-2147483648, -1, 0, 1, 2147483647", ", ", "-2147483648 -1 0 1 2147483647")]
		public void ConvertToMany_StringSeparators_Int(
			string s, 
            string separator, 
            string expectedFormattedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertToMany<int>(separator);

			// Assert
			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		[InlineData(" 1 2 3")]
		[InlineData("1  2 3")]
		[InlineData("1 a 3")]
		public void ConvertToMany_StringSeparators_Int_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<int>(" ").ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("0", " ", "0")]
		[InlineData("0", ", ", "0")]
		[InlineData("-1", " ", "-1")]
		[InlineData("-1", ", ", "-1")]
		[InlineData("1", " ", "1")]
		[InlineData("1", ", ", "1")]
		[InlineData("-9223372036854775808", " ", "-9223372036854775808")] // long.MinValue
		[InlineData("-9223372036854775808", ", ", "-9223372036854775808")] // long.MinValue
		[InlineData("9223372036854775807", " ", "9223372036854775807")] // long.MaxValue
		[InlineData("9223372036854775807", ", ", "9223372036854775807")] // long.MaxValue
		[InlineData("-9223372036854775808 -1 0 1 9223372036854775807",
					" ",
					"-9223372036854775808 -1 0 1 9223372036854775807")]
		[InlineData("-9223372036854775808, -1, 0, 1, 9223372036854775807",
					", ",
					"-9223372036854775808 -1 0 1 9223372036854775807")]
		public void ConvertToMany_StringSeparators_Long(
			string s, 
            string separator, 
            string expectedFormattedResult)
		{
			// Arrange

			// Act
			var result = s.ConvertToMany<long>(separator);

			// Assert
			Assert.Equal(string.Join(" ", result), expectedFormattedResult);
		}

		[Theory]
		[InlineData("")]
		[InlineData("a")]
		[InlineData("b10")]
		[InlineData("1c0")]
		[InlineData("10d")]
		[InlineData(" 1 2 3")]
		[InlineData("1  2 3")]
		[InlineData("1 a 3")]
		public void ConvertToMany_StringSeparators_Long_Throw_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<long>(" ").ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		[Theory]
		[InlineData("01-15-1980", ",")]
		[InlineData("01-15-1980", ", ")]
		[InlineData("01-23-2017 10:40:30", ",")]
		[InlineData("01-23-2017 10:40:30", ", ")]
		[InlineData("01/23/2017 10:40:30", ",")]
		[InlineData("01/23/2017 10:40:30", ", ")]
		[InlineData("01/23/2017 10:40:30 am", ",")]
		[InlineData("01/23/2017 10:40:30 am", ", ")]
		[InlineData("01/23/2017 10:40:30 pm", ",")]
		[InlineData("01/23/2017 10:40:30 pm", ", ")]
		[InlineData("01/22/2017 20:40:30", ",")]
		[InlineData("01/22/2017 20:40:30", ", ")]
		[InlineData("01-15-1980,01/23/2017 10:40:30 pm", ",")]
		[InlineData("01-15-1980, 01/23/2017 10:40:30 pm", ", ")]
		public void ConvertToMany_StringSeparators_DateTime(
            string str, 
            string separator)
		{
			// Arrange

			// Act
			var result = str.ConvertToMany<DateTime>(separator);

			// Assert
			var expectedFormattedResult = str.Split(',')
				.Select(DateTime.Parse);

			Assert.True(result.SequenceEqual(expectedFormattedResult));
		}

		[Theory]
		[InlineData("a")]
		[InlineData("02-30-1980")]
		[InlineData("02-29-1981")]
		[InlineData("13-29-1980")]
		[InlineData("3-32-1980")]
		[InlineData("3-4-1980 25:30:30")]
		[InlineData("3-4-1980 5:60:30")]
		[InlineData("3-4-1980 5:30:60")]
		[InlineData("02-30-1980,3-4-1980 25:30:30")]
		public void ConvertToMany_StringSeparators_DateTime_Throws_FormatException(string s)
		{
			// Arrange

			// Act
			Action act = () => s.ConvertToMany<DateTime>(",").ToArray();

			// Assert
			Assert.Throws<FormatException>(act);
		}

		#endregion StringSeparators

		#endregion ConvertToMany
	}
}
