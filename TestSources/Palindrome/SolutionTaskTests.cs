using NUnit.Framework;

namespace CodeClash.UserSolutionTest
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

		[Test]
		public void PalindromeTest_NegativeNumber() => RunTest(-121, false);

		[Test]
		public void PalindromeTest_SingleDigitPositive() => RunTest(7, true);

		[Test]
		public void PalindromeTest_TwoDigitPalindrome() => RunTest(22, true);

		[Test]
		public void PalindromeTest_TwoDigitNonPalindrome() => RunTest(23, false);

		[Test]
		public void PalindromeTest_LargeEvenLengthPalindrome() => RunTest(1221, true);

		[Test]
		public void PalindromeTest_LargeEvenLengthNonPalindrome() => RunTest(1234, false);

		[Test]
		public void PalindromeTest_LargeOddLengthPalindrome() => RunTest(12321, true);

		[Test]
		public void PalindromeTest_LargeOddLengthNonPalindrome() => RunTest(12345, false);

		[Test]
		public void PalindromeTest_NumberWithTrailingZeros() => RunTest(1001, true);

		[Test]
		public void PalindromeTest_NonPalindromeWithZeros() => RunTest(12012, false);

		[Test]
		public void PalindromeTest_Zero() => RunTest(0, true);


		[Test]
		public void PalindromeTest_PalindromeWithOddDigits() => RunTest(13531, true);

		[Test]
		public void PalindromeTest_PalindromeWithEvenDigits() => RunTest(1441, true);

		[Test]
		public void PalindromeTest_LargeNumberNotPalindrome() => RunTest(123456789, false);

		[Test]
		public void PalindromeTest_LargeNumberIsPalindrome() => RunTest(543212345, true);

		[Test]
		public void PalindromeTest_BorderCasePalindrome() => RunTest(100000001, true);

		[Test]
		public void PalindromeTest_BorderCaseNonPalindrome() => RunTest(1000000002, false);

		[Test]
		public void PalindromeTest_NumberWithRepeatingDigitsPalindrome() => RunTest(11111111, true);

		[Test]
		public void PalindromeTest_NumberWithRepeatingDigitsNonPalindrome() => RunTest(11112111, false);
	}
}
