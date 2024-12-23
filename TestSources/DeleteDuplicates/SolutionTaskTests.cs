using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Task.DeleteDuplicates
{
	public class SolutionTaskTests
	{
		private SolutionTask solution = new SolutionTask();

		private void RunTest(int[] nums, int expectedDuplicates, int[] expectedArray)
		{
			var (actualDuplicates, actualArray) = solution.DeleteDuplicates(nums);

			Assert.That(actualDuplicates, Is.EqualTo(expectedDuplicates), "The number of duplicates does not match.");
			Assert.That(actualArray, Is.EqualTo(expectedArray), "The resulting array after removing duplicates does not match.");
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
	}
}
