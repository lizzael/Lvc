using System;
using System.Linq;
using Xunit;

namespace Lvc.Tests
{
	public class ValidateTests
    {
		#region NotNull

		[Theory]
		[InlineData("")]
		[InlineData("sdaadsf")]
		public void NotNull(object obj)
		{
			// Arrange

			// Act
			Validate.NotNull(obj, nameof(obj));

			// Assert
		}

		[Fact]
		public void NotNull_Throw_ArgumentNullException()
		{
			// Arrange
			object obj = null;

			// Act
			Action act = () => Validate.NotNull(obj, nameof(obj));

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		#endregion NotNull

		#region AllNotNull

		[Theory]
		[InlineData("a")]
		[InlineData("a b")]
		[InlineData("asdf bfdgsfg")]
		public void AllNotNull(string s)
		{
			// Arrange
			var items = s.Split(' ');

			// Act
			Validate.AllNotNull(items, nameof(items));

			// Assert
		}

		[Theory]
		[InlineData(null)]
		[InlineData("null")]
		[InlineData("null sad fdsfsdf")]
		[InlineData("das null sadas")]
		[InlineData("sadas das null")]
		public void AllNotNull_Throw_ArgumentNullException(string str)
		{
			//Arrange
			var items = str?.Split(' ')
				.Select(s => s == "null" ? null : s);

			// Act
			Action act = () => Validate.AllNotNull(items, nameof(items));

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		#endregion AllNotNull

		#region NotValidValue

		[Theory]
		[InlineData(1, 0)]
		[InlineData(-1, 0)]
		[InlineData(int.MinValue, 0)]
		[InlineData(int.MaxValue, 0)]
		[InlineData(1, 10)]
		[InlineData(-1, 10)]
		[InlineData(int.MinValue, -10)]
		[InlineData(int.MaxValue, -10)]
		public void NotValidValue_Int(int value, int notValidValue)
		{
			// Arrange

			// Act
			Validate.NotValidValue(value, notValidValue, nameof(value));

			// Assert

		}

		[Theory]
		[InlineData("", null)]
		[InlineData(null, "")]
		[InlineData("asdasd", null)]
		[InlineData("asdsadd", "")]
		public void NotValidValue_String(string value, string notValidValue)
		{
			// Arrange

			// Act
			Validate.NotValidValue(value, notValidValue, nameof(value));

			// Assert
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(-1)]
		[InlineData(int.MinValue)]
		[InlineData(int.MaxValue)]
		public void NotValidValue_Int_Throw_ArgumentException(int value)
		{
			// Arrange

			// Act
			Action act = () => Validate.NotValidValue(value, value, nameof(value));

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("a")]
		[InlineData("adfasdf")]
		public void NotValidValue_String_Throw_ArgumentException(string value)
		{
			// Arrange

			// Act
			Action act = () => Validate.NotValidValue(value, value, nameof(value));

			// Assert
			Assert.Throws<ArgumentException>(act);
		}

		#endregion NotValidValue

		#region NotValidState

		[Theory]
		[InlineData(false)]
		public void NotValidState(bool notValidState)
		{
			// Arrange

			// Act
			Validate.NotValidState(notValidState, nameof(notValidState));

			// Assert
		}

		[Theory]
		[InlineData(true)]
		public void NotValidState_Throw_InvalidOperationException(bool notValidState)
		{
			// Arrange
		
			// Act
			Action act = () => Validate.NotValidState(notValidState, nameof(notValidState));

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		#endregion NotValidState

		#region LowerThan

		[Theory]
		[InlineData(0, 1)]
		[InlineData(-1, 0)]
		[InlineData(0, 10)]
		[InlineData(-10, 0)]
		public void LowerThan_Int(int value, int minValue)
		{
			// Arrange
			
			// Act
			Validate.LowerThan(value, minValue, nameof(value));

			// Assert
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 1)]
		[InlineData(200, 100)]
		public void LowerThan_Int_Throw_ArgumentOutOfRangeException(int value, int minValue)
		{
			// Arrange	

			// Act
			Action act = () => Validate.LowerThan(value, minValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData(0L, 1L)]
		[InlineData(-1L, 0L)]
		[InlineData(0L, 10L)]
		[InlineData(-10L, 0L)]
		public void LowerThan_Long(long value, long minValue)
		{
			// Arrange

			// Act
			Validate.LowerThan(value, minValue, nameof(value));

			// Assert
		}

		[Theory]
		[InlineData(0L, 0L)]
		[InlineData(1L, 1L)]
		[InlineData(200L, 100L)]
		public void LowerThan_Long_Throw_ArgumentOutOfRangeException(long value, long minValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.LowerThan(value, minValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("a", "b")]
		[InlineData("b", "c")]
		[InlineData("cda", "cdd")]
		[InlineData("cd", "cde")]
		public void LowerThan_String(string value, string minValue)
		{
			// Arrange

			// Act
			Validate.LowerThan(value, minValue, nameof(value));

			// Assert
		}

		[Theory]
		[InlineData("b", "b")]
		[InlineData("b", "a")]
		[InlineData("c", "b")]
		[InlineData("c", "c")]
		[InlineData("cda", "cd")]
		[InlineData("cdfg", "cd")]
		public void LowerThan_string_Throw_ArgumentOutOfRangeException(string value, string minValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.LowerThan(value, minValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion LowerThan

		#region AllLowerThan

		[Theory]
		[InlineData("1", 2)]
		[InlineData("1 2 1 3 5 3 2", 6)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 9)]
		public void AllLowerThan_Int(string str, int minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(int.Parse);

			// Act
			Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("2", 1)]
		[InlineData("1 2 1 3 5 3 2", 1)]
		[InlineData("1 2 1 3 5 3 2", 2)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 7)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 8)]
		public void AllLowerThan_Int_Throw_ArgumentOutOfRangeException(string str, int minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(int.Parse);

			// Act
			Action act = () => Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("1", 2L)]
		[InlineData("1 2 1 3 5 3 2", 6L)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 9L)]
		public void AllLowerThan_Long(string str, long minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(long.Parse);

			// Act
			Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("1", 1L)]
		[InlineData("2", 1L)]
		[InlineData("1 2 1 3 5 3 2", 1L)]
		[InlineData("1 2 1 3 5 3 2", 2L)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 7L)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", 8L)]
		public void AllLowerThan_Long_Throw_ArgumentOutOfRangeException(string str, long minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(long.Parse);

			// Act
			Action act = () => Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("a", "b")]
		[InlineData("cb", "cc")]
		[InlineData("bb ab cc", "dd")]
		[InlineData("bb cc dd", "def")]
		public void AllLowerThan_String(string str, string minValue)
		{
			// Arrange
			var items = str.Split(' ');

			// Act
			Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("b", "b")]
		[InlineData("b", "a")]
		[InlineData("bb ab cc", "cc")]
		[InlineData("bb aa cc", "ab")]
		public void AllLowerThan_string_Throw_ArgumentOutOfRangeException(string str, string minValue)
		{
			// Arrange
			var items = str.Split(' ');

			// Act
			Action act = () => Validate.AllLowerThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion AllLowerThan

		#region GreaterThan

		[Theory]
		[InlineData(1, 0)]
		[InlineData(0, -1)]
		[InlineData(10, 0)]
		[InlineData(0, -10)]
		public void GreaterThan_Int(int value, int minValue)
		{
			// Arrange

			// Act
			Validate.GreaterThan(value, minValue, nameof(value));

			// Assert
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 1)]
		[InlineData(100, 200)]
		public void GreaterThan_Int_Throw_ArgumentOutOfRangeException(int value, int minValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.GreaterThan(value, minValue,nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData(1L, 0L)]
		[InlineData(0L, -1L)]
		[InlineData(10L, 0L)]
		[InlineData(0L, -10L)]
		public void GreaterThan_Long(long value, long minValue)
		{
			// Arrange

			// Act
			Validate.GreaterThan(value, minValue, nameof(value));

			// Assert

		}

		[Theory]
		[InlineData(0L, 0L)]
		[InlineData(1L, 1L)]
		[InlineData(100L, 200L)]
		public void GreaterThan_Long_Throw_ArgumentOutOfRangeException(long value, long minValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.GreaterThan(value, minValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("b", "a")]
		[InlineData("c", "b")]
		[InlineData("cdd", "cda")]
		[InlineData("cde", "cd")]
		public void GreaterThan_String(string value, string minValue)
		{
			// Arrange

			// Act
			Validate.GreaterThan(value, minValue, nameof(value));

			// Assert

		}

		[Theory]
		[InlineData("b", "b")]
		[InlineData("b", "c")]
		[InlineData("c", "d")]
		[InlineData("c", "c")]
		[InlineData("c", "d")]
		[InlineData("cd", "cda")]
		[InlineData("cde", "cdfg")]
		public void GreaterThan_string_Throw_ArgumentOutOfRangeException(string value, string minValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.GreaterThan(value, minValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion GreaterThan

		#region AllGreaterThan

		[Theory]
		[InlineData("1", 0)]
		[InlineData("1 2 1 3 5 3 2", 0)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", -10)]
		public void AllGreaterThan_Int(string str, int minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(int.Parse);

			// Act
			Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("1", 1)]
		[InlineData("1", 2)]
		[InlineData("1 2 1 3 5 3 2", 1)]
		[InlineData("1 2 1 3 5 3 2", 2)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", -2)]
		public void AllGreaterThan_Int_Throw_ArgumentOutOfRangeException(string str, int minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(int.Parse);

			// Act
			Action act = () => Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("1", 0L)]
		[InlineData("1 2 1 3 5 3 2", 0L)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", -10L)]
		public void AllGreaterThan_Long(string str, long minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(long.Parse);

			// Act
			Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("1", 1L)]
		[InlineData("1", 2L)]
		[InlineData("1 2 1 3 5 3 2", 1L)]
		[InlineData("1 2 1 3 5 3 2", 2L)]
		[InlineData("0 -1 2 -4 4 -5 7 8 -9", -2L)]
		public void AllGreaterThan_Long_Throw_ArgumentOutOfRangeException(string str, long minValue)
		{
			// Arrange
			var items = str.Split(' ')
				.Select(long.Parse);

			// Act
			Action act = () => Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Theory]
		[InlineData("b", "a")]
		[InlineData("cc", "cb")]
		[InlineData("bb ab cc", "aa")]
		[InlineData("bb cc dd", "aaa")]
		public void AllGreaterThan_String(string str, string minValue)
		{
			// Arrange
			var items = str.Split(' ');

			// Act
			Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert
		}

		[Theory]
		[InlineData("b", "b")]
		[InlineData("b", "c")]
		[InlineData("bb ab cc", "ab")]
		[InlineData("bb aa cc", "ab")]
		public void AllGreaterThan_string_Throw_ArgumentOutOfRangeException(string str, string minValue)
		{
			// Arrange
			var items = str.Split(' ');
			
			// Act
			Action act = () => Validate.AllGreaterThan(items, minValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion GreaterThan

		#region CheckRange

		[Theory]
		[InlineData(0, 0, 1)]
		[InlineData(-2, -2, 2)]
		[InlineData(-1, -2, 2)]
		[InlineData(0, -2, 2)]
		[InlineData(1, -2, 2)]
		public void CheckRange(int value, int minValue, int maxValue)
		{
			// Arrange

			// Act
			Validate.CheckRange(value, minValue, maxValue, nameof(value));

			// Assert

		}

		[Theory]
		[InlineData(1, 0, 1)]
		[InlineData(-3, -2, 2)]
		[InlineData(2, -2, 2)]
		[InlineData(-4, -2, 2)]
		[InlineData(4, -2, 2)]
		public void CheckRange_Throw_ArgumentOutOfRangeException(int value, int minValue, int maxValue)
		{
			// Arrange

			// Act
			Action act = () => Validate.CheckRange(value, minValue, maxValue, nameof(value));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion CheckRange

		#region CheckRangeAll

		[Theory]
		[InlineData("0 0 0", 0, 1)]
		[InlineData("-2 -1 0 1", -2, 2)]
		public void CheckRangeAll(string values, int minValue, int maxValue)
		{
			// Arrange
			var items = values.Split(' ')
				.Select(int.Parse);

			// Act
			Validate.CheckRangeAll(items, minValue, maxValue, nameof(items));

			// Assert

		}

		[Theory]
		[InlineData("0 1 0", 0, 1)]
		[InlineData("-2 -3 0 1", -2, 2)]
		[InlineData("-2 -1 0 1 2", -2, 2)]
		[InlineData("-3 -2 -1 0 1", -2, 2)]
		public void CheckRangeAll_Throw_ArgumentOutOfRangeException(string values, int minValue, int maxValue)
		{
			// Arrange
			var items = values.Split(' ')
				.Select(int.Parse);

			// Act
			Action act = () => Validate.CheckRangeAll(items, minValue, maxValue, nameof(items));

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion CheckRangeAll
	}
}
