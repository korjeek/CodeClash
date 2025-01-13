using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClash.UserSolutionTest
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

		[Test]
		public void LongestCommonPrefix_NoCommonPrefix() => RunTest(new string[] { "cat", "dog", "bird" }, "");

		[Test]
		public void LongestCommonPrefix_PartialMatch() => RunTest(new string[] { "internet", "internal", "interval" }, "inter");

		[Test]
		public void LongestCommonPrefix_AllSingleLetters() => RunTest(new string[] { "a", "a", "a" }, "a");

		[Test]
		public void LongestCommonPrefix_MixedCase() => RunTest(new string[] { "Abc", "abc", "abcd" }, "");

		[Test]
		public void LongestCommonPrefix_EmptyStrings() => RunTest(new string[] { "", "", "" }, "");

		[Test]
		public void LongestCommonPrefix_PrefixOneCharacter() => RunTest(new string[] { "b", "bake", "ball" }, "b");

		[Test]
		public void LongestCommonPrefix_NumericalStrings() => RunTest(new string[] { "12345", "123", "123456" }, "123");

		[Test]
		public void LongestCommonPrefix_SpecialCharacters() => RunTest(new string[] { "$special", "$spec", "$spaghetti" }, "$sp");

		[Test]
		public void LongestCommonPrefix_LongPrefixes() => RunTest(new string[] { "prefix", "prefecture", "preflight" }, "pref");

		[Test]
		public void LongestCommonPrefix_OneEmptyString() => RunTest(new string[] { "", "prefix", "preference" }, "");

		[Test]
		public void LongestCommonPrefix_StringsDifferAfterFirstLetter() => RunTest(new string[] { "xylophone", "x-ray", "xenon" }, "x");

		[Test]
		public void LongestCommonPrefix_IdenticalStrings() => RunTest(new string[] { "repeat", "repeat", "repeat" }, "repeat");

		[Test]
		public void LongestCommonPrefix_VeryShortStrings() => RunTest(new string[] { "a", "ab", "abc" }, "a");

		[Test]
		public void LongestCommonPrefix_VeryLongStrings() => RunTest(new string[] { new string('a', 1000), new string('a', 100), new string('a', 10) }, new string('a', 10));

		[Test]
		public void LongestCommonPrefix_NoStrings() => RunTest(new string[] { }, "");

		[Test]
		public void LongestCommonPrefix_LargeData1()
		{
			var strs = GenerateLargeData(100000, 10, 15);
			var expected = "";
			RunTest(strs, expected);
		}

		[Test]
		public void LongestCommonPrefix_LargeData2()
		{
			var strs = GenerateLargeData(1000000, 20, 30);
			var expected = "";
			RunTest(strs, expected);
		}

		private string[] GenerateLargeData(int size, int minLength, int maxLength)
		{
			var random = new Random();
			var strs = new string[size];
			for (int i = 0; i < size; i++)
			{
				var length = random.Next(minLength, maxLength + 1);
				var word = new string(Enumerable.Range(1, length).Select(_ => (char)random.Next(97, 123)).ToArray());
				strs[i] = word;
			}
			return strs;
		}

	}
}
