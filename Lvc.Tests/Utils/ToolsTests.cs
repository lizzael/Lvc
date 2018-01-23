using Lvc.Utils;
using Xunit;

namespace Lvc.Tests.Utils
{
	public class ToolsTests
	{
		[Theory]
		[InlineData(0, 1)]
		[InlineData(5, 23)]
		[InlineData(int.MinValue, int.MaxValue)]
		public void Swap_Int(int x, int y) =>
			Swap(ref x, ref y);

		[Theory]
		[InlineData("", "asd")]
		[InlineData(null, "3421d")]
		[InlineData("34rfcds", null)]
		[InlineData("sdfkluf;", "dafjsdlkjfa;s")]
		public void Swap_String(string x, string y) =>
			Swap(ref x, ref y);

		[Fact]
		public void Swap_Object() 
		{
			object x = null;
			object y = null;

			Swap(ref x, ref y);
		}

		private static void Swap<T>(ref T x, ref T y)
		{
			// Arrange
			var expectedX = y;
			var expectedY = x;

			// Act
			Tools.Swap(ref x, ref y);

			// Assert
			Assert.Equal(expectedX, x);
			Assert.Equal(expectedY, y);
		}
	}
}
