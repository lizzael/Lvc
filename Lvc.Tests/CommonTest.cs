using Xunit;

namespace Lvc.Tests
{
	public static class CommonTest
	{
		public static void TestOverridedObjectMethods(bool expectedResult, object p1, object p2)
		{
			// Act and Assert
			Assert.True(p1 != null);
			Assert.True(p2 != null);
			Assert.True(!p1.Equals(null));
			Assert.True(!p2.Equals(null));

			Assert.False(p2 == null);
			Assert.False(p1.Equals(null));
			Assert.False(p2.Equals(null));

			Assert.Equal(expectedResult, p1.Equals(p2));
			Assert.Equal(expectedResult, p2.Equals(p1));

			Assert.Equal(expectedResult, p1.GetHashCode() == p2.GetHashCode());
			Assert.Equal(expectedResult, p1.ToString() == p2.ToString());
		}
	}
}
