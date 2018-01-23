using Lvc.Performance.ATDs.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Trees
{
	public class NTreeTests
	{
		#region ctor Ntree<>()

		[Fact]
		public void NTree_Int_Default() 
		{
			// Act
			var sut = new NTree<int>();

			// Assert
			Assert.Equal(sut.Value, 0);
			Assert.Equal(sut.ChildsList.Count, 0);
			Assert.False(sut.ChildsEnumerable.Any());
		}

		[Fact]
		public void NTree_Object_Default()
		{
			// Act
			var sut = new NTree<object>();

			// Assert
			Assert.Equal(sut.Value, null);
			Assert.Equal(sut.ChildsList.Count, 0);
			Assert.False(sut.ChildsEnumerable.Any());
		}

		#endregion ctor Ntree<>()

		#region ctor Ntree<T>(value)

		[Theory]
		[InlineData(0)]
		[InlineData(int.MinValue)]
		[InlineData(int.MaxValue)]
		public void NTree_Int(int value)
		{
			// Act
			var sut = new NTree<int>(value);

			// Assert
			Assert.Equal(sut.Value, value);
			Assert.Equal(sut.ChildsList.Count, 0);
			Assert.False(sut.ChildsEnumerable.Any());
		}

		[Theory]
		[InlineData(null)]
		[InlineData("sdfasd")]
		public void NTree_Object(object value)
		{
			// Act
			var sut = new NTree<object>(value);

			// Assert
			Assert.Equal(sut.Value, value);
			Assert.Equal(sut.ChildsList.Count, 0);
			Assert.False(sut.ChildsEnumerable.Any());
		}

		#endregion ctor Ntree<T>(value)

		#region ctor Ntree<T>(value, childs)

		[Theory]
		[InlineData(0, "1")]
		[InlineData(-2147483648, "-2147483648")] //int.MinValue
		[InlineData(2147483647, "2147483647")] //int.MaxValue
		[InlineData(0, "-2147483648 0 2147483647")]
		public void NTree_Int_WhithChilds(int value, string childsValues)
		{
			// Arrange
			var childs = childsValues.Split(' ')
				.Select(s => new NTree<int>(int.Parse(s)))
				.ToList();

			// Act
			var sut = new NTree<int>(value, childs);

			// Assert
			Assert.Equal(sut.Value, value);
			Assert.Same(sut.ChildsList, childs);
			Assert.True(sut.ChildsEnumerable.SequenceEqual(childs));
		}

		[Theory]
		[InlineData(null, "")]
		[InlineData("", "")]
		[InlineData("asd", "asd e  5rer sdf 2121")]
		public void NTree_Object_WhithChilds(object value, string childsValues)
		{
			// Arrange
			var childs = childsValues.Split(' ')
				.Select(s => new NTree<object>(s))
				.ToList();

			// Act
			var sut = new NTree<object>(value, childs);

			// Assert
			Assert.Equal(sut.Value, value);
			Assert.Same(sut.ChildsList, childs);
			Assert.True(sut.ChildsEnumerable.SequenceEqual(childs));
		}

		[Fact]
		public void NTree_Object_WhithChildsNull_Throws_ArgumentNullException()
		{
			// Arrange
			List<NTree<object>> childs = null;
			var value = new object();

			// Act
			Action act = () => new NTree<object>(value, childs);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		#endregion ctor Ntree<T>(value, childs)

		#region PreOrder

		[Fact]
		public void PreOrder_OneLevel()
		{
			// Arrange
			var value = 0;

			var sut = new NTree<int>(value);

			// Act
			var result = sut.PreOrder;

			// Assert
			Assert.True(result.SequenceEqual(new[] { value }));
		}

		[Fact]
		public void PreOrder_TwoLevels_ThreeChilds()
		{
			// Arrange
			var values = Enumerable.Range(0, 4).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
				new NTree<int>(values[3]),
			});

			// Act
			var result = sut.PreOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void PreOrder_ThreeLevels_ThreeChildsPerLevels()
		{
			// Arrange
			var values = Enumerable.Range(0, 13).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>>
				{
					new NTree<int>(values[2]),
					new NTree<int>(values[3]),
					new NTree<int>(values[4]),
				}),
				new NTree<int>(values[5], new List<NTree<int>>
				{
					new NTree<int>(values[6]),
					new NTree<int>(values[7]),
					new NTree<int>(values[8]),
				}),
				new NTree<int>(values[9], new List<NTree<int>>
				{
					new NTree<int>(values[10]),
					new NTree<int>(values[11]),
					new NTree<int>(values[12]),
				}),
			});

			// Act
			var result = sut.PreOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void PreOrder_OnEvenTree()
		{
			// Arrange
			var values = Enumerable.Range(0, 7).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>>
				{
					new NTree<int>(values[2]),
				}),
				new NTree<int>(values[3]),
				new NTree<int>(values[4], new List<NTree<int>>
				{
					new NTree<int>(values[5]),
					new NTree<int>(values[6]),
				}),
			});

			// Act
			var result = sut.PreOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		#endregion PreOrder

		#region PostOrder

		[Fact]
		public void PostOrder_OneLevel()
		{
			// Arrange
			var value = 0;

			var sut = new NTree<int>(value);

			// Act
			var result = sut.PostOrder;

			// Assert
			Assert.True(result.SequenceEqual(new[] { value }));
		}

		[Fact]
		public void PostOrder_TwoLevels_ThreeChilds()
		{
			// Arrange
			var values = Enumerable.Range(0, 4).ToArray();

			var sut = new NTree<int>(values[3], new List<NTree<int>>
			{
				new NTree<int>(values[0]),
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
			});

			// Act
			var result = sut.PostOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void PostOrder_ThreeLevels_ThreeChildsPerLevels()
		{
			// Arrange
			var values = Enumerable.Range(0, 13).ToArray();

			var sut = new NTree<int>(values[12], new List<NTree<int>>
			{
				new NTree<int>(values[3], new List<NTree<int>>
				{
					new NTree<int>(values[0]),
					new NTree<int>(values[1]),
					new NTree<int>(values[2]),
				}),
				new NTree<int>(values[7], new List<NTree<int>>
				{
					new NTree<int>(values[4]),
					new NTree<int>(values[5]),
					new NTree<int>(values[6]),
				}),
				new NTree<int>(values[11], new List<NTree<int>>
				{
					new NTree<int>(values[8]),
					new NTree<int>(values[9]),
					new NTree<int>(values[10]),
				}),
			});

			// Act
			var result = sut.PostOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void PostOrder_UnEvenTree()
		{
			// Arrange
			var values = Enumerable.Range(0, 7).ToArray();

			var sut = new NTree<int>(values[6], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>>
				{
					new NTree<int>(values[0]),
				}),
				new NTree<int>(values[2]),
				new NTree<int>(values[5], new List<NTree<int>>
				{
					new NTree<int>(values[3]),
					new NTree<int>(values[4]),
				}),
			});

			// Act
			var result = sut.PostOrder;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		#endregion PostOrder

		#region ByLevel

		[Fact]
		public void ByLevel_OneLevel()
		{
			// Arrange
			var value = 0;

			var sut = new NTree<int>(value);

			// Act
			var result = sut.ByLevel;

			// Assert
			Assert.True(result.SequenceEqual(new[] { value }));
		}

		[Fact]
		public void ByLevel_TwoLevels_ThreeChilds()
		{
			// Arrange
			var values = Enumerable.Range(0, 4).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
				new NTree<int>(values[3]),
			});

			// Act
			var result = sut.ByLevel;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void ByLevel_ThreeLevels_ThreeChildsPerLevels()
		{
			// Arrange
			var values = Enumerable.Range(0, 13).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>>
				{
					new NTree<int>(values[4]),
					new NTree<int>(values[5]),
					new NTree<int>(values[6]),
				}),
				new NTree<int>(values[2], new List<NTree<int>>
				{
					new NTree<int>(values[7]),
					new NTree<int>(values[8]),
					new NTree<int>(values[9]),
				}),
				new NTree<int>(values[3], new List<NTree<int>>
				{
					new NTree<int>(values[10]),
					new NTree<int>(values[11]),
					new NTree<int>(values[12]),
				}),
			});

			// Act
			var result = sut.ByLevel;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		[Fact]
		public void ByLevel_UnEvenTree()
		{
			// Arrange
			var values = Enumerable.Range(0, 7).ToArray();

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>>
				{
					new NTree<int>(values[4]),
				}),
				new NTree<int>(values[2]),
				new NTree<int>(values[3], new List<NTree<int>>
				{
					new NTree<int>(values[5]),
					new NTree<int>(values[6]),
				}),
			});

			// Act
			var result = sut.ByLevel;

			// Assert
			Assert.True(result.SequenceEqual(values));
		}

		#endregion ByLevel

		#region Equals

		[Fact]
		public void Equals_ObjectNull_Returns_False()
		{
			// Arrange
			object otherTree = null;
			var sut = new NTree<int>();

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Equals_NTreeNull_Returns_False()
		{
			// Arrange
			NTree<int> otherTree = null;

			var sut = new NTree<int>();

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Equals_Object1LevelSameValue_Returns_True()
		{
			// Arrange
			var value = 5;
			object otherTree = new NTree<int>(value);

			var sut = new NTree<int>(value);

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Equals_NTree1LevelSameValue_Returns_True()
		{
			// Arrange
			var value = 5;
			var otherTree = new NTree<int>(value);

			var sut = new NTree<int>(value);

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Equals_Object1LevelDifferentValue_Returns_False()
		{
			// Arrange
			var value1 = 5;
			var value2 = 50;
			object otherTree = new NTree<int>(value1);

			var sut = new NTree<int>(value2);

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Equals_NTree1LevelDifferentValue_Returns_False()
		{
			// Arrange
			var value1 = 5;
			var value2 = 50;
			var otherTree = new NTree<int>(value1);

			var sut = new NTree<int>(value2);

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Equals_NTree2LevelsSameValues_Returns_True()
		{
			// Arrange
			var values = new[] { 0, 1, 2 };
			var otherTree = new NTree<int>(values[0], new List<NTree<int>> 
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
			});

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
			});

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Equals_NTree2LevelsDifferentsValues_Returns_False()
		{
			// Arrange
			var values = new[] { 0, 1, 2 };
			var otherTree = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
			});

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2] + 1),
			});

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Equals_NTreeDifferentStructure_Returns_False()
		{
			// Arrange
			var values = new[] { 0, 1, 2, 3, 4 };
			var otherTree = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1]),
				new NTree<int>(values[2]),
				new NTree<int>(values[3]),
				new NTree<int>(values[4]),
			});

			var sut = new NTree<int>(values[0], new List<NTree<int>>
			{
				new NTree<int>(values[1], new List<NTree<int>> 
				{
					new NTree<int>(values[2]),
				}),
				new NTree<int>(values[3], new List<NTree<int>>
				{
					new NTree<int>(values[4]),
				}),
			});

			// Act
			var result = sut.Equals(otherTree);

			// Assert
			Assert.False(result);
		}

		#endregion Equals
	}
}
