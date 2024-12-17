using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.LongestCommonPrefix
{
	[TestFixture]
	public class SolutionTaskTests
	{
		private SolutionTask solution = new SolutionTask();

		private void RunTest(string[] args, string expected)
		{
			var result = solution.LongestCommonPrefix(args);
			Assert.That(expected, Is.EqualTo(result));
		}

		[Test]
		public void LongestCommonPrefix_Example1() => RunTest(new string[] { "flower", "flow", "flight" }, "fl");

		[Test]
		public void LongestCommonPrefix_Example2() => RunTest(new string[] { "dog", "race", "car" }, "");

		[Test]
		public void LongestCommonPrefix_Example3() => RunTest(new string[] { "racecar", "car" }, "");

		[Test]
		public void LongestCommonPrefix_TwoLetters() => RunTest(new string[] { "apple", "ape", "apricot" }, "ap");

		[Test]
		public void LongestCommonPrefix_ThreeLetters() => RunTest(new string[] { "coding", "codec", "code" }, "cod");

		[Test]
		public void LongestCommonPrefix_EmptyInput() => RunTest(new string[] { }, "");

		[Test]
		public void LongestCommonPrefix_SingleWord() => RunTest(new string[] { "hello" }, "hello");

		[Test]
		public void LongestCommonPrefix_SameWord() => RunTest(new string[] { "same", "same", "same" }, "same");

		[Test]
		public void LongestCommonPrefix_LongWords() => RunTest(new string[] { "verylongword", "verylongword", "verylongword" }, "verylongword");
	}
}
