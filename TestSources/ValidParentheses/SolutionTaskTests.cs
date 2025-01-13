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

		private void RunTest(string str, bool expected)
		{
			var result = solution.IsValid(str);
			Assert.That(expected, Is.EqualTo(result));
		}

		[Test]
		public void ValidParentheses_Example1() => RunTest("()", true);

		[Test]
		public void ValidParentheses_Example2() => RunTest("[", false);

		[Test]
		public void ValidParentheses_Example3() => RunTest("}{", false);

		[Test]
		public void ValidParentheses_BracketsInBrackets() => RunTest("{()}", true);

		[Test]
		public void ValidParentheses_BracketsInBrackets2() => RunTest("[[[]]]", true);

		[Test]
		public void ValidParentheses_IncorrectClosingBracket() => RunTest("((})", false);

		[Test]
		public void ValidParentheses_MoreClosingBrackets() => RunTest("[[[]]]]", false);

		[Test]
		public void ValidParentheses_MixedBrackets() => RunTest("{[}]()", false);

		[Test]
		public void ValidParentheses_EmptyString() => RunTest("", true);

		[Test]
		public void ValidParentheses_SingleOpeningBracket() => RunTest("(", false);

		[Test]
		public void ValidParentheses_SingleClosingBracket() => RunTest(")", false);

		[Test]
		public void ValidParentheses_NestedBrackets() => RunTest("{[()]}", true);

		[Test]
		public void ValidParentheses_UnmatchedBrackets() => RunTest("{[}", false);

		[Test]
		public void ValidParentheses_OnlyOpeningBrackets() => RunTest("(({{[[", false);

		[Test]
		public void ValidParentheses_OnlyClosingBrackets() => RunTest("))}}]]", false);

		[Test]
		public void ValidParentheses_MismatchedOrder() => RunTest("[({})]", true);

		[Test]
		public void ValidParentheses_LongValidSequence() => RunTest("()[]{}(()[]{})", true);

		[Test]
		public void ValidParentheses_LongInvalidSequence() => RunTest("({[}])", false);

		[Test]
		public void ValidParentheses_AlternatingBrackets() => RunTest("({)}", false);

		[Test]
		public void ValidParentheses_RepeatingValidSequence() => RunTest("()()()()()", true);

		[Test]
		public void ValidParentheses_RepeatingInvalidSequence() => RunTest("(((((((((", false);

		[Test]
		public void ValidParentheses_AllTypesUnmatched() => RunTest("{[(])}", false);

		[Test]
		public void ValidParentheses_AllTypesMatched() => RunTest("{[()()]}", true);

		[Test]
		public void ValidParentheses_MultiplePairs() => RunTest("{}[]()", true);

		[Test]
		public void ValidParentheses_MismatchedClosingFirst() => RunTest(")(", false);

		[Test]
		public void ValidParentheses_ValidSequenceWithSpaces() => RunTest("{ [ ( ) ] }", false);

		[Test]
		public void ValidParentheses_ComplexNested() => RunTest("{[({[()]})]}", true);

		[Test]
		public void ValidParentheses_ComplexNestedInvalid() => RunTest("{[({[()]})}}", false);
	}
}
