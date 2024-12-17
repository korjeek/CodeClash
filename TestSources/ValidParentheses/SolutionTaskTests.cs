using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Valid_Parentheses
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
	}
}
