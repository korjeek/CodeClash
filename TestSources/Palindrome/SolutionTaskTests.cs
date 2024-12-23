using NUnit.Framework;

namespace Task.Palindrome
{
	[TestFixture]
	public class SolutionTaskTests
	{
		private SolutionTask solution = new SolutionTask();
		private void RunTest(int number, bool expected)
		{
			var result = solution.IsPalindrome(number);
			Assert.That(expected, Is.EqualTo(result));
		}

		[Test]
		public void PalindromeTest_Example1()
		{
			var number = 101;
			RunTest(number, true);
		}

		[Test]
		public void PalindromeTest_Example2() => RunTest(205, false);

		[Test]
		public void PalindromeTest_Example3() => RunTest(-101, false);


		[Test]
		public void PalindromeTest_ZeroValue() => RunTest(0, true);

		[Test]
		public void PalindromeTest_OneValue() => RunTest(01, true);

		[Test]
		public void PalindromeTest_BigValue() => RunTest(999999999, true);

		[Test]
		public void PalindromeTest_MirrorValue() => RunTest(1234321, true);
	}
}
