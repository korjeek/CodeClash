using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CodeClash.UserSolutionTest
{
	public class SolutionTaskTests
	{
		private SolutionTask solution = new SolutionTask();

		private void RunTest(int[] nums, int expectedDuplicates, int[] expectedArray)
		{
			var (actualDuplicates, actualArray) = solution.DeleteDuplicates(nums);

			Assert.That(actualDuplicates, Is.EqualTo(expectedDuplicates), "Количество дупликатов неверное.");
			Assert.That(actualArray, Is.EqualTo(expectedArray), "Массив уникальных элементов неверный.");
		}

		[Test]
		public void DeleteDuplicates_Example1()
		{
			RunTest(new[] { 1, 2, 3, 4, 5 }, 0, new[] { 1, 2, 3, 4, 5 });
		}

		[Test]
		public void DeleteDuplicates_Example2()
		{
			RunTest(new[] { 1, 1, 1, 1 }, 3, new[] { 1 });
		}

		[Test]
		public void DeleteDuplicates_SomeDuplicates()
		{
			RunTest(new[] { 1, 2, 2, 3, 4, 4, 4, 5 }, 3, new[] { 1, 2, 3, 4, 5 });
		}

		[Test]
		public void DeleteDuplicates_SingleElement()
		{
			RunTest(new[] { 42 }, 0, new[] { 42 });
		}

		[Test]
		public void DeleteDuplicates_MixedOrder()
		{
			RunTest(new[] { 3, 1, 2, 3, 2, 4 }, 2, new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void DeleteDuplicates_EmptyArray()
		{
			RunTest(new int[] { }, 0, new int[] { });
		}

		[Test]
		public void DeleteDuplicates_AllUnique()
		{
			RunTest(new[] { 10, 20, 30, 40, 50 }, 0, new[] { 10, 20, 30, 40, 50 });
		}

		[Test]
		public void DeleteDuplicates_AllSameElement()
		{
			RunTest(new[] { 7, 7, 7, 7, 7 }, 4, new[] { 7 });
		}

		[Test]
		public void DeleteDuplicates_NegativeNumbers()
		{
			RunTest(new[] { -1, -2, -2, -3, -3, -3 }, 3, new[] { -3, -2, -1 });
		}

		[Test]
		public void DeleteDuplicates_MixedNumbers()
		{
			RunTest(new[] { -1, 0, 1, 0, -1 }, 2, new[] { -1, 0, 1 });
		}

		[Test]
		public void DeleteDuplicates_LargeNumbers()
		{
			RunTest(new[] { int.MaxValue, int.MinValue, int.MaxValue }, 1, new[] { int.MinValue, int.MaxValue });
		}

		[Test]
		public void DeleteDuplicates_RepeatedZeros()
		{
			RunTest(new[] { 0, 0, 0, 0 }, 3, new[] { 0 });
		}

		[Test]
		public void DeleteDuplicates_MixedPositiveAndNegative()
		{
			RunTest(new[] { 5, -5, 5, -5, 5 }, 3, new[] { -5, 5 });
		}

		[Test]
		public void DeleteDuplicates_AlternatingNumbers()
		{
			RunTest(new[] { 1, 2, 1, 2, 1, 2 }, 4, new[] { 1, 2 });
		}

		[Test]
		public void DeleteDuplicates_LongArray()
		{
			RunTest(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }, 5, new[] { 1, 2, 3, 4, 5 });
		}

		[Test]
		public void DeleteDuplicates_PartialDuplicates()
		{
			RunTest(new[] { 1, 2, 3, 2, 4, 1 }, 2, new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void DeleteDuplicates_DuplicatesAtEnd()
		{
			RunTest(new[] { 1, 2, 3, 4, 4, 4 }, 2, new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void DeleteDuplicates_DuplicatesAtStart()
		{
			RunTest(new[] { 3, 3, 3, 1, 2, 3 }, 3, new[] { 1, 2, 3 });
		}

		[Test]
		public void DeleteDuplicates_DuplicatesSpreadOut()
		{
			RunTest(new[] { 1, 2, 1, 3, 2, 4, 3 }, 3, new[] { 1, 2, 3, 4 });
		}

		[Test]
		public void DeleteDuplicates_AllZeroArray()
		{
			RunTest(new[] { 0, 0, 0, 0, 0, 0 }, 5, new[] { 0 });
		}

		[Test]
		public void DeleteDuplicates_MixedZeroesAndOnes()
		{
			RunTest(new[] { 0, 1, 0, 1, 0, 1 }, 4, new[] { 0, 1 });
		}

		[Test]
		public void DeleteDuplicates_UnorderedDuplicates()
		{
			RunTest(new[] { 10, 20, 10, 30, 20, 40 }, 2, new[] { 10, 20, 30, 40 });
		}

		[Test]
		public void DeleteDuplicates_LargeData1()
		{
			var nums = GenerateLargeData(1000000, 1, 5);
			var expectedDuplicates = 999995;
			var expectedArray = new[] { 1, 2, 3, 4, 5 };
			RunTest(nums, expectedDuplicates, expectedArray);
		}

		[Test]
		public void DeleteDuplicates_LargeData2()
		{
			var nums = GenerateLargeData(1000000, 0, 1);
			var expectedDuplicates = 999998;
			var expectedArray = new[] { 0, 1 };
			RunTest(nums, expectedDuplicates, expectedArray);
		}

		private int[] GenerateLargeData(int size, int minValue, int maxValue)
		{
			var random = new Random();
			var nums = new int[size];
			for (var i = 0; i < size; i++)
				nums[i] = random.Next(minValue, maxValue + 1);
			return nums;
		}
	}
}
