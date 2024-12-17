using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CodeClash.UserSolutionTest
{
	[TestFixture]
	public class SolutionTaskTests
	{
		private SolutionTask solution = new ();
		private void RunTest(int[] nums, int target, int[] expected)
		{
			var result = solution.FindSum(nums, target);
			CollectionAssert.AreEqual(expected, result);
		}
		
		[TestCase]
		public void FindSumTest_Example1()
		{
			var nums = new []{ 1, 1 };
			var target = 2;
			var expected = new []{ 0, 1 };
			RunTest(nums, target, expected);
		}
		
		[TestCase]
		public void FindSumTest_Example2()
		{
			var nums = new []{ 2, 7, 11, 3 };
			var target = 9;
			var expected = new []{ 0, 1 };
			RunTest(nums, target , expected);
		}
		
		[TestCase]
		public void FindSumTest_Example3()
		{
			var nums = new [] { 1, 2, 3, 4 };
			var target = 6;
			var expected = new [] { 1, 3 };
			RunTest(nums, target, expected);
		}
		
		[TestCase]
		public void OneElementIsZero()
		{
			var nums = new [] { 5, 0, 3, 4 };
			var target = 5;
			var expected = new [] { 0, 1 };
			RunTest(nums, target, expected);
		}
		
		[TestCase]
		public void OneElementIsNegative()
		{
			var nums = new [] { 11, 9, -5, 3, 20 };
			var target = 15;
			var expected = new [] { 2, 4 };
			RunTest(nums, target, expected);
		}
		
		[TestCase]
		public void AllElementsIsNegative()
		{
			var nums = new [] { -3, -1, -5, -9, -2 };
			var target = -10;
			var expected = new [] { 1, 3 };
			RunTest(nums, target, expected);
		}
	}
}

// using NUnit.Framework;
// using Task.Palindrome;
//
// namespace CodeClash.UserSolutionTest
// {
//     [TestFixture]
//     public class SolutionTaskTests
//     {
//         private SolutionTask solution = new SolutionTask();
//         private void RunTest(int number, bool expected)
//         {
//             var result = solution.IsPalindrome(number);
//             Assert.That(expected, Is.EqualTo(result));
//         }
//
//         [Test]
//         public void PalindromeTest_Example1()
//         {
//             var number = 101;
//             RunTest(number, true);
//         }
//
//         [Test]
//         public void PalindromeTest_Example2() => RunTest(205, false);
//
//         [Test]
//         public void PalindromeTest_Example3() => RunTest(-101, false);
//
//
//         [Test]
//         public void PalindromeTest_ZeroValue() => RunTest(0, true);
//
//         [Test]
//         public void PalindromeTest_OneValue() => RunTest(01, true);
//
//         [Test]
//         public void PalindromeTest_BigValue() => RunTest(999999999, true);
//
//         [Test]
//         public void PalindromeTest_MirrorValue() => RunTest(1234321, true);
//     }
// }
