using Lvc.Extensions;
using Lvc.Performance.ADTs.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lvc.Performance.Tests.ADTs.Heaps
{
	public class HeapTests
	{
		#region ctor Heap(capacity, comparison)

		[Theory]
		[InlineData(0)]
		[InlineData(10)]
		[InlineData(100)]
		public void Heap_Int(int capacity)
		{
			// Arrange

			// Act
			var sut = new Heap<int>(capacity);

			// Assert
		}

		[Fact]
		public void Heap_Int_NegativeCapacity_Throws_ArgumentOutOfRangeException()
		{
			// Arrange
			var capacity = -1;

			// Act
			Action act = () => new Heap<int>(capacity);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		#endregion ctor Heap(capacity, comparison)

		#region Count

		[Theory]
		[InlineData(10, 0, 0)]
		[InlineData(10, 1, 1)]
		[InlineData(10, 2, 2)]
		[InlineData(10, 10, 10)]
		public void Count(int capacity, int count, int expectedCount)
		{
			// Arrange
			var sut = new Heap<int>(capacity);
			sut.Insert(Enumerable.Range(0, count));

			// Act
			var result = sut.Count;

			// Assert
			Assert.Equal(expectedCount, result);
		}

		#endregion Count

		#region Capacity

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(10)]
		public void Capacity(int capacity)
		{
			// Arrange
			var sut = new Heap<int>(capacity);

			// Act
			var result = sut.Capacity;

			// Assert
			Assert.Equal(capacity, result);
		}

		#endregion Capacity

		#region Comparison

		[Fact]
		public void Comparison_PassingNoComparison_ReturnsDefaultComparison()
		{
			// Arrange
			Comparer<int> defaultIntComparer = Comparer<int>.Default;
			
			int capacity = 0;

			var sut = new Heap<int>(capacity);

			// Act
			var result = sut.Comparer;

			// Assert
			Assert.Equal(defaultIntComparer, result);
		}

		[Fact]
		public void Comparison_GivenNullComparison_ReturnsDefaultComparison()
		{
			// Arrange
			Comparer<int> defaultIntComparer = Comparer<int>.Default;

			int capacity = 0;
			Comparer<int> comparer = null;

			var sut = new Heap<int>(capacity, comparer);

			// Act
			var result = sut.Comparer;

			// Assert
			Assert.Equal(defaultIntComparer, result);
		}

		[Fact]
		public void Comparison_GivenComparison_ReturnsSameComparison()
		{
			// Arrange
			int capacity = 0;
			Comparer<int> comparer = Comparer<int>.Create((x, y) => y - x);

			var sut = new Heap<int>(capacity, comparer);

			// Act
			var result = sut.Comparer;

			// Assert
			Assert.Same(comparer, result);
		}

		#endregion Comparison

		#region IsEmpty

		[Theory]
		[InlineData(0, 0, 0, true)]
		[InlineData(1, 1, 1, true)]
		[InlineData(10, 10, 10, true)]
		[InlineData(10, 1, 0, false)]
		[InlineData(10, 5, 1, false)]
		[InlineData(10, 10, 9, false)]
		public void IsEmpty(int capacity, int inserts, int deletes, bool expectedResult)
		{
			// Arrange
			var sut = new Heap<int>(capacity);
			sut.Insert(Enumerable.Range(0, inserts));
			sut.Delete(deletes);

			// Act
			var result = sut.IsEmpty;

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion IsEmpty

		#region IsFull

		[Theory]
		[InlineData(0, 0, 0, true)]
		[InlineData(1, 0, 0, false)]
		[InlineData(1, 1, 1, false)]
		[InlineData(10, 10, 0, true)]
		[InlineData(10, 10, 10, false)]
		[InlineData(10, 10, 9, false)]
		[InlineData(10, 1, 0, false)]
		[InlineData(10, 9, 0, false)]
		public void IsFull(int capacity, int inserts, int deletes, bool expectedResult)
		{
			// Arrange
			var sut = new Heap<int>(capacity);
			sut.Insert(Enumerable.Range(0, inserts));
			sut.Delete(deletes);

			// Act
			var result = sut.IsFull;

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion IsFull

		#region Peek

		[Theory]
		[InlineData(1, 1, 0, 0)]
		[InlineData(10, 10, 0, 0)]
		[InlineData(10, 10, 1, 1)]
		[InlineData(10, 10, 5, 5)]
		[InlineData(10, 10, 9, 9)]
		public void Peek(int capacity, int inserts, int deletes, int expectedResult)
		{
			// Arrange
			var sut = new Heap<int>(capacity);
			sut.Insert(Enumerable.Range(0, inserts));
			sut.Delete(deletes);

			// Act
			var result = sut.Peek();

			// Assert
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public void Peek_EmptyHeap_Throws_InvalidOperationException()
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);

			// Act
			Action act = () => sut.Peek();

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		#endregion Peek

		#region Insert

		[Theory]
		[InlineData("5")]
		[InlineData("0 1")]
		[InlineData("0 1 10 -3 -5 100 -20")]
		public void Insert_OneByOne(string str)
		{
			// Arrange
			var capacity = 10;
			var items = str.ConvertToMany<int>(' ');

			var sut = new Heap<int>(capacity);

			// Act
			foreach(var x in items)
				sut.Insert(x);

			// Assert
			Assert.Equal(items.Min(), sut.Peek());
		}

		[Theory]
		[InlineData("5")]
		[InlineData("0 1")]
		[InlineData("0 1 10 -3 -5 100 -20")]
		public void Insert_Enumerable(string str)
		{
			// Arrange
			var capacity = 10;
			var items = str.ConvertToMany<int>(' ');

			var sut = new Heap<int>(capacity);

			// Act
			sut.Insert(items);

			// Assert
			Assert.Equal(items.Min(), sut.Peek());
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		public void Insert_OneByOne_WhenFull_Throws_InvalidOperationException(int capacity)
		{
			// Arrange
			var sut = new Heap<int>(capacity);

			var items = Enumerable.Range(0, capacity);
			foreach (var x in items)
				sut.Insert(x);

			// Act
			Action act = () => sut.Insert(capacity);

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		public void Insert_Enumerable_WhenIsGoingToBeFull_Throws_InvalidOperationException(string str)
		{
			// Arrange
			var capacity = 10;
			var items = Enumerable.Range(0, capacity + 1);

			var sut = new Heap<int>(capacity);

			// Act
			Action act = () => sut.Insert(items);

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		#endregion Insert

		#region Delete

		[Theory]
		[InlineData("0", 1)]
		[InlineData("1", 1)]
		[InlineData("-1", 1)]
		[InlineData("5", 1)]
		[InlineData("0 1 -1 5 -20 10", 3)]
		public void Delete_OneByOne(string str, int deletes)
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);

			var items = str.ConvertToMany<int>(' ');
			sut.Insert(items);

			// Act
			var result = Enumerable.Range(0, deletes)
				.Select(s => sut.Delete());

			// Assert
			var expectedResult = items
				.OrderBy(x => x)
				.Take(deletes);
			Assert.True(expectedResult.SequenceEqual(result));
		}

		[Fact]
		public void Delete_OneByOne_WhenEmpty_ThrowsInvalidOperationException()
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);

			// Act
			Action act = () => sut.Delete();

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		[Theory]
		[InlineData("0", 1)]
		[InlineData("1", 1)]
		[InlineData("-1", 1)]
		[InlineData("5", 1)]
		[InlineData("0 1 -1 5 -20 10", 3)]
		public void Delete_N(string str, int deletes)
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);

			var items = str.ConvertToMany<int>(' ');
			sut.Insert(items);

			// Act
			var result = sut.Delete(deletes);

			// Assert
			var expectedResult = items
				.OrderBy(x => x)
				.Take(deletes);
			Assert.True(expectedResult.SequenceEqual(result));
		}

		[Fact]
		public void Delete_N_WhenIsGoingTobeEmpty_ThrowsInvalidOperationException()
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);
			sut.Insert(Enumerable.Range(0, capacity));
			// Act
			Action act = () => sut.Delete(capacity + 1);

			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}

		#endregion Delete

		#region DeleteItem

		[Theory]
		[InlineData("0", 0, true)]
		[InlineData("0", 1, false)]
		[InlineData("1", 0, false)]
		[InlineData("0 1 -1 5 -20 10", 1, true)]
		[InlineData("0 1 -1 5 -20 10", -20, true)]
		[InlineData("0 1 -1 5 -20 10", -5, false)]
		[InlineData("0 1 -1 5 -20 10", 20, false)]
		public void DeleteItem_T(string str, int deleteItem, bool expectedResult)
		{
			// Arrange
			var capacity = 10;

			var sut = new Heap<int>(capacity);

			var items = str.ConvertToMany<int>(' ');
			sut.Insert(items);

			// Act
			var result = sut.DeleteItem(deleteItem);

			// Assert
			Assert.Equal(expectedResult, result);
		}

		#endregion DeleteItem
	}
}
